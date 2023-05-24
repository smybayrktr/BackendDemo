using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependecyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using DataAccess;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

//Şema veriyoruz(Nugetten eklediğimiz paketin şemasını verdik.)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true, //Oluşturulacak token değerinin kimlerin veya hangi sitelerin kontrol edip etmeyeceğini kontrol eder.
        ValidateIssuer = true, //Oluşturulacak token değeri kimden alındı bilgisini verir.
        ValidateLifetime = false, //Token belli süre sonra bitsin mi hayır dedik.
        ValidateIssuerSigningKey = true, //Üretilecek token değerinin uygulamamıza ait değer olduğunu ifade eden security keyin doğrulaması
        ValidIssuer = builder.Configuration["Token:Issuer"], //Oluşturulan yapı içinde tanımlanacak. Oluşturulan token içinde Issuer değerine sahipsin dedik.
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero //Expression süresine bu süreyi ekler. Saat farklarında sorun yaşamamak için +5 falan verilir
    };
});


builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule(),
});

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

