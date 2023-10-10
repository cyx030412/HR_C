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
    public class salary_grant_detailsDao
    {
        string constr = "Data Source=DESKTOP-9EC5BJ1;Initial Catalog=HR_DB;Integrated Security=True";
        public async Task<int> FindAsync(List<salary_grant_details> sd,salary_grant s)
        {
            using ( SqlConnection con = new SqlConnection(constr))
            {
                string sqlsg = "INSERT INTO [dbo].[salary_grant](salary_grant_id,SalaryStandardId,first_kind_id,first_kind_name,second_kind_id,second_kind_name,third_kind_id,third_kind_name,human_amount,salary_standard_sum,salary_paid_sum,register,regist_time,check_status)";
                sqlsg += $@" values('{s.salary_grant_id}','{s.SalaryStandardId}','{s.first_kind_id}','{s.first_kind_name}','{s.second_kind_id}','{s.second_kind_name}','{s.third_kind_id}','{s.third_kind_name}',{s.human_amount},'{s.salary_standard_sum}','{s.salary_paid_sum}','{s.register}','{s.regist_time}',0)";
                int doressg = await con.ExecuteAsync(sqlsg);
                if (doressg == 0)
                {
                    return 0;
                }
                double sum = 0;
                if (sd !=null)
                {
                    foreach (var item in sd)
                    {
                        sum += item.salary_paid_sum;
                        string sql = $@"INSERT INTO [dbo].[salary_grant_details]( [salary_grant_id], [human_id], [human_name], [bouns_sum], [sale_sum], [deduct_sum], [salary_standard_sum], [salary_paid_sum]) VALUES('{s.salary_grant_id}','{item.human_id}','{item.human_name}','{item.bouns_sum}','{item.sale_sum}','{item.deduct_sum}','{item.salary_standard_sum}','{item.salary_paid_sum}')";
                        string sql3 = $@"UPDATE [dbo].[human_file]  SET PayrollStatus = 1 WHERE human_id='{item.human_id}'";
                        int dore = await con.ExecuteAsync(sql);
                        dore= await con.ExecuteAsync(sql3);
                        if (dore == 0)
                        {
                            return 0;
                        }
                    }
                }
             return 1;
            }
        }

        public async Task<IEnumerable<salary_grant_details>> Find_sgidAnsyc(string sgid)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"SELECT * FROM salary_grant_details WHERE salary_grant_id = '{sgid}'";
                return await con.QueryAsync<salary_grant_details>(sql);
            }
        }
        public async Task<int> UpdateAsync(List<salary_grant_details> sd, string djr, string time, string sgid,string ssid)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                double sum = 0;
                int dore = 0;
                if (sd != null)
                {
                    foreach (var item in sd)
                    {
                        sum += item.salary_paid_sum;
                        string sql = $@"UPDATE [dbo].[salary_grant_details] SET bouns_sum='{item.bouns_sum}',sale_sum='{item.sale_sum}',deduct_sum='{item.deduct_sum}',salary_paid_sum='{item.salary_paid_sum}' WHERE grd_id='{item.grd_id}'";
                         dore = await con.ExecuteAsync(sql);
                        if (dore == 0)
                        {
                            return 0;
                        }
                    }

                }
                string sql2 = $@"UPDATE [dbo].[salary_grant] SET salary_paid_sum='{sum}',checker='{djr}', check_time='{time}',check_status='1' where  salary_grant_id='{sgid}'";
                 dore=await  con.ExecuteAsync(sql2);
                if (dore == 0)
                {
                    return 0;
                }
                string sql3 = $@"UPDATE [dbo].[human_file] SET PayrollStatus=2 WHERE salary_standard_id ='{ssid}'AND PayrollStatus = 1 ";
                dore = await con.ExecuteAsync(sql3);
                if (dore == 0)
                {
                    return 0;
                }
                return 1;
            }
        }

    }
}
