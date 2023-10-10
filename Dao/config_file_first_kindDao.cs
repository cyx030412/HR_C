﻿using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public class config_file_first_kindDao
    {
        string constr = "Data Source=DESKTOP-9EC5BJ1;Initial Catalog=HR_DB;Integrated Security=True";

        //查询全部
        public async Task<IEnumerable<config_file_first_kind>> FindAsync()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "SELECT * FROM  [dbo].[config_file_first_kind]";
                return await con.QueryAsync<config_file_first_kind>(sql);
            }
         }
        //添加
        public async Task<int> SelectAsync(config_file_first_kind c)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                //查询数量得出编号
                string Noe = "SELECT TOP 1 \r\n    CASE \r\n        WHEN [first_kind_id] + 1 < 10 THEN '0' + CAST([first_kind_id] + 1 AS VARCHAR(2))\r\n        ELSE CAST([first_kind_id] + 1 AS VARCHAR(2))\r\n    END AS FormattedValue\r\nFROM [dbo].[config_file_first_kind] \r\nORDER BY ffk_id DESC;";
                string no = con.QueryFirst<string>(Noe);
                string sql = $@"INSERT INTO  [dbo].[config_file_first_kind] ( first_kind_id,[first_kind_name], [first_kind_salary_id], [first_kind_sale_id]) VALUES('{no}','{c.first_kind_name}','{c.first_kind_salary_id}','{c.first_kind_sale_id}')";
                return await con.ExecuteAsync(sql);
            }
        }
        //删除
        public async Task<int> DelectAsync(string Kid)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"DELETE FROM [dbo].[config_file_first_kind] WHERE first_kind_id='{Kid}'";
                return await con.ExecuteAsync(sql);
            }
        }
        //查询一条数据
        public async Task<config_file_first_kind> FindIDAsync(string id)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"SELECT * FROM  [dbo].[config_file_first_kind] WHERE first_kind_id='{id}'";
                return await con.QueryFirstAsync<config_file_first_kind>(sql);
            }
        }
        //查询一条修改
        public async Task<int> UpdateAsync(config_file_first_kind c)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"UPDATE [dbo].[config_file_first_kind] SET first_kind_name='{c.first_kind_name}',first_kind_salary_id='{c.first_kind_salary_id}',first_kind_sale_id='{c.first_kind_sale_id}' WHERE first_kind_id='{c.first_kind_id}'";
                return await con.ExecuteAsync(sql);
            }
        }
        //查询编号和名称
        public async Task<IEnumerable<config_file_first_kind>> FindName()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "SELECT first_kind_id,first_kind_name FROM [dbo].[config_file_first_kind]";
                return await con.QueryAsync<config_file_first_kind>(sql);
            }
        }
        //查询人力资源表一级人数

        //查询全部
        public async Task<IEnumerable<SalaSum>> FindsumnAsync()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "SELECT  f.first_kind_id as id, f.first_kind_name as name, (SELECT SUM(salary_sum) FROM human_file WHERE first_kind_id = f.first_kind_id and second_kind_id is null and PayrollStatus = 0) AS money, (SELECT COUNT(0) FROM human_file WHERE first_kind_id = f.first_kind_id and second_kind_id is null and PayrollStatus = 0) AS sunm FROM [dbo].[config_file_first_kind] f";
                return await con.QueryAsync<SalaSum>(sql);
            }
        }
    }
}
