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
    public class jurisdictionDao
    {
        string constr = "Data Source=DESKTOP-9EC5BJ1;Initial Catalog=HR_DB;Integrated Security=True";
        public async Task<IEnumerable<Menu>> GetTherrs()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "SELECT * FROM  [dbo].[jurisdiction]";
                IEnumerable<jurisdiction> list =await con.QueryAsync<jurisdiction>(sql);
                IEnumerable<Menu> data =await GetTreeData(list, 0);
                return data;
            }
        }

        private async Task<IEnumerable<Menu>> GetTreeData(IEnumerable<jurisdiction> list, int pid)
        {
            List<Menu> trees = new List<Menu>();
            List<jurisdiction> plist = list.Where(e => e.jurPid == pid).ToList();
            foreach (jurisdiction item in plist)
            {
                Menu trees1 = new Menu()
                {
                    id = item.juriID,
                    label = item.jurName,
                    pid = item.jurPid,
                    children =await GetTreeData(list,item.juriID)
                };
                trees.Add(trees1);
            }
            return trees;
        }
        //查询 已存在的
        public async Task<IEnumerable<RolesJurisdiction>> FindRJAsync(string id)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"SELECT * FROM [dbo].[RolesJurisdiction] WHERE RolesID='{id}'";
                return await con.QueryAsync< RolesJurisdiction > (sql);
            }
        }
        //
    }
}
