using WebApplication3.Comman.Models;

namespace WebApplication3.Entity.DataBase
{
    public class Product : CreatedUser
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public int ProductPrice { get; set; }

        public int Productquantity { get; set; }
        public string? PathImage { get; set; }

        public float rating { get; set; }

        public ICollection<InvoiceDetail>? InvoiceDetails { get; set; }


    }
}
