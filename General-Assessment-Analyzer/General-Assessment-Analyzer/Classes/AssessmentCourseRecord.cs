using System;
using System.Xml.Serialization;
/// <summary>
/// Data class to store Course Records from Banner.  
/// 
/// Identical to the Banner course record with the exception of the BB_Course_ID Variable to make matching easier. 
/// </summary>
namespace General_Assessment_Analyzer.Classes
{
    [Serializable]
    public class AssessmentCourseRecord
    {
        [XmlElement("Term")]
        public string Term { get; set; }
        [XmlElement("Term_Desc")]
        public string Term_Desc { get; set; }
        [XmlElement("CRN")]
        public string CRN { get; set; }
        [XmlElement("Subject")]
        public string Subject { get; set; }
        [XmlElement("Number")]
        public string Number { get; set; }
        [XmlElement("Title")]
        public string Title { get; set; }
        [XmlElement("Section")]
        public string Section { get; set; }
        [XmlElement("Enrollment")]
        public string Enrollment { get; set; }
        [XmlElement("PTRM")]
        public string PTRM { get; set; }
        [XmlElement("Start_Date")]
        public string Start_Date { get; set; }
        [XmlElement("End_Date")]
        public string End_Date { get; set; }
        [XmlElement("Instructor_Last")]
        public string Instructor_Last { get; set; }
        [XmlElement("Instructor_First")]
        public string Instructor_First { get; set; }
        [XmlElement("Email")]
        public string Email { get; set; }
        [XmlElement("BB_Course_ID")]
        public string BB_Course_ID { get; set; }
        public AssessmentCourseRecord()
        {

        }

    }
}

