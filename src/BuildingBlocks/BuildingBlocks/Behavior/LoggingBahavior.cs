using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behavior;
public class LoggingBahavior<TRequest, TResponse>(ILogger<LoggingBahavior<TRequest, TResponse>> logger):
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        logger.LogInformation("[START] Handling request={Request} - response={response} - RequestData={RequestData}", 
                typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();

        timer.Start();

        var response = await next();

        var timeTaken = timer.Elapsed;

        if(timeTaken.Seconds > 3)
        {
            logger.LogWarning("[SLOW PERFORMANCE] Handling request={Request} took {TimeTaken}",
                typeof(TRequest).Name, timeTaken.Seconds);
        }
        logger.LogInformation("Handled {Request} with {response}", typeof(TRequest).Name, typeof(TResponse).Name);
        return response;
    }
}
