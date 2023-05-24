using System;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Dtos;

namespace Business
{
	public interface IAuthService
	{
		IResult Register(RegisterAuthDto authDto);
		IDataResult<Token> Login(LoginAuthDto loginAuthDto);
	}
}

