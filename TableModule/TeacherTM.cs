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
                throw new EntityNotFoundExeption("Entity not found");
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

        public IEnumerable<TeacherDTO> GetAll(string where)
        {
            var table = _teacherTDG.GetAll(where);
            if(table.Rows.Count == 0)
                throw new EntityNotFoundExeption("Entity not found");
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

        public int Create(string first_name, string last_name, string email, string password, string offer_text)
        {
            return _teacherTDG.Create(first_name, last_name,email,password, offer_text);
        }

        public bool Update(TeacherDTO person)
        {
            return _teacherTDG.Update(person.Id, person.First_Name, person.Last_Name, person.Active, person.Offer_Text);
        }

        public bool Delete(int id)
        {
            return _teacherTDG.Delete(id);
        }
    }
}