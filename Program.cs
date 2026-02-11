using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Repositories;
using BookStore.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddScoped<IAuthorRepo, AuthorRepo>();
builder.Services.AddScoped<IBookRepo, BookRepo>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
