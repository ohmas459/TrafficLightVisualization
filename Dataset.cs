using System;

namespace TrafficLightDetect
{

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class dataset
    {

        private datasetFrame[] frameField;

        private string nameField;

        private decimal versionField;

        private string commentsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("frame")]
        public datasetFrame[] frame
        {
            get
            {
                return this.frameField;
            }
            set
            {
                this.frameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public decimal version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string comments
        {
            get
            {
                return this.commentsField;
            }
            set
            {
                this.commentsField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class datasetFrame
    {

        private datasetFrameObject[] objectlistField;

        private object grouplistField;

        private ushort numberField;

        private ushort secField;

        private bool secFieldSpecified;

        private ushort msField;

        private bool msFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("object", IsNullable = false)]
        public datasetFrameObject[] objectlist
        {
            get
            {
                return this.objectlistField;
            }
            set
            {
                this.objectlistField = value;
            }
        }

        /// <remarks/>
        public object grouplist
        {
            get
            {
                return this.grouplistField;
            }
            set
            {
                this.grouplistField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public ushort number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public ushort sec
        {
            get
            {
                return this.secField;
            }
            set
            {
                this.secField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool secSpecified
        {
            get
            {
                return this.secFieldSpecified;
            }
            set
            {
                this.secFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public ushort ms
        {
            get
            {
                return this.msField;
            }
            set
            {
                this.msField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool msSpecified
        {
            get
            {
                return this.msFieldSpecified;
            }
            set
            {
                this.msFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class datasetFrameObject
    {

        private byte orientationField;

        private datasetFrameObjectBox boxField;

        private string appearanceField;

        private datasetFrameObjectHypothesislist hypothesislistField;

        private byte idField;

        /// <remarks/>
        public byte orientation
        {
            get
            {
                return this.orientationField;
            }
            set
            {
                this.orientationField = value;
            }
        }

        /// <remarks/>
        public datasetFrameObjectBox box
        {
            get
            {
                return this.boxField;
            }
            set
            {
                this.boxField = value;
            }
        }

        /// <remarks/>
        public string appearance
        {
            get
            {
                return this.appearanceField;
            }
            set
            {
                this.appearanceField = value;
            }
        }

        /// <remarks/>
        public datasetFrameObjectHypothesislist hypothesislist
        {
            get
            {
                return this.hypothesislistField;
            }
            set
            {
                this.hypothesislistField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class datasetFrameObjectBox
    {

        private byte hField;

        private byte wField;

        private short xcField;

        private short ycField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte h
        {
            get
            {
                return this.hField;
            }
            set
            {
                this.hField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte w
        {
            get
            {
                return this.wField;
            }
            set
            {
                this.wField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public short xc
        {
            get
            {
                return this.xcField;
            }
            set
            {
                this.xcField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public short yc
        {
            get
            {
                return this.ycField;
            }
            set
            {
                this.ycField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class datasetFrameObjectHypothesislist
    {

        private datasetFrameObjectHypothesislistHypothesis hypothesisField;

        /// <remarks/>
        public datasetFrameObjectHypothesislistHypothesis hypothesis
        {
            get
            {
                return this.hypothesisField;
            }
            set
            {
                this.hypothesisField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class datasetFrameObjectHypothesislistHypothesis
    {

        private datasetFrameObjectHypothesislistHypothesisType typeField;

        private datasetFrameObjectHypothesislistHypothesisSubtype subtypeField;

        private decimal evaluationField;

        private byte idField;

        private decimal prevField;

        /// <remarks/>
        public datasetFrameObjectHypothesislistHypothesisType type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public datasetFrameObjectHypothesislistHypothesisSubtype subtype
        {
            get
            {
                return this.subtypeField;
            }
            set
            {
                this.subtypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public decimal evaluation
        {
            get
            {
                return this.evaluationField;
            }
            set
            {
                this.evaluationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public decimal prev
        {
            get
            {
                return this.prevField;
            }
            set
            {
                this.prevField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class datasetFrameObjectHypothesislistHypothesisType
    {

        private decimal evaluationField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public decimal evaluation
        {
            get
            {
                return this.evaluationField;
            }
            set
            {
                this.evaluationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class datasetFrameObjectHypothesislistHypothesisSubtype
    {

        private decimal evaluationField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public decimal evaluation
        {
            get
            {
                return this.evaluationField;
            }
            set
            {
                this.evaluationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
}


