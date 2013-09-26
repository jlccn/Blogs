using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Reflection;
using System.Data.SQLite;
using DapperExtensions;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;


namespace Blogs.DAL
{
    public class DBHelper
    {
        //public static readonly string sqliteConnectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;

      
        //public static SQLiteConnection GetConn()
        //{
        //    var conn = new SQLiteConnection(sqliteConnectionString);
        //    conn.Open();
        //    return conn;
        //}

        public static IDatabase GetDatabase(string sqliteConnectionString)
        {
            IDatabase Db;
            SQLiteConnection connection = new SQLiteConnection(sqliteConnectionString);
            var config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new SqliteDialect());
            var sqlGenerator = new SqlGeneratorImpl(config);
            Db = new Database(connection, sqlGenerator);
            return Db;
        }
    }
}