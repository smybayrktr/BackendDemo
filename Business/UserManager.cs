using System;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Caching;
using Core.Aspects.Transaction;
using Core.Aspects.Validation;
using Core.Utilities.Hashing;
using Core.Utilities.Results;
using DataAccess;
using Entities;
using Entities.Dtos;

namespace Business
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IFileService _fileService;


        public UserManager(IUserDal userDal, IFileService fileService)
        {
            _userDal = userDal;
            _fileService = fileService;
        }

        [CacheRemoveAspect("IUserService.GetList")]
        public IResult Add(RegisterAuthDto authDto)
        {
            var user = CreateUser(authDto);
            _userDal.Add(user);
            return  new SuccessResult("Başarılı");
        }

        private User CreateUser(RegisterAuthDto registerAuthDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(registerAuthDto.Password, out passwordHash, out passwordSalt);
            User user = new User();
            user.Id = 0;
            user.EMail = registerAuthDto.EMail;
            user.Name = registerAuthDto.Name;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ImageUrl = _fileService.FileSaveToServer(registerAuthDto.Image, "./Content/Images/");
            return user;

        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(p=> p.EMail == email);
        }

        [CacheAspect(30)]
        public IDataResult<List<User>> GetList()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(),"Listelendi.");
        }


        [ValidationAspect(typeof(UserValidator))]
        [TransactionAspect()]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.Updated);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult("Silindi");
        }


        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(p=>p.Id == id));
        }

        [ValidationAspect(typeof(UserChangePasswordValidator))]
        public IResult ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var user = _userDal.Get(p=>p.Id == userChangePasswordDto.UserId);
            bool result = HashingHelper.VerifyPasswordHash(userChangePasswordDto.CurrentPassword,
                user.PasswordHash, user.PasswordSalt);
            if (!result)
            {
                return new ErrorResult(Messages.WrongCurrentPassword);
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(userChangePasswordDto.NewPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userDal.Update(user);
            return new SuccessResult(Messages.PasswordChanged);
        }

        public List<OperationClaim> GetUserOperationClaims(int userId)
        {
            return _userDal.GetUserOperationClaims(userId);
        }
    }
}

