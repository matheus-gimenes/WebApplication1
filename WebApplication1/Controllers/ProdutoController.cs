using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IConfiguration _configutarion;
        public ProdutoController(IConfiguration configuration)
        {
            _configutarion = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.tb_produto";
            DataTable table = new DataTable();
            string sqlDataSource = _configutarion.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Produto prod)
        {
            string query = @"insert into dbo.tb_produto values(@id, @nome_produto, @categoria, @qtd_estoque, @preco_venda)";
            DataTable table = new DataTable();
            string sqlDataSource = _configutarion.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", prod.Id);
                    myCommand.Parameters.AddWithValue("@nome_produto", prod.Nome_produto);
                    myCommand.Parameters.AddWithValue("@categoria", prod.Categoria);
                    myCommand.Parameters.AddWithValue("@qtd_estoque", prod.Qtd_estoque);
                    myCommand.Parameters.AddWithValue("@preco_venda", prod.Preco_venda);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Adicionado com sucesso");
        }

    }
}
