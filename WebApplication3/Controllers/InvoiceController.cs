using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using WebApplication3.Context;
using WebApplication3.Entity;
using WebApplication3.InterFace;

namespace WebApplication3.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController :ControllerBase
    {

        private readonly IDataBaseService<InvoiceHeader> _InvoiceHeader;
        private readonly IDataBaseService<User> _IuserData;
        private readonly IDataBaseService<InvoiceDetail> _InvoiceDetail;

        private readonly Iuser _Iuser;


        public InvoiceController(IDataBaseService<InvoiceHeader> InvoiceHeader, Iuser iuser, IDataBaseService<User> IuserData, IDataBaseService<InvoiceDetail> InvoiceDetail) 
        {
            _InvoiceHeader = InvoiceHeader;
            _Iuser = iuser;
            _IuserData = IuserData;
            _InvoiceDetail = InvoiceDetail;

        }


        [HttpPost("Save")]

        public async Task<IActionResult> InsertHeaderWithDetail(InvoiceHeader invoiceHeader)
        {
            if (!ModelState.IsValid)
            {


                return BadRequest(ModelState);
            }


            
            try
            {



                invoiceHeader.UserId = _Iuser.GetUserID();
                var userdata=await _IuserData.Find(x=>x.UserId== invoiceHeader.UserId);
                invoiceHeader.InvoiceName = userdata.UserName;

                invoiceHeader.TotalAmount = invoiceHeader.InvoiceDetails.Sum(x => x.LineTotal);
                //ordersHDs.orderConfigDs.Select(x=>x.OrderConfigHID)= ordersHDs.OrderConfigHID;
                var Data = await _InvoiceHeader.Save(invoiceHeader);




                return Ok(Data);
            }
            catch (SqlException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid)
            {


                return BadRequest(ModelState);
            }



            try
            {

                //var Data = await _InvoiceHeader.GetAll();
                var Data = await _InvoiceHeader.GetInclude(x => x.InvoiceDetails);


                return Ok(Data);
            }
            catch (SqlException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{ID}")]
        public async Task<IActionResult> Delete(int ID)
        {
            if (!ModelState.IsValid)
            {


                return BadRequest(ModelState);
            }



            try
            {

                var Data = await _InvoiceHeader.Delete(ID);

                return Ok(Data);
            }
            catch (SqlException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] InvoiceHeader invoiceHeader)
        {
            if (!ModelState.IsValid)
            {


                return BadRequest(ModelState);
            }



            try
            {

                var OldOrderConfigDsData = await _InvoiceDetail.FindAll(x => x.InvoiceHId == invoiceHeader.InvoiceHId);
                var RemovedOldOrderConfigDsData = _InvoiceDetail.Compare(OldOrderConfigDsData.ToList(), invoiceHeader.InvoiceDetails.ToList(), x => x.DetailId);

                if (!RemovedOldOrderConfigDsData.IsNullOrEmpty())
                {
                    await _InvoiceDetail.DeleteRange(RemovedOldOrderConfigDsData);

                }
                invoiceHeader.UserId = _Iuser.GetUserID();
                var Data = await _InvoiceHeader.Update(invoiceHeader);

                return Ok(Data);
            }
            catch (SqlException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
