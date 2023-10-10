using Dao;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HR.Controllers
{
    public class Salary_IssueController : Controller
    {
        salary_grantDao  sgdao=new salary_grantDao();
        human_fileDao hdao =new human_fileDao();
        salary_standard_detailsDao ssddao=new salary_standard_detailsDao();
        salary_grant_detailsDao sgddao=new salary_grant_detailsDao();
        config_file_first_kindDao leve1=new config_file_first_kindDao();
        config_file_third_kindDao leve3 = new config_file_third_kindDao();
        config_file_second_kindDao leve2=new config_file_second_kindDao();
        // GET: Salary_Issue
        public ActionResult register_locate()
        {
            return View();
        }
        //查询薪资发放
        public async Task<ActionResult> register_list(string leve)
        {
            ViewBag.leve = leve;
            string sum=await sgdao.FindSum(leve);
            string money = await sgdao.FindmoneySum(leve);
            ViewBag.money = money;
            ViewBag.sum = sum;  
            return View();
        }
        //查询等级
        public async Task<ActionResult> register_listFind(string leve)
        {
            if (leve == "I")
            {
                IEnumerable<SalaSum> list = await leve1.FindsumnAsync();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else if(leve == "II")
            {
                IEnumerable<SalaSum> list = await leve2.FindsumnAsync();
                return Json(list, JsonRequestBehavior.AllowGet);

            }
            else{
                IEnumerable<SalaSum> list = await leve3.FindsumnAsync();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        //查询薪资登记
        public  ActionResult register_commit(string leve ,string  leveid)
        {
            ViewBag.leve = leve;
            ViewBag.leveid = leveid;
            return View();
        }
        //查询 档案
        public async Task<ActionResult> HFLeve(string leve, string leveid)
        {
            if (leve == "I")
            {
                IEnumerable<human_file> list = await hdao.FindLeve1(leveid);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else if (leve == "II")
            {
                IEnumerable<human_file> list = await hdao.FindLeve2(leveid);
                return Json(list, JsonRequestBehavior.AllowGet);

            }
            else
            {
                IEnumerable<human_file> list = await hdao.FindLeve3(leveid);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        //使用薪资标准编号查询档案信息
        public async Task<ActionResult> FindFileSSid(string ssid)
        {
           IEnumerable<human_file> list=await  hdao.FindAsync(ssid);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> FindssdSSid(string ssid)
        {
            IEnumerable<salary_standard_details> list = await ssddao.FindIDAsync(ssid);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> InsertSGD(List<salary_grant_details> list,salary_grant sglist)
        {
            int dore = await sgddao.FindAsync(list, sglist);
            return Content(dore.ToString());       
        }
        //薪酬发放登记审核 视图
        public ActionResult check_list()
        {
            return View();
        }
        //薪酬发放登记审核 查询数据
        public async Task<ActionResult> check_listFind(FenYe<salary_grant> fen)
        {
            FenYe<salary_grant> list =await sgdao.FindAllAnscy(fen);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //薪酬发放复核
        public ActionResult check(string ssid,string sgid)
        {
            ViewBag.ssid = ssid;    
            ViewBag.sgid = sgid;
            return View();
        }
        //薪酬发放复核 查询薪酬发放详细数据
        public async Task<ActionResult> check_sgdFind(string sgid)
        {
            IEnumerable<salary_grant_details> list =await  sgddao.Find_sgidAnsyc(sgid);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //薪酬发放复核 提交
        public async Task<ActionResult> check_tj(List<salary_grant_details> list, string djr, string time, string sgid,string ssid)
        {
            int dore = await sgddao.UpdateAsync(list, djr, time, sgid, ssid);
            return Content(dore.ToString());
        }
        //使用薪资发放id 查询等级 
         public async Task<ActionResult> FindLeve(string sgid)
        {
            string dore=await sgdao.FindLeve(sgid);
            return Content(dore.ToString());
        }
        //薪酬发放查询 视图
        public ActionResult query_locate()
        {
            return View();
        }
        //薪酬发放查询结果 视图
        public ActionResult query_list(string where)
        {
            ViewBag.where = where;  
            return View();
        }
        //薪酬发放查询 结果
         public async Task<ActionResult> query_list_fenye(FenYe<salary_grant> fy,string where)
        {
            FenYe<salary_grant> list = await sgdao.FindAllwhereAnscy(fy,where);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //薪酬发放 查询单个
        public ActionResult query_view(string ssid,string sgid,string time)
        {
            ViewBag.ssid= ssid;
            ViewBag.sgid= sgid; 
            ViewBag.time= time;
            return View();
        }
    }
}