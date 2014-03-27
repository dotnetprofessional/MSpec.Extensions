using System.Collections.Generic;
using System.Xml.Serialization;

namespace MSpec.Extensions.Model.MSpec
{
    public class Context
    {
        [XmlAttribute("time")]
        public long ExecutionTime { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type-name")]
        public string Type { get; set; }

        [XmlElement("output")]
        public string CapturedOutput { get; set; }

        [XmlElement("specification")]
        public List<Specification> Specifications { get; set; } 
    }
}
