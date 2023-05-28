using System;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Dtos;

namespace Business
{
    public interface IAuthService
    {
        Task<IResult> Register(RegisterAuthDto registerDto);
        Task<IDataResult<Token>> Login(LoginAuthDto loginDto);
    }
}

