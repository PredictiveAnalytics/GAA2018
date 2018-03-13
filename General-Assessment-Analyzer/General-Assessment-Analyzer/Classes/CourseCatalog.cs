using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace General_Assessment_Analyzer.Classes
{
    [Serializable]
    public class CourseCatalog
    {
        [XmlElement("Entry")]
        public List<CourseCatalogEntry> Entries { get; set; }
    }
}
