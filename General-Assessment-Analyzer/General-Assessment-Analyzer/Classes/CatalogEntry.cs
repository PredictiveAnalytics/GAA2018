using System; 
using System.Xml.Serialization;

namespace General_Assessment_Analyzer.Classes
{
    public class CatalogEntry
    {
        [XmlElement("CourseKey")]
        public string CourseKey { get; set; }
        [XmlElement("Subject")]
        public string Subject { get; set; }
        [XmlElement("Course")]
        public string Course { get; set; }
        [XmlElement("Assessment")]
        public string Assessment { get; set; }
    }
}
