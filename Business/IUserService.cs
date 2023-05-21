using System;
using Core.Utilities.Results;
using Entities;
using Entities.Dtos;

namespace Business
{
	public interface IUserService
	{
		IResult Add(RegisterAuthDto authDto);

        IResult Update(User user);

        IResult Delete(User user);

        IResult ChangePassword(UserChangePasswordDto userChangePasswordDto);

        IDataResult<List<User>> GetList();

		User GetByEmail(string email);

		IDataResult<User> GetById(int id);
	}
}

