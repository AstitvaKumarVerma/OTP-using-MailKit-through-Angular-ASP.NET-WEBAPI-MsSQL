using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NETCore.MailKit.Core;
using SendOTP.Models;
using SendOTP.Repository;
using SendOTP.RequestModel;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<sdirectdbContext>(a => a.UseSqlServer("Server=192.168.0.240;Database=sdirectdb;UID=sdirectdb;PWD=sdirectdb;"));
builder.Services.AddScoped<IRegister, RegisterRepository>();




// Add services to the container.
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<sdirectdbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnectionString")));
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder
  .AllowAnyOrigin()
  .AllowAnyMethod()
  .AllowAnyHeader();
}));



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();



app.UseHttpsRedirection();
app.UseAuthentication();


app.UseAuthorization();




app.MapControllers();
app.UseCors("corsapp");



app.Run();

