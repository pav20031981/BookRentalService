using BookRentalService.Data;
using BookRentalService.Repository;
using BookRentalService.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<SmtpClient>(provider => new SmtpClient
{
    Host = "your-smtp-host",
    Port = 587, // SMTP port
    Credentials = new NetworkCredential("your-email@example.com", "your-email-password"),
    EnableSsl = true,
});

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
