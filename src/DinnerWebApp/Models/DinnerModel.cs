using System.Collections.Generic;

namespace DinnerWebApp.Models
{
    public class DinnerModel
    {
        public List<Dinner> Dinners { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
