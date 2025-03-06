using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication3.Context;
using WebApplication3.Entity.DataBase;
using WebApplication3.InterFace;
using WebApplication3.Migrations;

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
        private readonly Iinvoice _Iinvoice;


        public InvoiceController(IDataBaseService<InvoiceHeader> InvoiceHeader, Iinvoice Iinvoice, Iuser iuser, IDataBaseService<User> IuserData, IDataBaseService<InvoiceDetail> InvoiceDetail) 
        {
            _InvoiceHeader = InvoiceHeader;
            _Iuser = iuser;
            _IuserData = IuserData;
            _Iinvoice = Iinvoice;
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



                //invoiceHeader.UserId = _Iuser.GetUserID();
                //var userdata=await _IuserData.Find(x=>x.UserId== invoiceHeader.UserId);
                //    invoiceHeader.InvoiceName = userdata.UserName;

                //invoiceHeader.TotalAmount = invoiceHeader.InvoiceDetails.Sum(x => x.LineTotal);
                ////ordersHDs.orderConfigDs.Select(x=>x.OrderConfigHID)= ordersHDs.OrderConfigHID;
                //var Data = await _InvoiceHeader.Save(invoiceHeader);


                var Data = await _Iinvoice.Save(invoiceHeader);



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

                var Data = await _InvoiceHeader.GetAll();
                //var Data = await _InvoiceHeader.GetInclude(x => x.InvoiceDetails);


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


        [HttpGet("GetHeaderDetail")]
        public async Task<IActionResult> GetDetailByID()
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

        [HttpGet("GetHeaderDetailsPages")]
        public async Task<IActionResult> GetHeaderDetailsPages(int pageNumber = 1, int pageSize = 10)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            {
                try
                {
                    var UserID = _Iuser.GetUserID();
                    // Get data and total count
                    var (totalCount, data) = await _InvoiceHeader.GetIncludePages(pageNumber, pageSize , x=>x.UserId==UserID, x => x.InvoiceDetails);

                    // Return a response with both the totalCount and data
                    return Ok(new
                    {
                        TotalCount = totalCount,
                        Data = data
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("GetDetailsByHeaderID/{ID}")]
        public async Task<IActionResult> GetDetailByHeaderID(int ID)
        {
            if (!ModelState.IsValid)
            {


                return BadRequest(ModelState);
            }



            try
            {

                //var Data = await _InvoiceHeader.GetAll();
                var Data = await _InvoiceDetail.FindAll(x=>x.InvoiceHId== ID);


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
