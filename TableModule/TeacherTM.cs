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
{
    public class TeacherTM
    {
        private readonly Table_Teacher _teacherTDG;
        public TeacherTM()
        {
            _teacherTDG = new Table_Teacher();
        }

        public TeacherDTO GetById(int id)
        {
            var table = _teacherTDG.GetById(id);

            if (table.Rows.Count == 0)
            {
                throw new EntityNotFoundExeption();
            }

            var row = table.Rows[0];
            return new TeacherDTO
            {
                Id = Convert.ToInt32(row["teacher_id"]),
                First_Name = row["first_name"]?.ToString() ?? "",
                Last_Name = row["last_name"]?.ToString() ?? "",
                Email = row["email"]?.ToString() ?? "",
                Active = Convert.ToBoolean(row["offer_active"]),
                Offer_Text = row["offer_text"]?.ToString() ?? ""
            };
        }

        public IEnumerable<TeacherDTO> GetAll()
        {
            var table = _teacherTDG.GetAll();
            List<TeacherDTO> result = new();

            foreach (DataRow row in table.Rows)
            {
                var item = new TeacherDTO
                {
                    Id = Convert.ToInt32(row["teacher_id"]),
                    First_Name = row["first_name"]?.ToString() ?? "",
                    Last_Name = row["last_name"]?.ToString() ?? "",
                    Email = row["email"]?.ToString() ?? "",
                    Active = Convert.ToBoolean(row["offer_active"]),
                    Offer_Text = row["offer_text"]?.ToString() ?? ""
                };
                result.Add(item);
            }

            return result;
        }

        public int Create(string first_name, string last_name, string email, string password)// update
        {
            return _teacherTDG.Create(first_name, last_name,email,password);
        }

        public bool Update(int id, string first_name, string last_name, string email, bool active)// update 
        {
            return _teacherTDG.Update(id, first_name, last_name, email ,active);
        }

        public bool Delete(int id)
        {
            return _teacherTDG.Delete(id);
        }
    }
}