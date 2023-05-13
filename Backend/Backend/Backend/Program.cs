using Backend.Access;
using Backend.Service;
using Store = Backend.Service.Store;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var provider = builder.Services.BuildServiceProvider();
var configuration=provider.GetRequiredService<IConfiguration>();
builder.Services.AddCors(options =>
{
    var FrontUrl = "http://localhost:3000";
    options.AddDefaultPolicy(builder =>
    { builder.WithOrigins(FrontUrl).AllowAnyMethod().AllowAnyHeader(); });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    Backend.Service.Store.Instance.LoadStores();
    Backend.Service.Store.Instance.LoadOrders();
    Backend.Service.User.Instance.LoadMembers();
    Backend.Business.src.Utils.ChatManager.LoadChats();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
