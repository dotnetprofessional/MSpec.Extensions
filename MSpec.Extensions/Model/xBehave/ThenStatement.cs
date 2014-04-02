using System.Xml.Serialization;

namespace MSpec.Extensions.Model.xBehave
{
    public class ThenStatement
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public long ExecutionTime { get; set; }

        public string CapturedOutput { get; set; }

        [XmlAttribute]
        public ThenStatus Status { get; set; }


    }
}