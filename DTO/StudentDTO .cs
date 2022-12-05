using Domain.DTO;
using Project_VIS.Domain.Exceptions;
using Project_VIS.Domain.TableModule;
using System.Text;
using System.Text.RegularExpressions;

namespace Project_VIS.Domain.DTO
{
    public class StudentDTO : IUserDTO
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Clear();
            sb.Append("Uživatel čílo: ");
            sb.Append(this.Id);
            sb.Append("             ");
            sb.Append(this.First_Name + ' ' + this.Last_Name);
            sb.Append("\n\t");
            sb.Append("Login: ");
            sb.Append(this.Email + "\n");


            return sb.ToString();
        }
        public IUserDTO LogIn(string email, string password)
        {
            var _tableModule = new StudentTM();
            StringBuilder where = new StringBuilder();
            where.Clear();
            where.Append("WHERE email = '");
            where.Append(email);
            where.Append("' AND password = '");
            where.Append(password);
            where.Append("'");

            return  _tableModule.GetAll(where.ToString()).First() as IUserDTO;

        }

    }
}