using System;
namespace Entities.Dtos
{
	public class RegisterAuthDto
	{
		public string EMail { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

		public string ImageUrl { get; set; }
    }
}

