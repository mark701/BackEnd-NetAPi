using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using WebApplication3.Context;
using WebApplication3.Implemnetion;
using WebApplication3.InterFace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure the database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IDataBaseService<>), typeof(DataBaseServiceImp<>));

builder.Services.AddScoped<Iuser, UserService>();
builder.Services.AddScoped<Iinvoice, InvoiceImp>();

builder.Services.AddScoped<IProduct, ProductImp>();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true, // ENABLE LIFETIME VALIDATION
            ClockSkew = TimeSpan.Zero
        };

    });

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true; // Optional: Formats JSON output for readability
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularApp", builder =>
//    {
//        builder.WithOrigins("http://localhost:4200") // Angular app URL
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .WithExposedHeaders("Content-Disposition"); // Add if needed

        });
});


builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

//app.UseCors("AllowAngularApp");
app.UseCors("AllowAll");


app.UseStaticFiles();


app.UseHttpsRedirection(); // Important for security
app.UseRouting();

app.UseAuthentication(); // MUST be before UseAuthorization
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();




