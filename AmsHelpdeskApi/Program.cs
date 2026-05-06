using AmsHelpdeskApi.Application.Tickets.AssignTicket;
using AmsHelpdeskApi.Application.Tickets.CreateTicket;
using AmsHelpdeskApi.Application.Tickets.DeleteTicket;
using AmsHelpdeskApi.Application.Tickets.GetTicket;
using AmsHelpdeskApi.Application.Tickets.TakeTicket;
using AmsHelpdeskApi.Application.Tickets.UpdateTicket;
using AmsHelpdeskApi.Infrastructure.Data;
using AmsHelpdeskApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }   
    });
});

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AmsHelpdeskDb;Trusted_Connection=True"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false      
    };
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Where(e => e.Value.Errors.Count > 0).ToDictionary(e => e.Key, e => e.Value.Errors.Select(x => x.ErrorMessage).ToArray());

        var response = new
        {
            message = "Erro de validação",
            errors = errors
        };
        return new BadRequestObjectResult(response);
    };
});

builder.Services.AddScoped<CreateTicketUseCase>();
builder.Services.AddScoped<TakeTicketUseCase>();
builder.Services.AddScoped<AssignTicketUseCase>();
builder.Services.AddScoped<UpdateTicketUseCase>();
builder.Services.AddScoped<DeleteTicketUseCase>();
builder.Services.AddScoped<GetTicketUseCase>();

builder.Services.AddAuthorization();

builder.Services.AddScoped<PasswordService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
