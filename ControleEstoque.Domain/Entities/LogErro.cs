using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.Domain.Entities
{
    public class LogErro
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public string StackTrace { get; set; }
        public DateTime DataHora { get; set; }

        public LogErro(string mensagem, string stackTrace)
        {
            Mensagem = mensagem;
            StackTrace = stackTrace;
            DataHora = DateTime.UtcNow;
        }
    }
}
