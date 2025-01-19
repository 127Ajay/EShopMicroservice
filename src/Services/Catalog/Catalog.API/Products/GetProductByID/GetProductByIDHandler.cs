
namespace Catalog.API.Products.GetProductByID;

public record GetProductByIDQuery(Guid Id) : IQuery<GetProductByIDResult>;

public record GetProductByIDResult(Product Product);
internal class GetProductByIDQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByIDQuery, GetProductByIDResult>
{
    public async Task<GetProductByIDResult> Handle(GetProductByIDQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if(product == null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return new GetProductByIDResult(product);
    }
}
