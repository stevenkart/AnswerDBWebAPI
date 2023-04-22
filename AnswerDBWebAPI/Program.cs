using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using AnswerDBWebAPI.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddNewtonsoftJson();

        //Obtenemos informacion de la cadena de conexion almacenada en appsettings.json
        var CnnStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CNNSTR"));

        //Definimos una variable local que almacena el cnn string
        string cnnStr = CnnStrBuilder.ConnectionString;

        //definir la ccnstring al proyecto antes de iniciarlo
        builder.Services.AddDbContext<AnswersDBContext>(options => options.UseSqlServer(cnnStr));



        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}