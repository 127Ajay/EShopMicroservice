namespace Catalog.API.Products.GetProductByID;

//public record GetProductByIDRequest();

public record GetProductByIDResponse(Product Product);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}",
            async (ISender sender, Guid id) =>
            {
                var result = await sender.Send(new GetProductByIDQuery(id));
                var response = result.Adapt<GetProductByIDResponse>();
                return Results.Ok(response);
            })
            .Produces<GetProductByIDResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Product by ID")
            .WithDescription("Get Product by ID");
    }
}
