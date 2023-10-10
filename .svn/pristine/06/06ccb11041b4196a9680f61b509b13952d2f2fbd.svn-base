using Dao;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class TransferController : Controller
    {
        //leve dao层
        config_file_third_kindDao leve3=new config_file_third_kindDao();
        //人力资源档案
        human_fileDao hfDao=new human_fileDao();
        //职位分类 dao
         Config_Major_KindDAO CMKDao = new Config_Major_KindDAO();
        //职位 dao
        Config_MajorDAO cmdao=new Config_MajorDAO();
        //薪资
        salary_standardDao ssdao=new salary_standardDao();
        //职位调用
        major_changeDao mcdao = new major_changeDao();

        // GET: Transfer
        //调动登记查询 视图
        public ActionResult register_locate()
        {
            return View();
        }
        //查询一二三级的数据
        public async Task<ActionResult> LeveFind() {
           IEnumerable<Cascade>  list=  await  leve3.CascadeAsync();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //查询职位分类和职位
        public async Task<ActionResult> MCFind()
        {
            IEnumerable<Cascade> list = await cmdao.CascadeAsync();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //调动登记查询 结果视图
        public ActionResult register_list(string where) {
         ViewBag.where = where;
         return View(); 
        } 
        //调动登记查询 结果数据
        public async Task<ActionResult> FindHf(FenYe<human_file> fy,string where)
        {
            FenYe<human_file> fenye = await hfDao.FindFenYeAsync(fy, where);
            return Json(fenye, JsonRequestBehavior.AllowGet);
        }
        //调动登记 视图
        public async Task<ActionResult> register(string HFId)
        {
            human_file hf = await hfDao.FindFHidAsync(HFId);
            ViewBag.HF = hf;
            return View();
        }
        //查询职位分类数据
        public async Task<ActionResult> FindCMK()
        {
            IEnumerable<Config_Major_Kind> list = await CMKDao.ZWFLSelect();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //查询职位数据
         public async Task<ActionResult> FindCM(string Cid)
        {
            IEnumerable<Config_Major> list=await cmdao.FindMidAsync(Cid);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //查询薪资表
        public async Task<ActionResult> FindSS()
        {
            IEnumerable<salary_standard> list = await ssdao.FindAllAsync();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        //调动审核列表 视图
        public ActionResult check_list()
        {
            return View();
        }

        //调动登记
        public async Task<ActionResult> CheckInsert(major_change mc)
        {
            int dore=await mcdao.AddAsync(mc);
            return Content(dore.ToString());
        }
        //查询待复核的数据
        public async Task<ActionResult> FindFH(FenYe<major_change> fy)
        {
           FenYe<major_change>  list=await mcdao.FinddfhAsync(fy);
            return Json(list,JsonRequestBehavior.AllowGet);
        }
        //调动 审核 
        public async Task<ActionResult> check(string id)
        {
            major_change lc = await mcdao.FindIdAsync(id);
            ViewBag.mcobj = lc;
            return View();
        }
        //调用 通过
        public async Task<ActionResult> CheckUpdate(major_change mc)
        {
            int dore = await mcdao.UpdateAsync(mc);
            return Content(dore.ToString());
        }
        //不通过
        public async Task<ActionResult> CheckUpdateNo(major_change mc)
        {
            int dore = await mcdao.UpdateNoAync(mc);
            return Content(dore.ToString());
        }
        //调动查询
        public ActionResult locate()
        {
            return View();
        }
        //调档查询 list
        public ActionResult list(string where)
        {
            ViewBag.where = where;
            return View();
        }
        //调档查询结果
        public async Task<ActionResult> Findlist(string where)
        {
            IEnumerable<major_change> list=await mcdao.FindWhereAsync(where);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //调档 登记 查询
        public async Task<ActionResult> detail(string id)
        {
            major_change m=await mcdao.FindIDAsync(id);
            ViewBag.mcobj = m;
            return View();
        }
    }
}