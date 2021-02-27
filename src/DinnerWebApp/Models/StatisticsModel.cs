using System.Collections.Generic;
using System.ComponentModel;

namespace DinnerWebApp.Models
{
    public class StatisticsModel
    {
        // Average Dinner score per owner
        [DisplayName("Average dinner count per cook")]
        public Dictionary<Owner, double> AveragePerOwner { get; set; }

        // Best rated dinner
        [DisplayName("Best dinner")]
        public Dinner BestRatedDinner { get; set; }
        // Total Dinner

        [DisplayName("Total dinners")]
        public int TotalAmountOfDinnersTracked { get; set; }
        // Average dinner score

        [DisplayName("Average score")]
        public double AverageDinnerScore { get; set; }

    }
}
