using ControleEstoque.Domain.Entities;
using System.Threading.Tasks;

namespace ControleEstoque.Infrastructure.Interfaces
{
    public interface ILogErroRepository
    {
        Task RegistrarLogAsync(LogErro log);
    }
}
