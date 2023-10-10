﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using HR.Models;
using Model;
namespace Dao
{
    public class Config_Public_CharDAO
    {
        string ConStr = "Data Source=DESKTOP-9EC5BJ1;Initial Catalog=HR_DB;Integrated Security=True";
        //公共属性设置查询
        public async Task<IEnumerable<Config_Public_Char>> CPCSelect()
        {
            using (SqlConnection connection=new SqlConnection(ConStr))
            {
                string sql = "select * from [dbo].[config_public_char]";
                return await connection.QueryAsync<Config_Public_Char>(sql);
            }
        }
        //公共属性删除
        public async Task<int> GGDelete(int pbc_id)
        {
            using (SqlConnection connection=new SqlConnection(ConStr))
            {
                string sql = $"delete from [dbo].[config_public_char] where [pbc_id]='{pbc_id}'";
                return await connection.ExecuteAsync(sql);
            }
        }
        //公共属性新增
        public async Task<int> GGInsert(Config_Public_Char c)
        {
            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                string sql = $"insert into [dbo].[config_public_char]([attribute_kind],[attribute_name])values('{c.attribute_kind}','{c.attribute_name}')";
                return await connection.ExecuteAsync(sql);
            }
        }
        //职称设置
        public async Task<IEnumerable<Config_Public_Char>> ZCSelect()
        {
            using (SqlConnection connection=new SqlConnection(ConStr))
            {
                string sql = "select * from [dbo].[config_public_char] where [attribute_kind]='职称'";
                return await connection.QueryAsync<Config_Public_Char>(sql);
            }
        }
        //薪酬设置
        public async Task<IEnumerable<Config_Public_Char>> XCSelect()
        {
            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                string sql = "SELECT * FROM  [dbo].[config_public_char]  WHERE attribute_kind ='薪酬设置'";
                return await connection.QueryAsync<Config_Public_Char>(sql);
            }
        }

        //添加薪酬
        public async Task<int> XCAdd(string name)
        {
            using (SqlConnection con=new SqlConnection(ConStr))
            {
                string sql = $@"INSERT INTO [dbo].[config_public_char]([attribute_kind], [attribute_name]) VALUES('薪酬设置','{name}')";
                return await con.ExecuteAsync(sql); 
            }
        }
        //薪酬删除
        public async Task<int> XCDelect(string no)
        {
            using (SqlConnection con = new SqlConnection(ConStr))
            {
                string sql = $@"DELETE FROM  [dbo].[config_public_char] WHERE pbc_id='{no}'";
                return await con.ExecuteAsync(sql);
            }
         }

        //根据 kind 查询不同的公共属性

        public async Task<IEnumerable<Config_Public_Char>> FindKind_cyxAsync()
        {
            using (SqlConnection con=new SqlConnection(ConStr))
            {
                string sql = $@"SELECT * FROM [dbo].[config_public_char]";
                return await con.QueryAsync<Config_Public_Char>(sql);
            }
        }
    }
}
