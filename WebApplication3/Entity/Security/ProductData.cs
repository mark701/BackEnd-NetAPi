namespace WebApplication3.Entity.Security
{
    public class ProductData
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }

        public int Productquantity { get; set; }

        public IFormFile? ProfileImage { get; set; }
        public string? ImageUrl { get; set; }   // <-- Add this to return full image path

    }
}
