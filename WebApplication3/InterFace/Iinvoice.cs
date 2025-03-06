using WebApplication3.Entity.DataBase;

namespace WebApplication3.InterFace
{
    public interface Iinvoice
    {
        Task<InvoiceHeader> Save(InvoiceHeader invoiceHeader);

    }
}
