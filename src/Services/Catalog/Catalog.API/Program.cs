var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

//Add services to the container.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBahavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("CatalogConnection"));
}).UseLightweightSessions();


if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<Catalog.API.Data.CatalogInitialData>();


builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();

//Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();