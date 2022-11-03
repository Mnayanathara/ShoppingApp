using Microsoft.AspNetCore.Mvc;
using System.Data;

using System.Data.SqlClient;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet(Name = "GetAllProducts")]
        public JsonResult GetAll()
        {
            DataTable table= new DataTable();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductAppConnection")))
            {
                con.Open();
                string query = @"Select * from dbo.Product";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {

                        sda.Fill(ds);
                    }

                }
            }

            //table.Columns.Add("Name");
            //table.Columns.Add("Description");
            //table.Columns.Add("Price");
            //table.Columns.Add("Image");

            //DataRow dataRow = table.NewRow();
            //dataRow["Name"] = "ccc";
            //dataRow["Description"] = "cccdesc";
            //dataRow["Price"] = "20.00";
            //dataRow["Image"] = "200";
            //table.Rows.Add(dataRow);


            return new JsonResult(ds);
        }
    }
}
