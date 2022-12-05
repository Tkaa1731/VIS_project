using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public interface IUserDTO
    {
        int Id { get; set; }
        string First_Name { get; set; }
        string Last_Name { get; set; }
        string Email { get; set; }
        string Password { get; set; }

        IUserDTO LogIn(string email, string password);
    }
}
