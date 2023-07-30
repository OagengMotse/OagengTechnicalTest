using System.Security.Principal;

namespace OagengTechnicalTest.Data.Models
{
    public class DetailData
    {
        public DateOnly TransactionDate { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public DateOnly EffectiveStatusDate { get; set; }
        public string TimeBreached { get; set; }
    }
}
