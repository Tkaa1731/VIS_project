using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;

namespace Project_VIS.Database
{
    public class Table_Teacher : Table_PARENT
    {
        public Table_Teacher() : base(Table_TYPE._teacherTable, Table_TYPE._teacherId) { }

        public int Create(string first_name,string last_name,string email,string password)
        {
            SqlConnectionStringBuilder builder = DBConnector.GetBuilder();
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                var sb = new StringBuilder();
                sb.Clear();
                sb.Append($"INSERT INTO {_tableName} (first_name, last_name, email, password, last_visit, offer_active)");
                sb.Append("VALUES (@first_name, @last_name, @email, @password, CURRENT_TIMESTAMP,1);");
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

        public bool Update(int id, string first_name, string last_name, string email, bool active)
        {
            SqlConnectionStringBuilder builder = DBConnector.GetBuilder();
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                var sb = new StringBuilder();
                sb.Clear();
                sb.Append($"UPDATE {_tableName} SET first_name = @first_name, last_name = @last_name, email = @email, offer_active = @active WHERE teacher_id = @id");

                string sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@first_name", first_name);
                    command.Parameters.AddWithValue("@last_name", last_name);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@active", active);
                    command.ExecuteNonQuery();
                }
            }

            return true;
        }
    }
}