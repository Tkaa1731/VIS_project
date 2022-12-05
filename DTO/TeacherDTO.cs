using Domain.DTO;
using Project_VIS.Domain.Exceptions;
using Project_VIS.Domain.TableModule;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Project_VIS.Domain.DTO
{
    public struct TeacherProfile
    {
        public string first_name;
        public string last_name;
        public bool? active;
        public string offer_text;
    }
    public class TeacherDTO : IUserDTO
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string Offer_Text { get; set; }

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
            sb.Append(this.Email + "\n\t");
            sb.Append(this.Active ?"PŘIBÍRÁ":"NEPŘIBÍRÁ");
            sb.Append(" nové žáky");
            sb.Append("\n\t");
            sb.Append("POPIS:");
            sb.Append("\n\t");
            sb.Append(this.Offer_Text);

            return sb.ToString();
        }

        public IUserDTO LogIn(string email, string password)
        {
            var _tableModule = new TeacherTM();
            StringBuilder where = new StringBuilder();
            where.Clear();
            where.Append("WHERE email = '");
            where.Append(email);
            where.Append("' AND password = '");
            where.Append(password);
            where.Append("'");
                
            return _tableModule.GetAll(where.ToString()).First() as IUserDTO;
        }
        public bool Update(TeacherProfile profile)
        {
            if (profile.first_name is not null)
                First_Name = profile.first_name;
            if(profile.last_name is not null)
                Last_Name = profile.last_name;
            if(profile.active is not null)
                Active = (bool) profile.active;
            if(profile.offer_text is not null)
                Offer_Text = profile.offer_text;
            TeacherTM teacherTM = new TeacherTM();
            return teacherTM.Update(this);
        }
        public IEnumerable<StudentDTO> GetMyStudents()
        {
            StudentTM _studentTM = new StudentTM();
            return _studentTM.GetStudentsOfTeacher(this.Id);
        }
        public bool AddStudent(int student_id)
        {
            if (!this.Active)
            {
                throw new NoActiveTeacher("Neaktivni uživatel nemůže přidávat žáky");
                return false;
            }
            Teacher_StudentTM tM = new Teacher_StudentTM();
            if(tM.CheckExist(student_id,this.Id) is not null)
            {
                throw new RelationAlreadyExists($"Žák id {student_id} je už učiteli přidělen");
                return false;
            }
            tM.Create(student_id, this.Id);
            return true;
        }
    }
}