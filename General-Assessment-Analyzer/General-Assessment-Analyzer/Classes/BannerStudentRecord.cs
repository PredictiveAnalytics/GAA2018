using System;
using System.Xml.Serialization;

namespace General_Assessment_Analyzer.Classes
{
    [Serializable]
    public class BannerStudentRecord
    {
        [XmlElement("ID")]
        public string ID { get; set; }
        [XmlElement("Last")]
        public string Last { get; set; }
        [XmlElement("First")]
        public string First { get; set; }
        [XmlElement("Middle")]
        public string Middle { get; set; }
        [XmlElement("Degree")]
        public string Degree { get; set; }
        [XmlElement("MajorCode")]
        public string MajorCode { get; set; }
        [XmlElement("MajorDesc")]
        public string MajorDesc { get; set; }
        [XmlElement("RateCode")]
        public string RateCode { get; set; }
        [XmlElement("RateDesc")]
        public string RateDesc { get; set; }
        [XmlElement("User")]
        public string User { get; set; }
        [XmlElement("Cohort")]
        public string Cohort { get; set; }

        public BannerStudentRecord()
        {

        }

    }
}
