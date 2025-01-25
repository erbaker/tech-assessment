using CSharp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to use different ports
builder.WebHost.ConfigureKestrel(options =>
{
	options.ListenLocalhost(5004); // HTTP
	options.ListenLocalhost(5005, configure => configure.UseHttps()); // HTTPS
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddSingleton<ICustomerRepository, JsonCustomerRepository>();
builder.Services.AddSingleton<IOrderRepository, JsonOrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
