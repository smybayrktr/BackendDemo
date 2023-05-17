using System;
using Core.Utilities.Hashing;
using DataAccess;
using Entities;
using Entities.Dtos;

namespace Business
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(RegisterAuthDto authDto)
        {

            var ext = authDto.Image.FileName.Substring(authDto.Image.FileName.LastIndexOf('.')).ToLower();
            string fileName = Guid.NewGuid().ToString();

            var path = "./Content/Images/" + fileName;
            using (var stream = System.IO.File.Create(path))
            {
                authDto.Image.CopyTo(stream);
            }

                byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(authDto.Password, out passwordHash ,out passwordSalt);

            User user = new User();
            user.Id = 0;
            user.EMail = authDto.EMail;
            user.Name = authDto.Name;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ImageUrl = fileName;

            _userDal.Add(user);
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(p=> p.EMail == email);
        }

        public List<User> GetList()
        {
            return _userDal.GetAll();
        }
    }
}

