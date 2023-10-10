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
    //职位分类查询
    public class Config_Major_KindDAO
    {
        string ConStr = "Data Source=DESKTOP-9EC5BJ1;Initial Catalog=HR_DB;Integrated Security=True";

        public async Task<IEnumerable<Config_Major_Kind>> ZWFLSelect()
        {
            using (SqlConnection connection=new SqlConnection(ConStr))
            {
                string sql = " select * from [dbo].[config_major_kind]";
                return await connection.QueryAsync<Config_Major_Kind>(sql);
            }
        }

        //职位分类删除

        public async Task<int> ZWFLDelete(int mfk_id)
        {
            using (SqlConnection connection=new SqlConnection(ConStr))
            {
                string sql = $"delete from [dbo].[config_major_kind] where [mfk_id]='{mfk_id}'";
                return await connection.ExecuteAsync(sql);
            }
        }
        //职位分类新增
        public async Task<int> Config_major_kindInsertAsync(Config_Major_Kind c)
        {
            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                string sql = $"insert into [dbo].[config_major_kind]([major_kind_id],[major_kind_name])values((SELECT TOP 1  CASE  WHEN  + 1 < 10 THEN '0' + CAST([major_kind_id] + 1 AS VARCHAR(2)) ELSE CAST([major_kind_id] + 1 AS VARCHAR(2))  END AS FormattedValue FROM [dbo].[config_major_kind] ORDER BY [major_kind_id] DESC),'{c.major_kind_name}')";
                return await connection.ExecuteAsync(sql);
            }
        }


    }
}
