using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media;

namespace TrafficLightDetect
{
    public partial class MainWindow : Window
    {
        // Global varibles
        private bool fileDropped;
        private bool folderDropped;
        private string folderPath;
        private string filePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void UpdateImage(string imagePath, List<int[]> boxData)
        {
            // creates a new GeometryGroup labeled boxesToDraw
            var boxesToDraw = new GeometryGroup();

            // establishes the image source
            ImportImage.Source = new BitmapImage(new Uri(imagePath));
    
            // for loop that doubles as an if statement for boxes that need to be drawn
            for (int boxNum = 0; boxNum < boxData.Count(); boxNum++)
            {
                // adjusts x and y cords from xml data to better fit light
                int newXC = boxData[boxNum][2] - (boxData[boxNum][1] / 2);
                int newYC = boxData[boxNum][3] - (boxData[boxNum][0] / 2);
                int Width = boxData[boxNum][1];
                int Height = boxData[boxNum][0];

                // adds new rectangle to be drawn 
                boxesToDraw.Children.Add(new RectangleGeometry(new Rect(newXC, newYC, Width, Height)));
            }
            
            // triggers if there are boxes to be drawn
            if (boxData.Count() > 0)
            {
                // switch expression to integer to color value 
                var color = boxData[0][4] switch
                {
                    0 => Colors.Green,
                    1 => Colors.Red,
                    2 => Colors.Yellow,
                    3 => Colors.Blue,
                    _ => Colors.White
                };

                // adding color and dimension to object box
                ObjectBox.Data = boxesToDraw;
                ObjectBox.Stroke = new SolidColorBrush(color);
            }
        }

        private Dictionary<int, List<string>> GetLightStateDict(dataset xmlData)
        {
            // converts the data from the the Dataset.cs into an array so the states of the traffic lights can be referenced outside of the current frame
            var dataSetArray = xmlData.frame.AsEnumerable().ToArray();

            // list of current light states in one frame 
            List<string> lightState = new List<string>();

            // dict to be referenced when counting the encountered lights
            Dictionary<int, List<string>> lightStatePerFrame = new Dictionary<int, List<string>>();

            // iterates through the length of the array from the xml file
            for (int currentFrame = 0; currentFrame < dataSetArray.Length; currentFrame++)
            {
                // triggers when lights are detected in current frame
                if (dataSetArray[currentFrame].objectlist.Count() > 0)
                {
                    // triggers when multiple lights are detected in current frame
                    for (int lightNum = 0; lightNum < dataSetArray[currentFrame].objectlist.Count(); lightNum++)
                        // adds the string of the state of light to the list of strings
                        lightState.Add(dataSetArray[currentFrame].objectlist[lightNum].hypothesislist.hypothesis.subtype.Value);
                    // adds the list of light states to the lightStatePerFrame dict 
                    lightStatePerFrame.Add(currentFrame, lightState.ToList());
                }
                else
                {
                    // adds the empty list of light states to the lightStatePerFrame dict
                    lightStatePerFrame.Add(currentFrame, lightState.ToList());
                }

                // clears light state for next iteration
                lightState.Clear();
            }
            // dict return
            return lightStatePerFrame;
        }

