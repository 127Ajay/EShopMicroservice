
namespace Catalog.API.Products.GetProductByID;

public record GetProductByIDQuery(Guid Id) : IQuery<GetProductByIDResult>;

public record GetProductByIDResult(Product Product);
internal class GetProductByIDQueryHandler(IDocumentSession session, ILogger<GetProductByIDQueryHandler> logger)
    : IQueryHandler<GetProductByIDQuery, GetProductByIDResult>
{
    public async Task<GetProductByIDResult> Handle(GetProductByIDQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIDQueryHandler.Handle called with {@Query}", query);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if(product == null)
        {
            throw new ProductNotFoundException();
        }

        return new GetProductByIDResult(product);
    }
}
