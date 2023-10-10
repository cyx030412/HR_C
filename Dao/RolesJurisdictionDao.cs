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
    public class RolesJurisdictionDao
    {
        string constr = "Data Source=DESKTOP-9EC5BJ1;Initial Catalog=HR_DB;Integrated Security=True";

        //添加
        public async Task<int> AddAscny(string rid,string jid)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"INSERT INTO [dbo].[RolesJurisdiction] ([RolesID], [JuriID]) VALUES('{rid}','{jid}')";
                return await con.ExecuteAsync(sql);
            }
        }
        //添加
        public async Task<int> DelAscny(string rid, string jid)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"DELETE FROM [dbo].[RolesJurisdiction]  WHERE [RolesID]='{rid}' AND [JuriID] ='{jid}'";
                return await con.ExecuteAsync(sql);
            }
        }

        public async Task<List<Menu>> GetList(int userID)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                //根据用户id查角色id
                string sql1 = $"select * from [dbo].[userRoles] where userID={userID}";
                UserRoles userRoles = new UserRoles();
                userRoles = con.QueryFirst<UserRoles>(sql1);

                string sql = $"select q.* from [dbo].[jurisdiction] q inner join [dbo].[rolesJurisdiction] qr on q.juriID=qr.juriID where qr.rolesID='{userRoles.RolesID}'";
                List<jurisdiction> quans = con.Query<jurisdiction>(sql).ToList();
                List<Menu> per = GetPermissData1(quans, 0);
                return per;
            }
        }
        //递归调用权限数据
        private List<Menu> GetPermissData1(List<jurisdiction> list, int pid)
        {
            List<Menu> menus = new List<Menu>();
            List<jurisdiction> plist = list.Where(e => e.jurPid == pid).ToList();
            foreach (jurisdiction item in plist)
            {
                Menu menus1 = new Menu()
                {
                    id = item.juriID,
                    label = item.jurName,
                    Url = item.jurAddress,
                    children = GetPermissData1(list, item.juriID)
                };
                menus.Add(menus1);
            }
            return menus;
        }
    }
}
