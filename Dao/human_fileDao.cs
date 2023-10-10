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
    public class human_fileDao
    {
        string constr = "Data Source=DESKTOP-9EC5BJ1;Initial Catalog=HR_DB;Integrated Security=True";
        public async Task<IEnumerable<human_file>> FindAsync(string ssid)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"SELECT * FROM [dbo].[human_file] WHERE salary_standard_id='{ssid}'";
                return await con.QueryAsync<human_file>(sql);
            }
        }
        //计算 总人数
        public async Task<string> FindSum(string ssid)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"SELECT COUNT(0) FROM [dbo].[human_file] WHERE salary_standard_id='{ssid}' and check_status = 0";
                string dore= await con.QueryFirstAsync<string>(sql);
                if (dore == null)
                {
                    dore = "0";
                }
                return dore;

            }
        }
        //计算 薪酬金额
        public async Task<string> FindMoney(string ssid)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"SELECT sum(salary_sum) FROM [dbo].[human_file] WHERE salary_standard_id='{ssid}' and check_status = 0";
                string dore = await con.QueryFirstAsync<string>(sql);
                if (dore == null)
                {
                    dore = "0";
                }
                return dore;
            }
        }
        //查询数据 分页
        public async Task<FenYe<human_file>> FindFenYeAsync(FenYe<human_file> fy , string where)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@pageSize", fy.pageSize);
                dp.Add("@keyName", "huf_id");
                dp.Add("@tableName", "human_file");
                dp.Add("@currentPage", fy.currentPage);
                dp.Add("@where", where);
                dp.Add("@rows", 0, DbType.Int32, ParameterDirection.Output);
                FenYe<human_file> feye = new FenYe<human_file>();
                feye.CList = await con.QueryAsync<human_file>("proc_Fenye", dp, commandType: CommandType.StoredProcedure);
                int zts = dp.Get<int>("@rows");
                feye.Totalnumber = zts;
                feye.Totalpage = zts % fy.pageSize == 0 ? zts / fy.pageSize : zts / fy.pageSize + 1;
                return feye;
            }
        }

        //查询单条信息
        public async Task<human_file> FindFHidAsync(string fhid)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"SELECT * FROM [dbo].[human_file]  WHERE human_id='{fhid}'";
                return await con.QueryFirstAsync<human_file>(sql);
            }
        }
        //查询一级 档案数据
        public async Task<IEnumerable<human_file>> FindLeve1(string leveid)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"SELECT * from  [dbo].[human_file] WHERE first_kind_id='{leveid}' and second_kind_id is null and PayrollStatus = 0";
                return await con.QueryAsync<human_file>(sql);
            }
        }
        //查询二级 档案数据
        public async Task<IEnumerable<human_file>> FindLeve2(string leveid)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"SELECT * from  [dbo].[human_file] WHERE second_kind_id='{leveid}' and third_kind_id is null and PayrollStatus = 0";
                return await con.QueryAsync<human_file>(sql);
            }
        }
        //查询三级 档案数据
        public async Task<IEnumerable<human_file>> FindLeve3(string leveid)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = $@"SELECT * from  [dbo].[human_file] WHERE third_kind_id='{leveid}' and PayrollStatus = 0";
                return await con.QueryAsync<human_file>(sql);
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="hm"></param>
        /// <returns></returns>
        public async Task<int> HFInsertAsync(human_file hm)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sc = $"update engage_resume set pass_check_status='2' where res_id='{hm.ii}'";
                await con.ExecuteAsync(sc);
                string sql = $"select major_kind_name from [dbo].[config_major_kind] where [major_kind_id]='{hm.human_major_kind_id}'";
                string cc = con.QueryFirstOrDefault<string>(sql);
                string sql1 = $"select major_name from [dbo].[config_major] where [major_kind_id]='{hm.human_major_kind_id}' and [major_id]='{hm.human_major_id}'";
                string cc1 = con.QueryFirstOrDefault<string>(sql1);
                string yi = $"select [first_kind_name] from [dbo].[config_file_first_kind] where [first_kind_id]='{hm.first_kind_id}'";
                string yij = con.QueryFirstOrDefault<string>(yi);

                string er = $@"select second_kind_name from [dbo].[config_file_second_kind] where[second_kind_id]='{hm.second_kind_id}'";
                string erj = "";
                try
                {
                    erj = con.QueryFirstOrDefault<string>(er);
                }
                catch (Exception e)
                {
                    erj = "NULL";
                }

                string san = $@"select [third_kind_name] from [dbo].[config_file_third_kind] where [third_kind_id]='{hm.third_kind_id}'";
                string sanj = "";
                try
                {
                    sanj = con.QueryFirstOrDefault<string>(san);
                }
                catch (Exception e)
                {
                    sanj = "NULL";
                }
                string ccc = $@"select attribute_name from config_public_char where attribute_kind = '职称' and attribute_name = '{hm.human_pro_designation}'";
                string ccd = await con.QueryFirstAsync<string>(ccc);

                string sq = $@"SELECT RIGHT(CONCAT('000000',FLOOR(RAND()* 1000000)),6)";
                string sj = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
                string bh = "HYN" + sj + await con.QueryFirstAsync<string>(sq);

                string sql5 = $@"insert into [dbo].[human_file](human_id, first_kind_id, first_kind_name, 
                second_kind_id, second_kind_name, third_kind_id, third_kind_name, human_name, 
                human_address, human_postcode, human_pro_designation, human_major_kind_id, 
                human_major_kind_name, human_major_id, hunma_major_name, human_telephone, 
                human_mobilephone, human_bank, human_account, human_qq, human_email, human_hobby, 
                human_speciality, human_sex, human_religion, human_party, human_nationality, human_race, 
                human_birthday, human_birthplace, human_age, human_educated_degree, human_educated_years,
                human_educated_major, human_society_security_id, human_id_card, remark, 
                salary_standard_id, salary_standard_name, salary_sum, demand_salaray_sum, paid_salary_sum,
                major_change_amount, bonus_amount, training_amount, file_chang_amount, 
                human_histroy_records, human_family_membership, human_picture, attachment_name, 
                 register,regist_time
                ) values('{bh}',
                '{hm.first_kind_id}','{yij}','{hm.second_kind_id}','{erj}',
                '{hm.third_kind_id}','{sanj}',
                '{hm.human_name}','{hm.human_address}','{hm.human_postcode}','{ccd}','{hm.human_major_kind_id}',
                '{cc}','{hm.human_major_id}','{cc1}','{hm.human_telephone}',
                '{hm.human_mobilephone}','{hm.human_bank}','{hm.human_account}','{hm.human_qq}',
                '{hm.human_email}','{hm.human_hobby}','{hm.human_speciality}','{hm.human_sex}',
                '{hm.human_religion}','{hm.human_party}','{hm.human_nationality}','{hm.human_race}',
                '{hm.human_birthday}','{hm.human_birthplace}','{hm.human_age}','{hm.human_educated_degree}',
                '{hm.human_educated_years}','{hm.human_educated_major}','{hm.human_society_security_id}',
                '{hm.human_id_card}','{hm.remark}','{hm.salary_standard_id}','{hm.salary_standard_name}',
                '{hm.salary_sum}','{hm.demand_salaray_sum}','{hm.salary_sum}','{hm.major_change_amount}',
                '{hm.bonus_amount}','{hm.training_amount}','{hm.file_chang_amount}','{hm.human_histroy_records}',
                '{hm.human_family_membership}','{hm.human_picture}','{hm.attachment_name}',
                '{hm.register}','{hm.regist_time}')";
                return await con.ExecuteAsync(sql5);
            }
        }

        //查询待复核的数据
        public async  Task<IEnumerable<human_file>> FindFH()
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = "SELECT * FROM [dbo].[human_file]  WHERE check_status is NULL ";
                return await con.QueryAsync<human_file>(sql);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="hm"></param>
        /// <returns></returns>
        public async Task<int> HFUpdateAsync(human_file hm)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {

                string ccc = $@"select attribute_name from config_public_char where attribute_kind = '职称' and attribute_name = '{hm.human_pro_designation}'";
                string ccd = await con.QueryFirstAsync<string>(ccc);
                string sql5 = $@"update [dbo].[human_file]set   human_name='{hm.human_name}', human_address='{hm.human_address}', human_postcode='{hm.human_postcode}', human_pro_designation='{ccd}', human_telephone='{hm.human_telephone}', human_mobilephone='{hm.human_mobilephone}', human_bank='{hm.human_bank}', human_account='{hm.human_account}', human_qq='{hm.human_qq}', human_email='{hm.human_email}', human_hobby='{hm.human_hobby}', human_speciality='{hm.human_speciality}', human_sex='{hm.human_sex}', human_religion='{hm.human_religion}', human_party='{hm.human_party}', human_nationality='{hm.human_nationality}', human_race='{hm.human_race}', human_birthday='{hm.human_birthday}', human_birthplace='{hm.human_birthplace}', human_age='{hm.human_age}', human_educated_degree='{hm.human_educated_degree}', human_educated_years='{hm.human_educated_years}', human_educated_major='{hm.human_educated_major}', human_society_security_id='{hm.human_society_security_id}', human_id_card='{hm.human_id_card}', remark='{hm.remark}', salary_standard_id='{hm.salary_standard_id}', salary_standard_name='{hm.salary_standard_name}', salary_sum='{hm.salary_sum}', demand_salaray_sum='{hm.demand_salaray_sum}', paid_salary_sum='{hm.salary_sum}',  human_histroy_records='{hm.human_histroy_records}', human_family_membership='{hm.human_family_membership}',  attachment_name='{hm.attachment_name}', check_status=1,checker='{hm.checker}',check_time='{hm.check_time}'where  human_id='{hm.human_id}'";
                return await con.ExecuteAsync(sql5);
            }
        }
        public async Task<human_file> Jia(string id)
        {
            using (SqlConnection con=new SqlConnection(constr))
            {
                string sql = $@"SELECT * FROM [dbo].[human_file]  WHERE human_id='{id}'";
                return await  con.QueryFirstAsync<human_file>(sql);
            }
        }
    }
}
