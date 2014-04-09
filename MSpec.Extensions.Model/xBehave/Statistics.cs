using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSpec.Extensions.Model.xBehave
{
    public class Statistics
    {
        public int PassCount { get; set; }

        public int FailedCount { get; set; }

        public int IgnoredCount { get; set; }
 
        public int NotImplementedCount { get; set; }

        public int Total { get { return this.PassCount + this.FailedCount + this.IgnoredCount; } }

        public int SucessPercent
        {
            get
            {
                if (this.Total == 0)
                    return 0;

                return (int)(this.PassCount/(double)this.Total * 100);
            }
        }

        public int FailPercent
        {
            get
            {
                if (this.Total == 0)
                    return 0;

                return (int)(this.FailedCount / (double)this.Total * 100);
            }
        }
        public int IgnoredPercent
        {
            get
            {
                if (this.Total == 0)
                    return 0;

                return (int)(this.IgnoredCount / (double)this.Total * 100);
            }
        }
        public void Update(ThenStatus status)
        {
            switch (status)
            {
                case ThenStatus.Pass:
                    this.PassCount ++;
                    break;
                case ThenStatus.Failed:
                    this.FailedCount++;
                    break;
                case ThenStatus.Ignored:
                    this.IgnoredCount++;
                    break;
                case ThenStatus.NotImplemented:
                    this.NotImplementedCount++;
                    break;
            }
        }

        public ThenStatus Status
        {
            get
            {
                if (this.FailedCount > 0)
                    return ThenStatus.Failed;
                else if (this.IgnoredCount > 0)
                    return ThenStatus.Ignored;
                else if (this.PassCount > 0)
                    return ThenStatus.Pass;
                else return ThenStatus.NotImplemented;
            }
        }

        public void Update(Statistics statistics)
        {
            this.PassCount += statistics.PassCount;
            this.FailedCount += statistics.FailedCount;
            this.IgnoredCount += statistics.IgnoredCount;
            this.NotImplementedCount += statistics.NotImplementedCount;
        }
    }
}
