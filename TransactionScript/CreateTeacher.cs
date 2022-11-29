using Project_VIS.Database;

namespace Project_VIS.Domain.TransactionScript
{
    public class CreateTeacher
    {
        private readonly Table_Teacher _teacherTDG;
        public CreateTeacher()
        {
            _teacherTDG = new Table_Teacher();
        }

        public int Execute(string first_name, string last_name, string email, string password)
        {
            return _teacherTDG.Create(first_name, last_name, email, password);
        }
    }
}