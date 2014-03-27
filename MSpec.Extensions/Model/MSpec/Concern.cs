using System.Collections.Generic;
using System.Xml.Serialization;

namespace MSpec.Extensions.Model.MSpec
{
    public class Concern
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("context")]
        public List<Context> Contexts { get; set; } 
    }
}