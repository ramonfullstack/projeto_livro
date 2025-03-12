using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<BookStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(BookStoreDatabaseSettings)));

builder.Services.AddSingleton<IBookStoreDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<BookStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<BookService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", 
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();