using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.DataTypes.Database.Weather
{
    [Serializable]
    public class Metric
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }
}
