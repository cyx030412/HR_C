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
    public class major_changeDao
    {
        string constr = "Data Source=DESKTOP-9EC5BJ1;Initial Catalog=HR_DB;Integrated Security=True";
        //添加
        public async Task<int> AddAsync(major_change mc)
        {
            using ( SqlConnection con = new SqlConnection(constr))
            {
                string sql = " INSERT INTO major_change( [first_kind_id], [first_kind_name], [second_kind_id], [second_kind_name], [third_kind_id], [third_kind_name], [major_kind_id], ";
                sql += " [major_kind_name], [major_id], [major_name], [new_first_kind_id], [new_first_kind_name], [new_second_kind_id], [new_second_kind_name], [new_third_kind_id], [new_third_kind_name]";
                sql += "  ,[new_major_kind_id], [new_major_kind_name], [new_major_id], [new_major_name], [human_id], [human_name], [salary_standard_id], [salary_standard_name], [salary_sum],";
                sql += " [new_salary_standard_id], [new_salary_standard_name], [new_salary_sum], [change_reason], [register], [regist_time],check_status)";
                sql += $@" values('{mc.first_kind_id}','{mc.first_kind_name}','{mc.second_kind_id}','{mc.second_kind_name}','{mc.third_kind_id}','{mc.third_kind_name}','{mc.major_kind_id}','{mc.major_kind_name}','{mc.major_id}','{mc.major_name}','{mc.new_first_kind_id}','{mc.new_first_kind_name}','{mc.new_second_kind_id}','{mc.new_second_kind_name
                    }','{mc.new_third_kind_id}','{mc.new_third_kind_name}','{mc.new_major_kind_id}','{mc.new_major_kind_name}','{mc.new_major_id}','{mc.new_major_name}','{mc.human_id}','{mc.human_name}','{mc.salary_standard_id}','{mc.salary_standard_name}',{mc.salary_sum},'{mc.new_salary_standard_id}','{mc.new_salary_standard_name}',{mc.new_salary_sum},'{mc.change_reason}','{mc.register}','{mc.regist_time}',0)";

                return await con.ExecuteAsync(sql);
            }

        }
        //查询待复核的数据
        public async Task<FenYe<major_change>> FinddfhAsync(FenYe<major_change> fy)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = "SELECT * from [dbo].[major_change] where check_status =0";
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@pageSize", fy.pageSize);
                dp.Add("@keyName", "mch_id");
                dp.Add("@tableName", "major_change");
                dp.Add("@currentPage", fy.currentPage);
                dp.Add("@where", " check_status =0");
                dp.Add("@rows", 0, DbType.Int32, ParameterDirection.Output);
                FenYe<major_change> feye = new FenYe<major_change>();
                feye.CList = await con.QueryAsync<major_change>("proc_Fenye", dp, commandType: CommandType.StoredProcedure);
                int zts = dp.Get<int>("@rows");
                feye.Totalnumber = zts;
                feye.Totalpage = zts % fy.pageSize == 0 ? zts / fy.pageSize : zts / fy.pageSize + 1;
                return feye;
            }
        }
        //查询 单条 数据
        public  async Task<major_change> FindIdAsync(string id)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"SELECT * from  [dbo].[major_change] where mch_id ='{id}'";
                return await con.QueryFirstAsync<major_change>(sql);
            }
        }
        //调用通过
        public async Task<int> UpdateAsync(major_change mc)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"UPDATE [dbo].[major_change] SET new_first_kind_id='{mc.new_first_kind_id}' , new_first_kind_name='{mc.new_first_kind_name}' , new_second_kind_id ='{mc.new_second_kind_id}' ,new_second_kind_name='{mc.new_second_kind_name}',new_third_kind_id='{mc.new_third_kind_id}',new_third_kind_name='{mc.new_third_kind_name}' ,new_major_kind_id='{mc.new_major_kind_id}',new_major_kind_name='{mc.new_major_kind_name}',new_major_id ='{mc.new_major_id}',new_major_name='{mc.new_major_name}',new_salary_standard_id='{mc.new_salary_standard_id}',new_salary_standard_name='{mc.new_salary_standard_name}',new_salary_sum='{mc.new_salary_sum}',check_reason='{mc.check_reason}',check_status=1,checker='{mc.checker}',check_time='{mc.check_time}' where mch_id='{mc.mch_id}'";
                int dore = await con.ExecuteAsync(sql);
                if (dore == 0)
                {
                    return 0;
                }
                string sql2 = $@"UPDATE [dbo].[human_file] SET first_kind_id='{mc.new_first_kind_id}',first_kind_name='{mc.new_first_kind_name}',second_kind_id='{mc.new_second_kind_id}',second_kind_name='{mc.new_second_kind_name}',third_kind_id='{mc.new_third_kind_id}',third_kind_name='{mc.new_third_kind_name}',human_major_kind_id='{mc.new_major_kind_id}',human_major_kind_name='{mc.new_major_kind_name}',human_major_id='{mc.new_major_id}',hunma_major_name='{mc.new_major_name}',salary_standard_id='{mc.new_salary_standard_id}',salary_standard_name='{mc.new_salary_standard_name}',salary_sum='{mc.new_salary_sum}',major_change_amount=major_change_amount+1 where human_id='{mc.human_id}'";
                dore = await con.ExecuteAsync(sql2);
                if (dore== 0)
                {
                    return 0;
                }
                return 1;
            }
        }
        //调动不通过
        public async Task<int> UpdateNoAync(major_change mc)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"UPDATE [dbo].[major_change] SET check_reason ='{mc.check_reason}' ,  check_status =3 ,checker='{mc.checker}',check_time='{mc.check_time}' WHERE mch_id='{mc.mch_id}'";
                return await con.ExecuteAsync(sql);
            }
        }
        //按条件查询调档记录

        public async Task<IEnumerable<major_change>> FindWhereAsync(string where)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"SELECT * from [dbo].[major_change] WHERE {where}";
                return await con.QueryAsync<major_change>(sql);
            }
        }
        //使用 编号查询 单条数据
        public async Task<major_change> FindIDAsync(string id)
        {
            using(SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"SELECT * from [dbo].[major_change] WHERE mch_id='{id}'";
                return await con.QueryFirstAsync<major_change>(sql);
            }
        }

    }
}
