using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;

namespace Project_VIS.Database
{
    public class Table_Teacher_Student : Table_PARENT
    {
        public Table_Teacher_Student() : base(Table_TYPE._teacher_studentTable, Table_TYPE._teacher_studentId) { }

        public int Create(int student_id, int teacher_id) /// usecase hlidani jestli relace uxistuje nebo neni uzavrena
        {
            SqlConnectionStringBuilder builder = DBConnector.GetBuilder();
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                var sb = new StringBuilder();
                sb.Clear();
                sb.Append($"INSERT INTO {_tableName} (student_id, teacher_id, active, start_date)");
                sb.Append($"VALUES ({student_id}, {teacher_id}, 1, CURRENT_TIMESTAMP);");
                sb.Append("SELECT CAST(scope_identity() AS int)");

                string sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    object modified = command.ExecuteScalar();
                    return (int)modified;
                }
            }
        }

        public bool Update(int student_id,int teacher_id, bool active)
        {
            string end_date = "CURRENT_TIMESTAMP";
            if (active) // obnovuje relaci
            {
                end_date = "NULL";
            }
            SqlConnectionStringBuilder builder = DBConnector.GetBuilder();
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                var sb = new StringBuilder();
                sb.Clear();
                sb.Append($"UPDATE {_tableName} SET active = {active}, end_date = {end_date} WHERE teacher_id = {teacher_id} AND student_id = {student_id}");

                string sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {


                    command.ExecuteNonQuery();
                }
            }

            return true;
        }
        public bool? CheckExist(int student_id, int teacher_id)
        {
            SqlConnectionStringBuilder builder = DBConnector.GetBuilder();
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                var sb = new StringBuilder();
                sb.Clear();
                sb.Append($"SELECT active FROM {_tableName} WHERE student_id = {student_id} AND teacher_id = {teacher_id}");

                string sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    object modified = command.ExecuteScalar();
                    if (modified is null)
                        return null;
                    return Convert.ToBoolean(modified);
                }
            }
        
        }
    }
}