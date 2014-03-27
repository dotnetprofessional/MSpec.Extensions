using System.Collections.Generic;
using System.Xml.Serialization;

namespace MSpec.Extensions.Model.MSpec
{
    public class MSpec
    {
        [XmlElement("generated")]
        public Generated Generated { get; set; }

        [XmlElement("run")]
        public Runtime Runtime { get; set; }

        [XmlElement("assembly")]
        public List<Assembly> Assemblies { get; set; } 
    }
}