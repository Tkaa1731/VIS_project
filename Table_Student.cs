using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;

namespace Project_VIS.Database
{
    public class Table_Student : Table_PARENT
    {
        public Table_Student() : base(Table_TYPE._studentTable, Table_TYPE._studentId) { }

        public DataTable GetStudentsOfTeacher(int teacher_id)
        {
            var query = $"SELECT s.* FROM {_tableName} s JOIN Teacher_Student ts ON ts.student_id = s.student_id WHERE ts.teacher_id = {teacher_id};";
            var result = new DataTable();
            var connString = DBConnector.GetBuilder().ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result.Load(reader);
                    }
                }
            }
            return result;
        }
        public int Create(string first_name,string last_name,string email,string password)
        {
            SqlConnectionStringBuilder builder = DBConnector.GetBuilder();
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                var sb = new StringBuilder();
                sb.Clear();
                sb.Append($"INSERT INTO {_tableName} (first_name, last_name, email, password, last_visit)");
                sb.Append("VALUES (@first_name, @last_name, @email, @password, CURRENT_TIMESTAMP);");
                sb.Append("SELECT CAST(scope_identity() AS int)");

                string sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@first_name", first_name);
                    command.Parameters.AddWithValue("@last_name", last_name);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);

                    object modified = command.ExecuteScalar();
                    return (int)modified;
                }
            }
        }

        public bool Update(int id, string first_name, string last_name, string email)
        {
            SqlConnectionStringBuilder builder = DBConnector.GetBuilder();
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                var sb = new StringBuilder();
                sb.Clear();
                sb.Append($"UPDATE {_tableName} SET first_name = @first_name, last_name = @last_name, email = @email WHERE student_id = @id");

                string sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@first_name", first_name);
                    command.Parameters.AddWithValue("@last_name", last_name);
                    command.Parameters.AddWithValue("@email", email);

                    command.ExecuteNonQuery();
                }
            }

            return true;
        }
    }
}