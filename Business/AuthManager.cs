using System;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Hashing;
using Core.Utilities.Results;
using Entities.Dtos;
using FluentValidation.Results;

namespace Business
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;

        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public string Login(LoginAuthDto loginAuthDto)
        {
            var user = _userService.GetByEmail(loginAuthDto.Email);
            var result = HashingHelper.VerifyPasswordHash(loginAuthDto.Password, user.PasswordHash, user.PasswordSalt);
            if (result)
            {
                return "Success";
            }
            return "Fail";
        }

        public IResult Register(RegisterAuthDto authDto, int imgSize)
        {
            imgSize = 0; 
            UserValidator userValidator = new UserValidator();
            ValidationTool.Validate(userValidator, authDto);

            
            bool isExist = CheckIfEmailExists(authDto.EMail);
            if (isExist)
            {
               
                var isImageSize = CheckIfImageSize(imgSize);
                if (isImageSize)
                {
                    _userService.Add(authDto);
                    return new SuccessResult("İşlem Başarılı");
                }
                else
                {
                    return new ErrorResult("Yüklenen resim boyutu max 1mb olmalı");
                }
            }
            else
            {
                return new ErrorResult("İşlem Başarısız");
            }

        }

        bool CheckIfEmailExists(string email)
        {
            var list = _userService.GetByEmail(email);
            if (list != null)
            {
                return false;
            }
            return true;
        }

        bool CheckIfImageSize(int imgSize)
        {
            if (imgSize>1)
            {
                return false;
            }
            return true;
        }
    }
}

