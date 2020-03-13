using System;
using System.Text;

namespace FisOkuma.Models
{
    public class RecipeItemStr
    {
        public string LineDescription { get; set; }

        public string LineTax { get; set; }
        public string LineTotal { get; set; }

        public string ExpenseType { get; set; }

        public string ExpenseTypeCode { get; set; }
    }
}
