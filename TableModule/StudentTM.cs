using Project_VIS.Database;
using Project_VIS.Domain.DTO;
using Project_VIS.Domain.Exceptions;
//using Project_VIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_VIS.Domain.TableModule
{// TABLEMODULE prevadi TableDataGateway na DataTableObject
    public class StudentTM
    {
        private readonly Table_Student _studentTDG;
        public StudentTM()
        {
            _studentTDG = new Table_Student();
        }

        public StudentDTO GetById(int id)
        {
            var table = _studentTDG.GetById(id);

            if (table.Rows.Count == 0)
            {
                throw new EntityNotFoundExeption("Entity not found");
            }

            var row = table.Rows[0];
            return new StudentDTO
            {
                Id = Convert.ToInt32(row["student_id"]),
                First_Name = row["first_name"]?.ToString() ?? "",
                Last_Name = row["last_name"]?.ToString() ?? "",
                Email = row["email"]?.ToString() ?? "",
            };
        }

        public IEnumerable<StudentDTO> GetAll(string where)
        {
            var table = _studentTDG.GetAll(where);
            if (table.Rows.Count == 0)
            {
                throw new EntityNotFoundExeption("Entity not found");
            }

            List<StudentDTO> result = new();

            foreach (DataRow row in table.Rows)
            {
                var item = new StudentDTO
                {
                    Id = Convert.ToInt32(row["student_id"]),
                    First_Name = row["first_name"]?.ToString() ?? "",
                    Last_Name = row["last_name"]?.ToString() ?? "",
                    Email = row["email"]?.ToString() ?? "",
                };
                result.Add(item);
            }

            return result;
        }
        public IEnumerable<StudentDTO> GetStudentsOfTeacher(int teacher_id)
        {
            var table = _studentTDG.GetStudentsOfTeacher(teacher_id);
            if (table.Rows.Count == 0)
            {
                throw new EntityNotFoundExeption("Entity not found");
            }

            List<StudentDTO> result = new();

            foreach (DataRow row in table.Rows)
            {
                var item = new StudentDTO
                {
                    Id = Convert.ToInt32(row["student_id"]),
                    First_Name = row["first_name"]?.ToString() ?? "",
                    Last_Name = row["last_name"]?.ToString() ?? "",
                    Email = row["email"]?.ToString() ?? "",
                };
                result.Add(item);
            }

            return result;
        }

        public int Create(string first_name, string last_name, string email, string password, string offer_text)
        {
            return _studentTDG.Create(first_name, last_name,email,password);
        }

        public bool Update(int id)
        {
            var person = GetById(id);
            return _studentTDG.Update(person.Id, person.First_Name, person.Last_Name,person.Email);
        }

        public bool Delete(int id)
        {
            return _studentTDG.Delete(id);
        }
    }
}