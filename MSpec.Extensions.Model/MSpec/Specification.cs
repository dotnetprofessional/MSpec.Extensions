using System;
using System.Xml.Serialization;

namespace MSpec.Extensions.Model.MSpec
{
    [Serializable]
    public class Specification
    {
        [XmlAttribute("leader")]
        public string Leader { get; set; }

        [XmlAttribute("time")]
        public long ExecutionTime { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("output")]
        public string CapturedOutput { get; set; }

        [XmlAttribute("status")]
        public string Status { get; set; }

        [XmlAttribute("field-name")]
        public string Field { get; set; }
    }
}