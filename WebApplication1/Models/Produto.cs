using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome_produto { get; set; }
        public string Categoria { get; set; }
        public int Qtd_estoque { get; set; }
        public int Preco_venda { get; set; }
    }
}
