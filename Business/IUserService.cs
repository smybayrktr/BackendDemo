using System;
using Entities;
using Entities.Dtos;

namespace Business
{
	public interface IUserService
	{
		void Add(RegisterAuthDto authDto);
		List<User> GetList();
		User GetByEmail(string email);
	}
}

