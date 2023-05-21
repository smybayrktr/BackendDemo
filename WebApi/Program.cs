using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business;
using Business.DependencyResolvers.Autofac;
using DataAccess;
using DataAccess.EntityFramework;

var builder = WebApplication.CreateBuilder(args);
 

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
 * Site bazlı izin vermek için kullanılabilir.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder => builder.WithOrigins("https://localhost:7124", "yenistie, yenisite2.com"));//izin verilme şartları
});
//Web tarayıcısından gelen http isteklerinin izni olup omadığını kontrol eden yapı
*/

//Tüm istekleri karşılamak istiyosak;
builder.Services.AddCors(options =>
{
    //Gelen header isteğini kabul et, gelen metot isteğini kabul et, gelen içeriği kabul et
    options.AddPolicy("AllowOrigin", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

builder.Services.AddAuthentication(JwtBearerDefaults);



builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder=>builder.RegisterModule(new AutofacBusinessModule()));


//builder.Services.AddSingleton<IOperationClaimService, OperationClaimManager>();
//builder.Services.AddSingleton<IOperationClaimDal, EfOperationClaimDal>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin"); //Kullanımı. Çağırıyoruz

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

