using System;
using System.Xml.Serialization;

namespace General_Assessment_Analyzer.Classes
{
    [Serializable]
    public class ScalePoint
    {
        [XmlElement("Label")]
        public string Label { get; set; }
        [XmlElement("Value")]
        public int Value { get; set; }

        public int CompareTo(ScalePoint other)
        {
            return Value.CompareTo(other.Value);
        }

        public ScalePoint()
        {

        }

        public ScalePoint(string lb, int val)
        {
            Label = lb;
            Value = val;
        }
    }
}
