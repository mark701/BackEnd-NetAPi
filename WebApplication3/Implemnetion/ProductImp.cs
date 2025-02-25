using WebApplication3.Entity.DataBase;
using WebApplication3.Entity.Security;
using WebApplication3.InterFace;

namespace WebApplication3.Implemnetion
{
    public class ProductImp : IProduct
    {
        private readonly IDataBaseService<Product> _product;
        private readonly Iuser _Iuser;
        private readonly string PathString = "Assets/products";
        public ProductImp( IDataBaseService<Product> product, Iuser iuser)
        {
            _product = product;
            _Iuser = iuser;

        }
        public async Task<Product> Save(ProductData product)
        {
            string? PathName = null;

            // If an image is provided, save it and get the image name.
            if (product.ProfileImage.FileName != null)
            {
                 PathName = await _product.SaveImageAsync(product.ProfileImage, PathString);
            }

            Product data = new Product()
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                Productquantity = product.Productquantity,
                PathImage = PathName,
                CreateDateAndTime = DateTime.Now,
                UserId = _Iuser.GetUserID(),

            };
                return await _product.Save(data);


            
          
        }

        public async Task<Product> Update(ProductData product)
        {
            var database = await _product.Find(x => x.ProductID == product.ProductID);

            string? Pathimage = null;

            // If an image is provided, save it and get the image name.
            if (product.ProfileImage != null)
            {
                Pathimage = await _product.SaveImageAsync(product.ProfileImage, PathString);
            }
            else
            {
                Pathimage = database.PathImage;

            }

            // Create the product object with or without the image.
            var data = new Product
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                Productquantity = product.Productquantity,
                PathImage = Pathimage,
                CreateDateAndTime = DateTime.Now,
                UserId = _Iuser.GetUserID(),
            };

            // Update the product.
            return await _product.Update(data);
        }
    }
}
