using System.Collections.Generic;

namespace MSpec.Extensions.Model.xBehave
{
    public class WhenStatement
    {
        public WhenStatement()
        {
            this.Thens = new List<ThenStatement>();
            this.Statistics = new Statistics();
        }

        public string Narration { get; set; }

        public List<ThenStatement> Thens { get; set; }

        public void AddThen(ThenStatement then)
        {
            this.Thens.Add(then);
            this.Statistics.Update(then.Status);

        }
        public Statistics Statistics { get; set; }
    }
}