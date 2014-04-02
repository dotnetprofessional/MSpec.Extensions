using System.Xml.Serialization;

namespace MSpec.Extensions.Model.MSpec
{
    public class Runtime
    {
        [XmlAttribute("time")]
        public long ExecutionTime { get; set; }
        
    }
}