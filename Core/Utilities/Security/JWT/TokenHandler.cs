using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Core.Extensions;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.JWT
{
	public class TokenHandler:ITokenHandler
	{
        //Appsettings.json daki Token yapısına ulaşmam lazım o yüzden IConfiguration kullanıcaz.
        IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateToken(User user, List<OperationClaim> operationClaims)
        {
            Token token = new Token();

            //Appsettingsdeki securityKeyin simetriğini aldık.
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Şifrelenmiş kimliği oluşturcaz. İlk secuirtykey ve şifreleme algoritmasını göndermemiz gerekiyor.
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //Token ayarlamalarını yapıcaz.
            token.Expiration = DateTime.Now.AddMinutes(60); //Token 60 dakika sonra bitecek.

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: token.Expiration,
                claims: SetClaims(user,operationClaims),
                notBefore: DateTime.Now, //Token üretildikten ne zaman sonra devreye girsin
                signingCredentials: signingCredentials
                ); 
            //Token oluşturucusu sınıfından örnek alacaz.
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //Token üretcez
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(securityToken);

            //Refresh token üretcez.

            token.RefreshToken = CreateRefreshToken();
            return token;


        }


        //LifeTime=true ise refresh token üretmemiz lazım onu yazacaz
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddName(user.Name); //Biz oluşturduk bu yapıyı
            claims.AddRoles(operationClaims.Select(p=>p.Name).ToArray());
            return claims;
        }

    }
}

