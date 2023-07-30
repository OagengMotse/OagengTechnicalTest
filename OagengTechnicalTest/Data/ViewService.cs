using OagengTechnicalTest.Data.Enum;
using OagengTechnicalTest.Data.Models;
using OagengTechnicalTest.Utilities;

namespace OagengTechnicalTest.Data
{
    public class ViewService
    {
        private readonly TransactionsService _service;
        public IDictionary<int, string> accountTypes;

        public object CSVFile { get { return _service.CSVFile; } }
        public string lastError { get { return _service.lastError; } }
        public List<Transaction> transactions { get { return _service.transactions; } }

        public List<Report> reports { get; set; }
        public List<MasterData> masterData { get; set; }

        public ViewService(TransactionsService service)
        {
            this._service = service;
            accountTypes = Utilities.Utilities.Enumeration.GetAll<AccountType>();
        }

        public void prepareReport()
        {
            reports= new List<Report>();
            transactions.DistinctBy(x => x.BranchCode).ToList().ForEach(x =>
            {
               var res1= transactions.Where(y => y.BranchCode == x.BranchCode && y.AccountType == 1 && x.Status == "00").ToList();
                reports.Add(new Report { BranchCode = x.BranchCode, AccountType = "Cheque", Status = "Successful", TotalCount = res1.Count, TotalAmount = res1.Sum(z => z.Amount) });

                var res2 = transactions.Where(y => y.BranchCode == x.BranchCode && y.AccountType == 1 && x.Status == "30").ToList();
                reports.Add(new Report { BranchCode = x.BranchCode, AccountType = "Cheque", Status = "Disputed", TotalCount = res2.Count, TotalAmount = res2.Sum(z => z.Amount) });

                var res3 = transactions.Where(y => y.BranchCode == x.BranchCode && y.AccountType == 1 && x.Status != "00" && x.Status != "30").ToList();
                reports.Add(new Report { BranchCode = x.BranchCode, AccountType = "Cheque", Status = "Failed", TotalCount = res3.Count, TotalAmount = res3.Sum(z => z.Amount) });

                var res4 = transactions.Where(y => y.BranchCode == x.BranchCode && y.AccountType == 2 && x.Status == "00").ToList();
                reports.Add(new Report { BranchCode = x.BranchCode, AccountType = "Savings", Status = "Successful", TotalCount = res4.Count, TotalAmount = res4.Sum(z => z.Amount) });

                var res5 = transactions.Where(y => y.BranchCode == x.BranchCode && y.AccountType == 2 && x.Status == "30").ToList();
                reports.Add(new Report { BranchCode = x.BranchCode, AccountType = "Savings", Status = "Disputed", TotalCount = res5.Count, TotalAmount = res5.Sum(z => z.Amount) });

                var res6 = transactions.Where(y => y.BranchCode == x.BranchCode && y.AccountType == 2 && x.Status != "00" && x.Status != "30").ToList();
                reports.Add(new Report { BranchCode = x.BranchCode, AccountType = "Savings", Status = "Failed", TotalCount = res6.Count, TotalAmount = res6.Sum(z => z.Amount) });

            });
        }
        public void prepareMasterData()
        {
            masterData = new List<MasterData>();
            transactions.DistinctBy(x=>x.AccountNumber).ToList().ForEach(x =>
            {
                var accType = "";
                if (x.AccountType == 1)
                    accType = "Cheque";
                else if (x.AccountType == 2)
                    accType = "Savings";
                else
                    accType = "Unknown";

                masterData.Add(new MasterData { AccountHolder = x.AccountHolder, AccountType = accType, AccountNumber = x.AccountNumber, BranchCode = x.BranchCode });
            });
        }
        public List<DetailData> prepareDetailData(MasterData master)
        {
            var res= new List<DetailData>();
            transactions.Where(x => x.AccountNumber == master.AccountNumber).ToList().ForEach(x =>
            {
                var status = "";

                if (x.Status == "00")
                    status = "Successful";
                else if (x.Status == "30")
                    status = "Disputed";
                else 
                    status = "Failed";

                var timeBreached = "";
                var effectiveDate = DateTime.ParseExact(x.EffectiveStatusDate, "dd/MM/yyyy", null);
                var transactionDate = DateTime.ParseExact(x.TransactionDate, "dd/MM/yyyy", null);
                if ((effectiveDate.Date-transactionDate.Date).Days > 7)
                    timeBreached = "Yes";
                else 
                    timeBreached = "No";

                res.Add(new DetailData { Amount = x.Amount, EffectiveStatusDate = DateOnly.FromDateTime(effectiveDate), Status = status, TimeBreached = timeBreached, TransactionDate = DateOnly.FromDateTime(transactionDate) });
            
            });
            return res;
        }
        public void updateMasterData(MasterData master)
        {
            _service.updateTransactionList(master);
        }
    }
}
