using System;
namespace Core.Utilities.Security.JWT
{
	//Token yapımzı karşılayacak sınıf
	public class Token
	{
		public string AccessToken { get; set; }

		public DateTime Expiration { get; set; } //Tokenin bitiş süresi

		public string RefreshToken { get; set; } //Expiression süresi bittiğinde tokenın yenilenmesi
	}
}


