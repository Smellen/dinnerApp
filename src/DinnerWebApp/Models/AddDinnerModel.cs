using System.Collections.Generic;

namespace DinnerWebApp.Models
{
    public class AddDinnerModel
    {
        public List<Owner> Owners { get; set; }
        public Dinner Dinner { get; set; }
    }
}
