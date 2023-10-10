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
    
    public class HumanResourcesDao
    {
        string constr = "Data Source=DESKTOP-9EC5BJ1;Initial Catalog=HR_DB;Integrated Security=True";
        public async Task<IEnumerable<EngageResume>> Find()
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = "   SELECT * FROM engage_resume WHERE pass_passComment='通过' and pass_check_status = '1'";
                return await  con.QueryAsync<EngageResume> (sql);
            }
        }
        public async Task<EngageResume> FindID(string id)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"select * from  [dbo].[engage_resume] where res_id = '{id}'";
                return await con.QueryFirstAsync<EngageResume>(sql);
            }
        }

    }
}
