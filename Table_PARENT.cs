using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Project_VIS.Database
{
    internal static class Table_TYPE
    {
        public static readonly string _teacherTable = "Teacher";
        public static readonly string _teacherId = "teacher_id";

        public static readonly string _studentTable = "Student";
        public static readonly string _studentId = "student_id";


    }
    public abstract class Table_PARENT
    {
        protected string _tableName { get; }
        protected string _tableId { get; }
        protected Table_PARENT(string tableName, string tableId)
        {
            _tableName = tableName;
            _tableId = tableId;
        }

        public DataTable GetAll()
        {
            // write down query
            var query = $"select * from {_tableName};";

            // objet for results
            var result = new DataTable();

            // get connection string 
            var connString = DBConnector.GetBuilder().ConnectionString;

            // Connect
            using (SqlConnection connection = new SqlConnection(connString))
            {
                // Open connection
                connection.Open();

                // prepare command
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // execute command
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // read data from result
                        result.Load(reader);
                    }
                }
            }
            return result;
        }
        public bool Delete(int id)
        {
            SqlConnectionStringBuilder builder = DBConnector.GetBuilder();
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                var sb = new StringBuilder();
                sb.Clear();
                sb.Append($"DELETE FROM {_tableName} WHERE {_tableId} = @id");

                string sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }

            return true;
        }
        public DataTable GetById(int id)
        {
            var query = $"select * from {_tableName} where {_tableId} = @id;";
            var result = new DataTable();
            var connString = DBConnector.GetBuilder().ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result.Load(reader);
                    }
                }
            }
            return result;
        }
    }
}
