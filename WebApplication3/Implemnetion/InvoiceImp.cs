using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Entity.DataBase;
using WebApplication3.InterFace;
using WebApplication3.Migrations;

namespace WebApplication3.Implemnetion
{
    public class InvoiceImp : Iinvoice
    {
        private readonly IDataBaseService<InvoiceHeader> _Invoice;
        private readonly IDataBaseService<Product> _Product;
        private  readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private readonly Iuser _Iuser;
        public InvoiceImp(IDataBaseService<InvoiceHeader> invoiceHeader, IDataBaseService<Product> Product, Iuser iuser)
        {
            _Invoice = invoiceHeader;
            _Iuser = iuser;
            _Product = Product;

        }

        public async Task<InvoiceHeader> Save(InvoiceHeader invoiceHeader)
        {


            invoiceHeader.UserId = _Iuser.GetUserID();
            invoiceHeader.InvoiceName = GenerateTimestampedCode();
            invoiceHeader.TotalAmount = invoiceHeader.InvoiceDetails.Sum(x => x.LineTotal);

            foreach (var detail in invoiceHeader.InvoiceDetails)
            {
                // Fetch product from database
                var product = await _Product.Find(x => x.ProductID == detail.ProductID);
                if (product == null)
                {
                    throw new Exception($"Product with ID {detail.ProductID} not found.");
                }

                // Check if there is enough stock
                if (product.Productquantity < detail.Quantity)
                {
                    throw new Exception($"Not enough stock for product {product.ProductName}. Available: {product.Productquantity}, Requested: {detail.Quantity}");
                }

                // Reduce stock quantity
                product.Productquantity -= detail.Quantity;

                // Update product in database
                await _Product.Update(product);

            }

            var Data = await _Invoice.Save(invoiceHeader);

            return Data;
        }

        private  string GenerateTimestampedCode()
        {
            var timestamp = DateTime.Now.ToString("yyMMddHHmmss");
            var randomPart = new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
            return $"INV-{timestamp}-{randomPart}";
        }






    }
}
