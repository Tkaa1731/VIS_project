using Project_VIS.Database;
using Project_VIS.Domain.DTO;
//using Canteen.DataAccess;
using System.Data;

namespace Project_VIS.Domain.TransactionScript
{
    public class TeachersList
    {
        private readonly Table_Teacher _table_teacher;
        public TeachersList()
        {
            _table_teacher = new Table_Teacher();
        }

        public IEnumerable<TeacherDTO> Execute()
        {
            var table = _table_teacher.GetAll();
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
    }
}