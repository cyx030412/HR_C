using Dao;
using HR.Models;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class HumanResourcesController : Controller
    {
        //公共属性 dao
        Config_Public_CharDAO cpdao=new Config_Public_CharDAO();
        //
        HumanResourcesDao grdao=new HumanResourcesDao();
        //
        salary_standardDao ssdao=new salary_standardDao();
        //人力资源
        human_fileDao hfdao=new human_fileDao();
        //职位
        salary_standardDao sdao =new salary_standardDao();
        // GET: Position

        //人力资源档案登记
        public async Task<ActionResult> human_register(string id)
        {
            EngageResume obj=await grdao.FindID(id);
            IEnumerable<Config_Public_Char> list = await cpdao.ZCSelect();
            ViewBag.xia = await ssdao.FindAllAsync();
            ViewBag.ssssss = list;
            ViewBag.xi = obj;
            return View();
        }
        //查询 不同的公共属性
        public async Task<ActionResult> FindPC()
        {
            IEnumerable<Config_Public_Char> list=await cpdao.FindKind_cyxAsync();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //录入 list 视图
        public ActionResult FindList()
        {
            return View();
        }
        //查询 可以录入的
        public async Task<ActionResult> FindLL()
        {
            IEnumerable<EngageResume> list = await grdao.Find();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult WSC()
        {
            var file = HttpContext.Request.Files["file"];//获取上传文件对象
                                                         //生成文件名
            string name = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            //获取后缀名
            string ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            //相对路径
            string mz = name + ext;
            string path = $"~/Uploaders/" + mz;
            //绝对路径
            string jpath = Server.MapPath(path);
            //创建上传文件对应的文件夹
            (new FileInfo(jpath)).Directory.Create();
            file.SaveAs(jpath);
            return Content(mz);

        }
        public async Task<int> HMInsert(human_file ee)

        {
            return await hfdao.HFInsertAsync(ee);
        }
        /// <summary>
        /// 选择调动职位
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Xia1()
        {
            IEnumerable<Cascade> cs = await sdao.ZwAsync();
            return Json(cs, JsonRequestBehavior.AllowGet);
        }

        //复核list
        public ActionResult check_list()
        {
            return View();
        }
        //查询待复核的数据
        public async Task<ActionResult> FindFH()
        {
            IEnumerable<human_file> list=await hfdao.FindFH();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 人力资源档案复核
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Human_check(string id)
        {
            ViewBag.xia = await ssdao.FindAllAsync();
            ViewBag.ssssss = await cpdao.ZCSelect();
            ViewBag.xx = await hfdao.Jia(id);
            human_file ff = await hfdao.Jia(id);
            ViewBag.img = ff.human_picture;
            return View();
        }
        /// <summary>
        /// 提交人力资源档案复核
        /// </summary>
        /// <param name="ee"></param>
        /// <returns></returns>
        public async Task<int> HMUpdate(human_file ee)
        {
            return await hfdao.HFUpdateAsync(ee);
        }


    }
}