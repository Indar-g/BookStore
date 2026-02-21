using BookStore.Data;
using BookStore.Data.Extensions;
using BookStore.Interfaces;
using BookStore.Models.Entities;
using BookStore.Repositories;
using BookStore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Book Store", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
}); //���������� ����������� � Swagger

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddScoped<IAuthorRepo, AuthorRepo>();
builder.Services.AddScoped<IBookRepo, BookRepo>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICartRepo, CartRepo>();

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true; //made so that there can be no same emails
}) //password and email requirements
.AddEntityFrameworkStores<AppDbContext>();

//diplom
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters 
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]) //complaining because it thinks that there is no signing key(there is)
            )
    };
}); 


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    //.WithOrigins("https://localhost:7174")
    .SetIsOriginAllowed(origin => true)
);


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
