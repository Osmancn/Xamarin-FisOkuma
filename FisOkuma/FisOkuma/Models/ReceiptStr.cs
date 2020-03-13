using System.Collections.Generic;

namespace FisOkuma.Models
{
    public class ReceiptStr
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string SerialNumber { get; set; }
        public string DocumentNumber { get; set; }
        public string PlateNumber { get; set; }
        public string GrandTotal { get; set; }
        public string TaxTotal { get; set; }
        public string ExpenseTypeCode { get; set; }
        public string ExpenseType { get; set; }
        public string ReceiptType { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public List<RecipeItemStr> RecipeItems { get; set; }

    }
}
