using System;
using System.ComponentModel.DataAnnotations;

namespace DinnerWebApp.Models
{
    public class Dinner
    {
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; } 
        [Required]
        public string Owner { get; set; }
        [Required]
        public double BaseScore { get; set; }
        [Required]
        public double BonusPoints { get; set; }
        public double TotalScore
        {
            get { return BaseScore + BonusPoints; }
        }
    }
}
