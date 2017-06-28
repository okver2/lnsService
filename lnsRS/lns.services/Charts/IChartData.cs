using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lns.services.Charts
{
    public interface IChartData
    {
        string Key { get; set; }
        int Value { get; set; }
    }
}
