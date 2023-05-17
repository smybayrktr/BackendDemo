using System;
using Core.Utilities.Results;
using Entities.Dtos;

namespace Business
{
	public interface IAuthService
	{
		IResult Register(RegisterAuthDto authDto);
		string Login(LoginAuthDto loginAuthDto);
	}
}

