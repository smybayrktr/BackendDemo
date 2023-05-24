using System;
using Entities;

namespace Core.Utilities.Security.JWT
{
	public interface ITokenHandler
	{
		//Kullanıcı giriş yapınca token oluşturup kullanıcıya vermesi lazım onun için yazdık.
		//Kullanıcımızı ve yetkisini verdik.
		Token CreateToken(User user, List<OperationClaim> operationClaims); 
		
	}
}

