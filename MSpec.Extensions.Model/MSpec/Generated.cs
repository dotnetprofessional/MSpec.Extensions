using System.Xml.Serialization;

namespace MSpec.Extensions.Model.MSpec
{
    public class Generated
    {
        [XmlElement("date")]
        public string Date { get; set; }

        [XmlElement("time")]
        public string ExecutionTime { get; set; }
    }
}