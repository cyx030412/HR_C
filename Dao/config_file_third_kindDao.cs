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
    public class config_file_third_kindDao
    {
        string constr = "Data Source=DESKTOP-9EC5BJ1;Initial Catalog=HR_DB;Integrated Security=True";
        //查询所有数据
        public async Task<IEnumerable<config_file_third_kind>> FindAsync()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "SELECT * FROM  [dbo].[config_file_third_kind]";
                return await con.QueryAsync<config_file_third_kind>(sql);
            }
        }
        //查询单条数据
        public async Task<config_file_third_kind> FindIDAsync(string No)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"SELECT * FROM  [dbo].[config_file_third_kind] where third_kind_id='{No}'";
                return await con.QueryFirstAsync<config_file_third_kind>(sql);
            }
        }
        //添加
        public async Task<int> InsertAsyns(config_file_third_kind c)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                //I
                config_file_first_kindDao leve1 = new config_file_first_kindDao();
                config_file_first_kind cv = await leve1.FindIDAsync(c.first_kind_id);
                //II
                config_file_second_kindDao cvII = new config_file_second_kindDao();
                config_file_second_kind cvI = await cvII.FindIDAsync(c.second_kind_id);
                //no
                string Noe = "SELECT TOP 1  CASE    WHEN [third_kind_id] + 1 < 10 THEN '0' + CAST([third_kind_id] + 1 AS VARCHAR(2))  ELSE CAST([third_kind_id] + 1 AS VARCHAR(2))  END AS FormattedValue FROM [dbo].[config_file_third_kind] ORDER BY ftk_id DESC";
                string no = con.QueryFirst<string>(Noe);
                string sql = "INSERT INTO [dbo].[config_file_third_kind]( [first_kind_id], [first_kind_name], [second_kind_id], [second_kind_name], [third_kind_id], [third_kind_name], [third_kind_sale_id], [third_kind_is_retail])";
                sql += $@"VALUES('{c.first_kind_id}','{cv.first_kind_name}','{c.second_kind_id}','{cvI.second_kind_name}','{no}','{c.third_kind_name}','{c.third_kind_sale_id}','{c.third_kind_is_retail}')";
                return await con.ExecuteAsync(sql);
            }
        }
        //删除
        public async Task<int> DeleteAsync(string no)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"DELETE FROM config_file_third_kind WHERE third_kind_id='{no}'";
                return await con.ExecuteAsync(sql);
            }
        }
             //修改
       public async Task<int> UpdateAsync(config_file_third_kind c)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"UPDATE [dbo].[config_file_third_kind] SET third_kind_is_retail='{c.third_kind_is_retail}',third_kind_sale_id='{c.third_kind_sale_id}' WHERE third_kind_id='{c.third_kind_id}'";
                return await con.ExecuteAsync(sql);
            }
       }
        //查询一二三级级联

        public async Task<List<Cascade>> CascadeAsync()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "SELECT * FROM [dbo].[config_file_first_kind]";
                IEnumerable<config_file_first_kind> leve1 = await con.QueryAsync<config_file_first_kind>(sql);
                List<Cascade> list = new List<Cascade>();
                foreach (var item in leve1)
                {
                    Cascade c = new Cascade()
                    {
                        value = item.first_kind_id,
                        label = item.first_kind_name,
                        children = await Cacade2Async(item.first_kind_id)
                    };
                    list.Add(c);
                }
                return list;
            }
        }
        //2级查询
        public async Task<List<Cascade>> Cacade2Async(string pid)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql2 = $@"SELECT * FROM  [dbo].[config_file_second_kind] where [first_kind_id]='{pid}'";
                IEnumerable<config_file_second_kind> leve1 = await con.QueryAsync<config_file_second_kind>(sql2);
                List<Cascade> list = new List<Cascade>();
                foreach (var item in leve1)
                {

                    Cascade c = new Cascade()
                    {
                        value = item.second_kind_id,
                        label = item.second_kind_name,
                        children = await Cacade3Async(item.second_kind_id)
                    };
                    list.Add(c);
                }
                return list;
            }
        }
        //III级查询
        public async Task<List<Cascade>> Cacade3Async(string pid)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql2 = $@"SELECT * FROM  [dbo].[config_file_third_kind] WHERE second_kind_id ='{pid}'";
                IEnumerable<config_file_third_kind> leve1 = await con.QueryAsync<config_file_third_kind>(sql2);
                List<Cascade> list = new List<Cascade>();
                foreach (var item in leve1)
                {

                    Cascade c = new Cascade()
                    {
                        value = item.third_kind_id,
                        label = item.third_kind_name,
                    };
                    list.Add(c);
                }
                return list;
            }
        }
        //查询全部
        public async Task<IEnumerable<SalaSum>> FindsumnAsync()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "SELECT  f.third_kind_id as id, f.third_kind_name as name,  (SELECT SUM(salary_sum) FROM human_file WHERE third_kind_id = f.third_kind_id and PayrollStatus = 0) AS money,    (SELECT COUNT(0) FROM human_file WHERE third_kind_id = f.third_kind_id and PayrollStatus = 0) AS sunm FROM config_file_third_kind f";
                return await con.QueryAsync<SalaSum>(sql);
            }
        }




    }
}
