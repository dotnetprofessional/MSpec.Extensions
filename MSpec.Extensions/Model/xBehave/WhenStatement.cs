using System.Collections.Generic;

namespace MSpec.Extensions.Model.xBehave
{
    public class WhenStatement
    {
        public WhenStatement()
        {
            this.Thens = new List<ThenStatement>();
        }

        public string Narration { get; set; }

        public List<ThenStatement> Thens { get; set; } 
    }
}