using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public class RolesDao
    {
        string constr = "Data Source=DESKTOP-9EC5BJ1;Initial Catalog=HR_DB;Integrated Security=True";
        public async Task<IEnumerable<Rolese>> FindAsncy()
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = "SELECT * from Roles";
                return await con.QueryAsync<Rolese>(sql);
            }
        }
        public async  Task<FenYe<Rolese>> FenYeAsncy(FenYe<Rolese> fy)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@pageSize", fy.pageSize);
                dp.Add("@keyName", "RolesID");
                dp.Add("@tableName", "Roles");
                dp.Add("@currentPage", fy.currentPage);
                dp.Add("@where", " 1=1");
                dp.Add("@rows", 0, DbType.Int32, ParameterDirection.Output);
                FenYe<Rolese> feye = new FenYe<Rolese>();
                feye.CList = await con.QueryAsync<Rolese>("proc_Fenye", dp, commandType: CommandType.StoredProcedure);
                int zts = dp.Get<int>("@rows");
                feye.Totalnumber = zts;
                feye.Totalpage = zts % fy.pageSize == 0 ? zts / fy.pageSize : zts / fy.pageSize + 1;
                return feye;
            }
        }
        //添加角色
        public async Task<int>  Add_RolesAsncy(Rolese r)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"INSERT INTO [dbo].[Roles] ([RolesName], [RolesInstruction], [RolesIf]) VALUES('{r.RolesName}','{r.RolesInstruction}',{r.RolesIf})";
                return await con.ExecuteAsync(sql);
            }
        }
        //查询 单条数据
        public async Task<Rolese> FindIDAsncy(string rid)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"SELECT * FROM [dbo].[Roles] WHERE RolesID='{rid}'";
                return await con.QueryFirstAsync<Rolese>(sql);
            }
        }
        //修改
        public async Task<int> UpdateAync(Rolese r)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@" update [dbo].[Roles]  set RolesName='{r.RolesName}' ,RolesInstruction='{r.RolesInstruction}' ,RolesIf={r.RolesIf} where RolesID={r.RolesID}";
                return await con.ExecuteAsync(sql);
            }
        }
        //删除 
        public async Task<int> DelAync(string id)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                //查询 这个角色 有没有真在使用
                string sql = $@" SELECT COUNT(0) FROM [dbo].[UserRoles] WHERE RolesID={id}";
                int dore = await con.QueryFirstAsync<int>(sql);
                if (dore != 0)
                {
                    return 0;
                }
                string sqld = $@"DELETE FROM  Roles WHERE RolesID={id}";
                dore = await con.ExecuteAsync(sqld);
                return dore;
            }
        }
    }
}
