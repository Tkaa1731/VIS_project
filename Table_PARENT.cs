using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Project_VIS.Database
{
    public abstract class Table_PARENT
    {
        protected readonly string _tableName;
        protected readonly string _tableId;
        protected Table_PARENT(string tableName, string tableId)
        {
            _tableName = tableName;
            _tableId = tableId;
        }

        public DataTable GetAll(string where)
        {
            if (where == null)
                where = " ";
            var query = $"select * from {_tableName} {where};";
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
        public bool Delete(int id)
        {
            SqlConnectionStringBuilder builder = DBConnector.GetBuilder();
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                var sb = new StringBuilder();
                sb.Clear();
                sb.Append($"DELETE FROM {_tableName} WHERE {_tableId} = {id}");

                string sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                   // command.Parameters.AddWithValue("@id", id);
                    var returnValue = command.ExecuteNonQuery();
                    if (returnValue != 1)
                        return false;
                }
                
            }

            return true;
        }
        public DataTable GetById(int id)
        {
            var query = $"select * from {_tableName} where {_tableId} = {id}";
            var result = new DataTable();
            var connString = DBConnector.GetBuilder().ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@id", id);
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
