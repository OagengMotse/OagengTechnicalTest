using System.Security.Principal;

namespace OagengTechnicalTest.Data.Models
{
    public class Report
    {
       public int BranchCode {get;set;} 
       public string AccountType { get;set;}
       public string Status { get;set;} 
       public int TotalCount { get; set; }
       public double TotalAmount { get; set; }
    }
}
