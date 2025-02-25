using WebApplication3.Entity.DataBase;
using WebApplication3.Entity.Security;

namespace WebApplication3.InterFace
{
    public interface IProduct
    {
        Task<Product> Save(ProductData product);
        Task<Product> Update(ProductData product);

    }
}
