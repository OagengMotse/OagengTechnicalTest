using OagengTechnicalTest.Data.Models;
using OagengTechnicalTest.Utilities;
using System.Net;

namespace OagengTechnicalTest.Data
{
    public class TransactionsService
    {
        public Stream CSVFile { get; set; }
        public List<Transaction> transactions { get; set; }
        public string lastError { get; set; }
        
        public void updateTransactionList(MasterData master)
        {
            int accType=0;  
            switch(master.AccountType.ToLower())
            {
                case "savings":
                    {
                        accType = 2;    
                    }
                    break;
                case "cheque":
                    {
                        accType = 1;
                    }
                    break;
            }

            List<int> indexes = new List<int>();
            transactions.Where(x => x.AccountNumber == master.AccountNumber).ToList().ForEach(x =>
            {
              int index=transactions.FindIndex(y => y.PaymentId == x.PaymentId);
               transactions[index].AccountType =accType;
            });
        }
        public void uploadFile(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        CSVFile = new MemoryStream(fileBytes);
                    }
                    createTransectionList();
                }
            }
            catch(Exception ex)
            {
                lastError = ex.Message; 
            }
      
        }
        private void createTransectionList()
        {
            transactions = Utilities.Utilities.deserializeCVS(CSVFile);
            lastError = "";
        }
    }
}
