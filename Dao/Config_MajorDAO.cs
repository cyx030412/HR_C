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
    //职位设置查询
    public class Config_MajorDAO
    {
        string ConStr = "Data Source=DESKTOP-9EC5BJ1;Initial Catalog=HR_DB;Integrated Security=True";

        public async Task<IEnumerable<Config_Major>> ZWSelect()
        {
            using (SqlConnection connection=new SqlConnection(ConStr))
            {
                string sql = "select * from [dbo].[config_major]";
                return await connection.QueryAsync<Config_Major>(sql);
            }
        }
        //职位设置删除
        public async Task<int> ZWDelete(int major_kind_id)
        {
            using (SqlConnection connection=new SqlConnection(ConStr))
            {
                string sql = $"delete from [dbo].[config_major] where [mak_id]='{major_kind_id}'";
                return await connection.ExecuteAsync(sql);
            }
        }
        //职位设置新增
        public async Task<int> ZWSZInsert(Config_Major config_Major)
        {
            using (SqlConnection connection=new SqlConnection(ConStr))
            {
                string sql = $"insert into [dbo].[config_major]([major_kind_id], [major_kind_name], [major_id], [major_name])values('{config_Major.major_kind_id}','{config_Major.major_kind_name}','{config_Major.major_id}','{config_Major.major_name}')";
                return await connection.ExecuteAsync(sql);
            }
        }
        //根据职称分类查询职称
        public async Task<IEnumerable<Config_Major>> FindMidAsync(string Mid)
        {
            using (SqlConnection con=new SqlConnection(ConStr))
            {
                string sql = $@"SELECT * FROM  [dbo].[config_major] WHERE major_kind_id='{Mid}'";
                return await con.QueryAsync<Config_Major>(sql);
            }
        }
        //职位分类联级
        public async Task<List<Cascade>> CascadeAsync()
        {
            using (SqlConnection con = new SqlConnection(ConStr))
            {
                string sql = "SELECT * FROM   [dbo].[config_major_kind]";
                IEnumerable<Config_Major_Kind> leve1 = await con.QueryAsync<Config_Major_Kind>(sql);
                List<Cascade> list = new List<Cascade>();
                foreach (var item in leve1)
                {
                    Cascade c = new Cascade()
                    {
                        value = item.major_kind_id,
                        label = item.major_kind_name,
                        children = await Cacade3Async(item.major_kind_id)
                    };
                    list.Add(c);
                }
                return list;
            }
        }
        public async Task<List<Cascade>> Cacade3Async(string pid)
        {
            using (SqlConnection con = new SqlConnection(ConStr))
            {
                string sql2 = $@" select * from  [dbo].[config_major] WHERE major_kind_id ='{pid}'";
                IEnumerable<Config_Major> leve1 = await con.QueryAsync<Config_Major>(sql2);
                List<Cascade> list = new List<Cascade>();
                foreach (var item in leve1)
                {

                    Cascade c = new Cascade()
                    {
                        value = item.major_id,
                        label = item.major_name,
                    };
                    list.Add(c);
                }
                return list;
            }
        }
    }
}
