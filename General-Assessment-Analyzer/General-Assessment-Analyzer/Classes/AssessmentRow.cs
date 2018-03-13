using System;
using System.Xml.Serialization;

namespace General_Assessment_Analyzer.Classes
{
    [Serializable]
    public class AssessmentRow
    {
        [XmlElement("Rubric_ID")]
        public  string Rubric_ID { get; set; }
        [XmlElement("RA_pk1")]
        public string RA_pk1 { get; set; }
        [XmlElement("Rubric_Name")]
        public string Rubric_Name { get; set; }
        [XmlElement("Grade_Column_ID")]
        public string Grade_Column_ID { get; set; }
        [XmlElement("Grade_Column_Name")]
        public string Grade_Column_Name { get; set; }
        [XmlElement("Course_ID")]
        public string Course_ID { get; set; }
        [XmlElement("Course_Name")]
        public string Course_Name { get; set; }
        [XmlElement("STUDENT_ID")]
        public string STUDENT_ID { get; set; }
        [XmlElement("Student_Login")]
        public string Student_Login { get; set; }
        [XmlElement("Rubric_Row_Header")]
        public string Rubric_Row_Header { get; set; }
        [XmlElement("Rubric_Column_Header")]
        public string Rubric_Column_Header { get; set; }
        [XmlElement("Rubric_Feedback")]
        public string Rubric_Feedback { get; set; }
        [XmlElement("Rubric_Cell_Feedback")]
        public string Rubric_Cell_Feedback { get; set; }
        [XmlElement("Rubric_Cell_Score")]
        public string Rubric_Cell_Score { get; set; }

    }
}
