﻿using Dao;
using HR.Models;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    //客户化设置
    public class ClientController : Controller
    {
        //公共属性DAO层引用
        Config_Public_CharDAO config_Public_CharDAO = new Config_Public_CharDAO();
        //职位设置DAO层引用
        Config_MajorDAO config_majorDAO = new Config_MajorDAO();
        //职位分类DAO层引用
        Config_Major_KindDAO config_Major_KindDAO = new Config_Major_KindDAO();

        //一级机构设值Dao
        config_file_first_kindDao leveldao = new config_file_first_kindDao();
        //二级机构设置Dao
        config_file_second_kindDao leve2dao = new config_file_second_kindDao();
        //三级机构设置Dao
        config_file_third_kindDao leve3dao = new config_file_third_kindDao();
        //人力资源档案管理设置
        //Ⅰ级机构设置
        public ActionResult Leve1()
        {
            return View();
        }
        //职称查询
        public async Task<ActionResult> zccx()
        {
            IEnumerable<Config_Public_Char> cm = await config_Public_CharDAO.ZCSelect();
            return Json(cm, JsonRequestBehavior.AllowGet);
        }
        //职称查询
        public ActionResult zcszcx()
        {
            return View();
        }
        //公共属性设置查询
        public async Task<ActionResult> Index()
        {
            IEnumerable<Config_Public_Char> cpc = await config_Public_CharDAO.CPCSelect();
            return Json(cpc, JsonRequestBehavior.AllowGet);
        }
        //公共属性新增
        public async Task<ActionResult> GGInsert(Config_Public_Char config_Public_Char)
        {
            int result = await config_Public_CharDAO.GGInsert(config_Public_Char);
            if (result > 0)
            {
                return RedirectToAction("/ggsxsz");
            }
            else
            {
                return View(config_Public_Char);
            }
        }
        //公共属性新增
        public ActionResult ggsxxz()
        {
            return View();
        }
        //公共属性删除
        public async Task<ActionResult> GGDelete(int pbc_id)
        {
            int result = await config_Public_CharDAO.GGDelete(pbc_id);
            if (result > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("2");
            }
        }
        //公共属性设置
        public ActionResult ggsxsz()
        {
            return View();
        }

        //职位设置查询
        public async Task<ActionResult> Index1()
        {
            IEnumerable<Config_Major> cm = await config_majorDAO.ZWSelect();
            return Json(cm, JsonRequestBehavior.AllowGet);
        }


        //职位设置删除
        public async Task<ActionResult> ZWDelete(int major_kind_id)
        {
            int result = await config_majorDAO.ZWDelete(major_kind_id);
            if (result > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("2");
            }
        }
        //职位设置新增
        public async Task<ActionResult> ZWSZInsert(Config_Major config_Major)
        {
            int result = await config_majorDAO.ZWSZInsert(config_Major);
            if (result > 0)
            {
                return RedirectToAction("/zwssz");
            }
            else
            {
                return View(config_Major);
            }
        }
        //职位设置新增
        public ActionResult zwszadd()
        {
            return View();
        }
        //职位设置
        public ActionResult zwssz()
        {
            return View();
        }

        //职位分类查询
        public async Task<ActionResult> Index2()
        {
            IEnumerable<Config_Major_Kind> cm = await config_Major_KindDAO.ZWFLSelect();
            return Json(cm, JsonRequestBehavior.AllowGet);
        }
        //职位分类新增
        public ActionResult zzfladd()
        {
            return View();
        }
        //职位分类新增
        public async Task<ActionResult> ZWFLInsert(Config_Major_Kind config_Major_Kind)
        {

            int result = await config_Major_KindDAO.Config_major_kindInsertAsync(config_Major_Kind);
            if (result > 0)
            {
                return RedirectToAction("/zwflcx");

            }
            else
            {
                return View(config_Major_Kind);
            }


        }
        //职位分类删除
        public async Task<ActionResult> ZZDLDelete(int mfk_id)
        {
            int result = await config_Major_KindDAO.ZWFLDelete(mfk_id);
            if (result > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("2");
            }
        }
        //职位分类设置
        public ActionResult zwflcx()
        {
            return View();
        }


        //一级机构设置的查询所有数据
        public async Task<ActionResult> Leve1Find()
        {
            IEnumerable<config_file_first_kind> list = await leveldao.FindAsync();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //Ⅰ级机构设置添加视图
        public ActionResult Leve1Select()
        {
            return View();
        }
        //1级机构设置添加
        public async Task<ActionResult> Leve1SelectZX(config_file_first_kind t)
        {
            int dore = await leveldao.SelectAsync(t);
            if (dore == 1)
            {
                return Content("1");
            }
            else
            {
                return Content("2");
            }
        }
        //1级机构设置删除
        public async Task<ActionResult> Leve1Delect(string No)
        {
            int dore = await leveldao.DelectAsync(No);
            if (dore == 1)
            {
                return Content("1");
            }
            else
            {
                return Content("2");
            }
        }
        //一级机构设置修改视图
        public async Task<ActionResult> Leve1Update(string obj)
        {
            config_file_first_kind leve1 = await leveldao.FindIDAsync(obj);
            ViewBag.s = leve1;
            return View();
        }
        //1级机构设置修改
        public async Task<ActionResult> Leve1UpdateZX(config_file_first_kind t)
        {
            int dore = await leveldao.UpdateAsync(t);
            if (dore == 1)
            {
                return Content("1");
            }
            else
            {
                return Content("2");
            }
        }
        //二
        //二级机构设置
        public ActionResult Leve2()
        {
            return View();
        }
        //显示二级机构设置所有数据
        public async Task<ActionResult> Leve2Find()
        {
            IEnumerable<config_file_second_kind> list = await leve2dao.FindAsync();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //二级添加视图
        public async Task<ActionResult> Leve2Select()
        {
            IEnumerable<config_file_first_kind> list = await leveldao.FindName();
            ViewBag.s = list;
            return View();
        }
        //二级添加
        public async Task<ActionResult> Leve2Add(config_file_second_kind c)
        {
            int dore = await leve2dao.SelectAsync(c);
            return Content(dore.ToString());
        }
        //二级删除
        public async Task<ActionResult> Leve2Del(string No)
        {
            int dore = await leve2dao.DeleteAsync(No);
            return Content(dore.ToString());
        }
        //二级修改视图
        public async Task<ActionResult> Leve2UpdateView(string obj)
        {
            config_file_second_kind leve1 = await leve2dao.FindIDAsync(obj);
            ViewBag.s = leve1;
            return View();
        }
        //二级修改
        public async Task<ActionResult> Leve2Update(config_file_second_kind c)
        {
            int dore = await leve2dao.UpdateAsync(c);
            return Content(dore.ToString());
        }
        //
        //三级机构设置视图
        public ActionResult Leve3()
        {
            return View();
        }
        //三级机构设置查询全部数据
        public async Task<ActionResult> Leve3Find()
        {
            IEnumerable<config_file_third_kind> list = await leve3dao.FindAsync();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //三级机构设置添加视图
        public async Task<ActionResult> Leve3Select()
        {
            IEnumerable<config_file_first_kind> list = await leveldao.FindName();
            ViewBag.s = list;
            return View();
        }
        //三级机构添加
        public async Task<ActionResult> Leve3Insert(config_file_third_kind c)
        {
            int dore = await leve3dao.InsertAsyns(c);
            return Content(dore.ToString());
        }
        //三级机构删除
        public async Task<ActionResult> Leve3Del(string No)
        {
            int dore = await leve3dao.DeleteAsync(No);
            return Content(dore.ToString());
        }
        //三级机构修改视图
        public async Task<ActionResult> Leve3UpdateView(string No)
        {
            config_file_third_kind leve3 = await leve3dao.FindIDAsync(No);
            ViewBag.s = leve3;
            return View();
        }
        //三级机构修改
        public async Task<ActionResult> Leve3Update(config_file_third_kind f)
        {
            int dore = await leve3dao.UpdateAsync(f);
            return Content(dore.ToString());
        }

        //薪酬项目设置 视图
        public ActionResult Salary_item()
        {
            return View();
        }
        //薪酬项目 显示
        public async Task<ActionResult> Salary_itemSelect()
        {
         IEnumerable<Config_Public_Char> xc= await config_Public_CharDAO.XCSelect();
         return Json(xc, JsonRequestBehavior.AllowGet);
        }
        //薪酬项目 增加视图
        public ActionResult Salary_itemAddview()
        {
            return View();
        }
        //薪酬项目 添加
        public async Task<ActionResult> Salary_itemAdd(string name)
        {
          int dore=await config_Public_CharDAO.XCAdd(name);
            return Content(dore.ToString());
        }
        //薪酬项目 删除
        public async Task<ActionResult> Salary_itemDel(string name)
        {
            int dore = await config_Public_CharDAO.XCDelect(name);
            return Content(dore.ToString());
        }
        //级联
        public async Task<ActionResult> CascadeSele()
        {
            List<Cascade> list =await leve2dao.CascadeAsync();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}