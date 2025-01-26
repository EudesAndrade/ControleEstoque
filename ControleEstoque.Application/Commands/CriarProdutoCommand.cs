using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.Application.Commands
{
    public class CriarProdutoCommand
    {
        public string Nome { get; set; }
        public string PartNumber { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
