using CsvHelper;
using OagengTechnicalTest.Data.Models;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection.PortableExecutable;

namespace OagengTechnicalTest.Utilities
{
    public static class Utilities
    {
        public static List<Transaction> deserializeCVS(Stream file)
        {
            var records = new List<Transaction>();
            using (var reader = new StreamReader(file))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<Transaction>().ToList();
                }
            }
            return records;
        }
        public static class Enumeration
        {
            public static IDictionary<int, string> GetAll<TEnum>() where TEnum : struct
            {
                var enumerationType = typeof(TEnum);
                if (!enumerationType.IsEnum)
                    throw new ArgumentException("Enumeration type is expected.");
                var dictionary = new Dictionary<int, string>();

                foreach (int value in Enum.GetValues(enumerationType))
                {
                    var name = Enum.GetName(enumerationType, value);
                    dictionary.Add(value, name);
                }

                return dictionary;
            }
        }
    }
}
