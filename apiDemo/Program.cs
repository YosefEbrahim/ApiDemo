using apiDemo.Models;
using apiDemo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddDbContext<ApplicationDbContext>(optios =>
optios.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IMovieService, MovieService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc(
//        name: "v1",info:new OpenApiInfo
//        {
//            Version = "v1",
//            Title = "TestApi",
//            Description = "test api descrtion",
//            License = new OpenApiLicense
//            { 
//            Name ="my license",
//            Url= new Uri("https://facebook.com")
//            },
//            Contact = new OpenApiContact
//            {
//                Email = "yosefe00@gmail.com",
//                Name = "Yosef Ebrahim",
//                Url = new Uri("https://facebook.com")
//            }
            
            
//        });

//    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
//    {

//        Name = "Authriztion",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat ="JWT",
//         In=ParameterLocation.Header,
//         Description ="you should type bearer before token",
         

//    });
//    options.AddSecurityRequirement(securityRequirement: new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type =ReferenceType.SecurityScheme,
//                    Id ="Bearer"
//                },
//                Name ="Bearer",
//                In=ParameterLocation.Header
//                },
//            new List<String>()
//            }
//    }) ;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
