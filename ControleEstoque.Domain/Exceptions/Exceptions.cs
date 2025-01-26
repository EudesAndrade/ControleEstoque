using System;

namespace ControleEstoque.Domain.Exceptions
{
    public class EstoqueInsuficienteException : Exception
    {
        public EstoqueInsuficienteException(string mensagem)
            : base(mensagem) { }
    }
}
