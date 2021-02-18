using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Dinner
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Owner Owner { get; set; }
        public double BaseScore { get; set; }
        public double BonusPoints { get; set; }
        public double TotalScore
        {
            get { return BaseScore + BonusPoints; }
        }
    }
}
