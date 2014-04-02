using System.Collections.Generic;
using System.Xml.Serialization;

namespace MSpec.Extensions.Model.MSpec
{
    public class Assembly
    {
        [XmlAttribute("time")]
        public long ExecutionTime { get; set; }

        [XmlAttribute("location")]
        public string Location { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("output")]
        public string CapturedOutput { get; set; }

        [XmlElement("concern")]
        public List<Concern> Concerns { get; set; }
    }
}