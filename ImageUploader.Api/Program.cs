using ImageUploader.Api.Commands.Images.Upload;
using ImageUploader.Api.Common;
using ImageUploader.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFileCloudManager, AmazonS3FileManage>();
builder.Services.AddScoped<IUploadImageHandler, UploadImageHandler>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyHeader()
            .AllowAnyOrigin();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseMiddleware<CustomExceptionMiddleWare>();

app.UseAuthorization();

app.MapControllers();

app.Run();