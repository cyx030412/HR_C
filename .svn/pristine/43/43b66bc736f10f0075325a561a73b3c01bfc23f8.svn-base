using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public class salary_standard_detailsDao
    {

        string constr = "Data Source=DESKTOP-9EC5BJ1;Initial Catalog=HR_DB;Integrated Security=True";
        //使用ssid 查询 指定 数据
        public async Task<IEnumerable<salary_standard_details>> FindIDAsync(string Nostr)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"SELECT * FROM  [dbo].[salary_standard_details] WHERE standard_id='{Nostr}'";
                return await con.QueryAsync<salary_standard_details>(sql);
            }
        }
    }
}
