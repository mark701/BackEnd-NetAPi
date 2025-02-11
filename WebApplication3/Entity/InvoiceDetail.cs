using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication3.Entity
{
    public class InvoiceDetail 
    {
        public int DetailId { get; set; }
        public int InvoiceHId { get; set; }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int LineTotal => Quantity * UnitPrice;

        // Navigation property to InvoiceHeader

        public InvoiceHeader? InvoiceHeader { get; set; }

        public Product? Product { get; set; }
    }
}
