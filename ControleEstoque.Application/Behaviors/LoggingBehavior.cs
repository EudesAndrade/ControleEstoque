using ControleEstoque.Domain.Entities;
using ControleEstoque.Infrastructure.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ControleEstoque.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogErroRepository _logErroRepository;

        public LoggingBehavior(ILogErroRepository logErroRepository)
        {
            _logErroRepository = logErroRepository;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                // Continua a execução normalmente
                return await next();
            }
            catch (Exception ex)
            {
                // Captura erro e registra no banco
                var logErro = new LogErro(
                    mensagem: $"Erro ao processar {typeof(TRequest).Name}: {ex.Message}",
                    stackTrace: ex.StackTrace
                );

                await _logErroRepository.RegistrarLogAsync(logErro);

                throw; // Repropagar a exceção para manter o fluxo normal da aplicação
            }
        }
    }
}
