using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;
using DapperExtensions;
using System.Configuration;
using System.Data.SQLite;
using DapperExtensions.Mapper;
using System.Reflection;
using DapperExtensions.Sql;

namespace ExamSys
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var conn = DB.GetConn();
            //var list = conn.Query<Archive>("select * from Archive");
            //GridView1.DataSource = list;
            //GridView1.DataBind();
            if (!IsPostBack)
                bindData();
           
        }
        void bindData()
        {
            //https://github.com/tmsmith/Dapper-Extensions/blob/master/DapperExtensions.Test/IntegrationTests/Sqlite/CrudFixture.cs
            IDatabase Db;
            string connectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            var config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new SqliteDialect());
            var sqlGenerator = new SqlGeneratorImpl(config);
            Db = new Database(connection, sqlGenerator);

            int pageIndex = AspNetPager1.CurrentPageIndex - 1;
            int pageSzie = AspNetPager1.PageSize;

            IList<ISort> sort = new List<ISort>
                                    {
                                        Predicates.Sort<Archive>(p => p.Id)
                                    };
            IEnumerable<Archive> List = Db.GetPage<Archive>(null, sort, pageIndex, pageSzie);

            int count = Db.Count<Archive>(null);
            AspNetPager1.RecordCount = count;


            rptList.DataSource = List;
            rptList.DataBind();
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            bindData();
        }
    }
}