        private async Task MainThing(string workingDirectory, dataset xmlData)
        {
            // checks if a files.txt document is present in directory
            var textFile = Path.Combine(workingDirectory, "files.txt");
            if (File.Exists(textFile))
            {
                // Varibles declarations for whole method
                // Parse the file, then iterate through all of the listed images at 25 FPS
                var linesFromText = await File.ReadAllLinesAsync(textFile);

                // new list for boxes around lights
                var boxData = new List<datasetFrameObjectBox>();

                // Dict used to store the state or state of each in light in each frame
                Dictionary<int, List<string>> lightStatePerFrame = GetLightStateDict(xmlData);

                // all intergers used 
                int frameNumber = 0; // setting start frame at 0 and every itteration it adds 1
                int lightsInLastFrame = 0; // used to keep track of total number of lights in current frame, based on xml file data
                int lightsDetected = 0; // used to keep track of total number of lights detected in xml file
                int stoppedFrameTimer = 0; // used to keep track of what frame the light turned red

                // list that has time stop at each light
                List<int> framesStoppedAtLight = new List<int>();

                // Loop that deteremines the images being shown
                // using the data from the "files.txt" provide, each line in the file is passed into the string line
                foreach (string line in linesFromText)
                {

                    // Varibles declarations for embedded loops
                    // a list of integer arrays used to store dimensions of boxes to be drawn on image
                    List<int[]> boxesToDraw = new List<int[]>();

                    // gets the file path of the images to be used by combining the directory given and the line from the "files.txt")
                    string imageFilePath = Path.Combine(workingDirectory, line.Trim());

                    // Image "Proccesing" and XML data utilization
                    // checks if the file path just made is valid 
                    if (File.Exists(imageFilePath))
                    {
                        // makes sure that the images are jpgs, based on images given
                        if (Path.GetExtension(imageFilePath).ToLower() == ".jpg")
                        {
                            // Varibles declarations for embedded loops
                            // requests a specific frameNumber from the datasetFrame made in the dataset.cs file
                            datasetFrame frameData = xmlData.frame.Single(f => f.number == frameNumber);

                            // gets how many lights are detected in the current frame using the xml data
                            int lightsDedectedInFrame = frameData.objectlist.Length;

                            // Gets data to draw boxes
                            // detects if lights are in current frame
                            if (lightsDedectedInFrame > 0)
                            {
                                // iterates through every light in frame
                                for (int light = 0; light < lightsDedectedInFrame; light++)
                                {
                                    // adds all the dimensional data need to draw box from xml data
                                    boxData.Add(frameData.objectlist[light].box);
                                }

                                // iterates through every light in boxData
                                for (int lightNum = 0; lightNum < boxData.Count(); lightNum++)
                                {
                                    // switch expression to convert specific string to integer
                                    int boxColor = lightStatePerFrame[frameNumber][lightNum] switch
                                    {
                                        "go" => 0,
                                        "stop" => 1,
                                        "warning" => 2,
                                        "ambiguous" => 3,
                                        _ => throw new ArgumentException()
                                    };

                                    // stores the boxData into a integer array so multiple boxes can be stored
                                    int[] dataForDrawingBox = { boxData[lightNum].h, boxData[lightNum].w, boxData[lightNum].xc, boxData[lightNum].yc, boxColor };

                                    // adds the data for drawing boxes into the list of integer arrays so it can be passed onto image proccess
                                    boxesToDraw.Add(dataForDrawingBox);
                                }
                            }

                            // Light Detection
                            // checks if the the current frame isn't 0 or the last frame
                            if (frameNumber > 1 && frameNumber < linesFromText.Length)
                            {
                                // triggers if the amount of lights in current frame isn't equal to the amount in the last frame
                                if (lightStatePerFrame[frameNumber].Count() != lightsInLastFrame)
                                    
                                    // triggers when the next frame has more lights than the current lights detected
                                    if (lightStatePerFrame[frameNumber + 1].Count() > lightsInLastFrame)
                                    
                                    // adds one to lights in last frame and lightsDetected because a light was detected
                                    { lightsInLastFrame++; lightsDetected++; }

                                // if no lights are detected the current or next frame then the lights for the next frame should be zero
                                if (lightStatePerFrame[frameNumber].Count() < lightsInLastFrame && lightStatePerFrame[frameNumber + 1].Count() < lightsInLastFrame)
                                { lightsInLastFrame = lightsInLastFrame - 1; }
                            }

                            // Time Stoped
                            // checks if lights are detected in current frame
                            if (lightStatePerFrame[frameNumber].Count() != 0)
                            {
                                // triggers if light has state "stop" and a stop frame timer hasn't been already triggered
                                if (lightStatePerFrame[frameNumber][0] == "stop" && stoppedFrameTimer == 0)
                                {
                                    // sets current frame to stoppedFrameTimer  
                                    stoppedFrameTimer = frameNumber;
                                }

                                // triggers if light has state "go" and a stop frame timer has already been triggered
                                if ((lightStatePerFrame[frameNumber][0] == "go" && stoppedFrameTimer != 0))
                                {
                                    // adds amount of time in seconds stopped by light to the list of integers
                                    framesStoppedAtLight.Add((frameNumber - stoppedFrameTimer) / 25);

                                    // resets stoppedFrameTimer
                                    stoppedFrameTimer = 0;
                                }
                            }

                            // if the light is red and goes out of frame
                            if (lightStatePerFrame[frameNumber].Count() == 0 && stoppedFrameTimer != 0)
                            { 
                                // adds amount of time in seconds stopped by light to the list of integers
                                framesStoppedAtLight.Add((frameNumber - stoppedFrameTimer) / 25);

                                // resets stoppedFrameTimer
                                stoppedFrameTimer = 0;
                            }

                            // Displaying Data
                            // displays current frame, total frames, how many lights have been detected, lights in frame
                            CurrentFrame.Text = frameNumber.ToString();
                            TotalFrames.Text = linesFromText.Length.ToString();
                            LightsEncountered.Text = lightsDetected.ToString();
                            LightsInFrame.Text = lightsInLastFrame.ToString();

                            // displays after the first instance of a complete stop is completed
                            if (framesStoppedAtLight.Count() > 0)
                            {
                                // getting the average and sum for time at lights
                                TimeStopped.Text = (TimeSpan.FromSeconds(framesStoppedAtLight.Sum())).ToString("mm\\:ss");
                                AverageTimeStopped.Text = (TimeSpan.FromSeconds(Math.Floor(framesStoppedAtLight.Average()))).ToString("mm\\:ss");
                            }

                            // End of iteration, updates image, clears the boxData to not have the same box draun more than once, increases frame number and delays the next iteration
                            // update the image using the file path and the data need to draw boxes where needed
                            UpdateImage(imageFilePath, boxesToDraw);

                            // clears boxes current frame of 
                            boxData.Clear();

                            // adds to framenumber
                            frameNumber++;

                            // delaying the program for 40 ms to play at 25 FPS
                            await Task.Delay(40);
                        }
                    }
                }
            }
        }

        private async void itemInput_Drop(object sender, DragEventArgs e)
        {
            // checks if item was droppped
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // gets path of item dropped into StackPanel
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string droppedItemPath = Path.GetFullPath(files[0]);

                // checks if dropped item is the .xml file
                if (Path.GetExtension(droppedItemPath).ToLower() == ".xml")
                {
                    // sets filePath to the dropped item's path
                    filePath = @droppedItemPath;
                    fileDropped = true;
                }

                // gets the file attributes for the dropped item and checks if its a directory
                FileAttributes attr = File.GetAttributes(@droppedItemPath);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    // sets folderPath to the dropped item's path
                    folderPath = @droppedItemPath;
                    folderDropped = true;
                }

                // waits for both file and folder to be dropped
                if (fileDropped == true && folderDropped == true)
                {
                    // uses the Dataset.cs file to parse and serialize the given xml file, so information can be called easier later in the program
                    var serializer = new XmlSerializer(typeof(dataset));
                    using var file = File.OpenRead(@filePath);
                    var xmlData = (dataset)serializer.Deserialize(file);

                    // sends the path of the folder and the serialized version of the xml data to ShowFilesInFolder
                    await MainThing(folderPath, xmlData);
                }
            }
        }
    }
}