using System;

namespace DinnerWebApp.Models
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
