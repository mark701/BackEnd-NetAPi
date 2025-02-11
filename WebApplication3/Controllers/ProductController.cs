using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication3.Entity;
using WebApplication3.Entity.Security;
using WebApplication3.InterFace;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace WebApplication3.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IDataBaseService<Product> _ProductData;
        private readonly Iuser _Iuser;

        public ProductController(IDataBaseService<Product> ProductData, Iuser iuser) 
        {
            _ProductData = ProductData;
            _Iuser = iuser;
        }
        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {


                return BadRequest(ModelState);
            }



            try
            {
                product.UserId= _Iuser.GetUserID();
                var Data = await _ProductData.Save(product);

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

                var Data = await _ProductData.GetAll();

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

                var Data = await _ProductData.Delete(ID);

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
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {


                return BadRequest(ModelState);
            }



            try
            {
                product.UserId = _Iuser.GetUserID();
                var Data = await _ProductData.Update(product);

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
