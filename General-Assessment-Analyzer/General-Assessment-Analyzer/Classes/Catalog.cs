using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace General_Assessment_Analyzer.Classes
{
    public class Catalog
    {
        [XmlElement("CatalogEntry")]
        public List<CatalogEntry> Entries { get; set; }
    }
}
