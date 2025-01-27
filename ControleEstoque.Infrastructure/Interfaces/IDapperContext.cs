using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.Infrastructure.Interfaces
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection();
    }
}
