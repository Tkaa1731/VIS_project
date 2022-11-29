using System.Data.SqlClient;

namespace Project_VIS.Database
{
    public static class DBConnector
    {
        public static SqlConnectionStringBuilder GetBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"dbsys.cs.vsb.cz\STUDENT";
            builder.UserID = "kal0300";              
            builder.Password = "nXKG4e7sCQ6Q8u7m";   
            builder.InitialCatalog = "kal0300";
            return builder;
        }
    }
}