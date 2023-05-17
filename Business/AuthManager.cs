using System;
using System.ComponentModel.DataAnnotations;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Results;
using Entities.Dtos;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

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

        [ValidationAspect(typeof(UserValidator))]
        public IResult Register(RegisterAuthDto authDto)
        {
            IResult result = BusinessRules.Run
                (CheckIfEmailExists(authDto.EMail),
                CheckIfImageSize(authDto.Image.Length),
                CheckIfImageExtension(authDto.Image.FileName));

            if (result != null)
            {
                return result;

            }
            
            _userService.Add(authDto);
            return new SuccessResult("İşlem Başarılı");
        }

        private IResult CheckIfEmailExists(string email)
        {
            var list = _userService.GetByEmail(email);
            if (list != null)
            {
                return new ErrorResult("Bu mail adresi zaten mevcut");
            }
            return new SuccessResult("Kayıt Başarılı");
        }

        private IResult CheckIfImageSize(long imgSize)
        {
            var imgMbSize = Convert.ToDecimal(imgSize * 0.000001);

            if (imgMbSize > 2)
            {
                return new ErrorResult("Img size küçük olmalı") ;
            }
            return new SuccessResult("Başarılı");
        }

        private IResult CheckIfImageExtension(string fileName)
        {
            var extension = fileName.Substring(fileName.LastIndexOf('.')).ToLower();
            List<string> AllowFileExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };
            if (!AllowFileExtensions.Contains(extension))
            {
                return new ErrorResult("Image Format Dışı");
            }
            return new SuccessResult("İşlem Başarılı");
        }
    }
}

