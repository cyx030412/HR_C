using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public class usersDao
    {
        string constr = "Data Source=DESKTOP-9EC5BJ1;Initial Catalog=HR_DB;Integrated Security=True";

        //登录
        public async Task<int> LoginAsync(users u)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"SELECT * FROM users where u_name ='{u.u_name}' and u_password='{u.u_password}'";
                users us;
                try
                {
                     us = await con.QueryFirstAsync<users>(sql);
                }
                catch (Exception)
                {

                    return 0;
                }
                if (us != null)
                {
                   return us.u_id;
                }
                else
                {
                    return 0;
                } 
            }
        }
        //查询
        public async Task<IEnumerable<users>> FindAsyns()
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = "SELECT * FROM users";
                return await con.QueryAsync<users>(sql);  
            }
        }
        //查询用户信息（分页，两表）
        public async Task<FenYe<users>> FindFyAsyns(FenYe<users> fy)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@pageSize", fy.pageSize);
                dp.Add("@keyName", "u_id");
                dp.Add("@tableName", "users");
                dp.Add("@currentPage", fy.currentPage);
                dp.Add("@condition", " 1=1");
                dp.Add("@JointI", " INNER JOIN  [dbo].[UserRoles] ur on a.u_id=ur.UserID inner join [dbo].[Roles]  r on ur.RolesID=r.RolesID");
                dp.Add("@field", "  a.*,r.RolesName as RolesName");
                dp.Add("@rows", 0, DbType.Int32, ParameterDirection.Output);
                FenYe<users> feye = new FenYe<users>();
                feye.CList = await con.QueryAsync<users>("proc_Fenye_lc", dp, commandType: CommandType.StoredProcedure);
                int zts = dp.Get<int>("@rows");
                feye.Totalnumber = zts;
                feye.Totalpage = zts % fy.pageSize == 0 ? zts / fy.pageSize : zts / fy.pageSize + 1;
                return feye;
            }
        }
        //添加用户
        public async Task<int> AddAsync(users u)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"INSERT INTO  [dbo].[users] ([u_name], [u_true_name], [u_password]) VALUES('{u.u_name}','{u.u_true_name}','{u.u_password}')";
                int dore= await con.ExecuteAsync(sql);
                if (dore == 0)
                {
                    return 0;
                }
                sql = "SELECT TOp 1 u_id from  [dbo].[users] order by u_id desc";
                string uid = await con.QueryFirstAsync<string>(sql);
                sql = $@"INSERT [dbo].[UserRoles] ([UserID], [RolesID]) VALUES('{uid}','{u.RolesName}')";
                dore = await con.ExecuteAsync(sql);
                if (dore == 0)
                {
                    return 0;
                }
                return 1;
            }
        }
        //修改用户
        public async Task<int> UpdateAsync(users u)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"update users set u_name='{u.u_name}',u_password='{u.u_password}',u_true_name='{u.u_true_name}' where u_id={u.u_id}";
                int dore = await con.ExecuteAsync(sql);
                if (dore == 0)
                {
                    return 0;
                }
                sql = $@"UPDATE UserRoles SET RolesID ='{u.RolesName}' WHERE UserID='{u.u_id}'";
                dore = await con.ExecuteAsync(sql);
                if (dore == 0)
                {
                    return 0;
                }
                return 1;
            }
        }
        //删除用户
        public async Task<int> DelAsync(string uid)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"DELETE FROM users where u_id='{uid}'";
                int  dore= await con.ExecuteAsync(sql);
                if (dore == 0)
                {
                    return 0;
                }
                sql = $@"DELETE FROM UserRoles WHERE UserID='{uid}'";
                dore = await con.ExecuteAsync(sql);
                if (dore == 0)
                {
                    return 0;
                }
                return 1;
            }
        }
        //查询单条数据
        public async Task<users> FindIdAsync(string uid)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"SELECT U.*,r.RolesID as RolesName FROM [dbo].[users] U INNER JOIN [dbo].[UserRoles] r ON U.u_id=r.UserID  WHERE U.u_id='{uid}'";
                return await con.QueryFirstAsync<users>(sql);
            }
        }


    }
}
