using Dao;
using HR.Models;
using Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class Salary_scaleController : Controller
    {
        //公共属性DAO层引用
        Config_Public_CharDAO config_Public_CharDAO = new Config_Public_CharDAO();
        //薪酬 Dao
        salary_standardDao salaryDao = new salary_standardDao();
        //薪酬标准单详细信息表
        salary_standard_detailsDao ssdao = new salary_standard_detailsDao();
        //用户dao
        usersDao usersDao = new usersDao();
        // GET: Salary_scale
        //薪酬标准登记
        public ActionResult salarystandard_register()
        {
            return View();
        }
        //薪酬项目 显示
        public async Task<ActionResult> Salary_itemSelect()
        {

            IEnumerable<Config_Public_Char> xc = await config_Public_CharDAO.XCSelect();
            return Json(xc, JsonRequestBehavior.AllowGet);
        }
        //薪酬标准登记
        public async Task<ActionResult> Salary_itemADD(salary_standard s, string item_ids, string item_names, string salaries)
        {
            int dore = await salaryDao.SalarySelectAsync(s, item_ids, item_names, salaries);
            return Content(dore.ToString());
        }
        //薪酬标准登记复核  视图
        public ActionResult salarystandard_check_list()
        {
            return View();
        }
        //薪酬标准待复 查询所有
        public async Task<ActionResult> salarystandard_check_listFind(FenYe<salary_standard> f)
        {
            FenYe<salary_standard> list = await salaryDao.FenYeFindAsync(f);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //薪酬标准确认复核视图
        public async Task<ActionResult> salarystandard_check(string No)
        {
            ViewBag.s = No;
            salary_standard sa = await salaryDao.FindIDAsync(No);
            ViewBag.list = sa;
            return View();
        }
        //查询 薪酬标准单详细信息表
        public async Task<ActionResult> SSDId(string No)
        {
            IEnumerable<salary_standard_details> list = await ssdao.FindIDAsync(No);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //薪酬标准 复核
        public async Task<ActionResult> SSDFH(salary_standard s, string item_ids, string salaries)
        {
            int dore = await salaryDao.UpdateAsync(s, item_ids, salaries);
            return Content(dore.ToString());
        }

        //薪酬标准查询 视图
        public ActionResult salarystandard_query_locate()
        {
            return View();
        }
        //薪酬标准查询结果 视图
        public ActionResult salarystandard_query(string where)
        {
            ViewBag.s = where;
            return View();
        }
        //薪酬标准 查询所有
        public async Task<ActionResult> salarystandard_queryFind(FenYe<salary_standard> f, string where)
        {
            FenYe<salary_standard> list = await salaryDao.SelectAsync(f, where);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //薪酬标准查询 显示一条 视图
        public async Task<ActionResult> salarystandard_queryIdFind(string No)
        {
            salary_standard sa = await salaryDao.FindIDAsync(No);
            ViewBag.list = sa;
            return View();
        }
        //excel
        public async Task<ActionResult> ExcelOut(string No)
        {
            //创建workbook，说白了就是在内存中创建一个Excel文件
            IWorkbook workbook = new HSSFWorkbook();
            //要添加至少一个sheet,没有sheet的excel是打不开的
            ISheet sheet1 = workbook.CreateSheet("薪酬标准");
            ISheet sheet2 = workbook.CreateSheet("薪酬标准详细");
            IRow row0 = sheet1.CreateRow(0);
            ICell cell01=row0.CreateCell(0);
            cell01.SetCellValue("编号"+No+ "薪酬标准");
            cell01.Row.HeightInPoints = 40;
            //薪酬标准登记
            IRow row1 = sheet1.CreateRow(1);//添加第1行,注意行列的索引都是从0开始的
            ICell cell1 = row1.CreateCell(0);//给第1行添加第1个单元格
            ICell cell2 = row1.CreateCell(1);
            ICell cell3 = row1.CreateCell(2);
            ICell cell4 = row1.CreateCell(3);
            ICell cell5 = row1.CreateCell(4);
            ICell cell6 = row1.CreateCell(5);
            ICell cell7 = row1.CreateCell(6);
            ICell cell8 = row1.CreateCell(7);
            ICell cell9 = row1.CreateCell(8);
            ICell cell10 = row1.CreateCell(9);
            cell1.SetCellValue("薪酬标准单编号");//给单元格赋值
            cell2.SetCellValue("薪酬标准单名称");
            cell3.SetCellValue("制定者名字");//给单元格赋值
            cell4.SetCellValue("登记人");
            cell5.SetCellValue("复核人");
            cell6.SetCellValue("登记时间");
            cell7.SetCellValue("复核时间");
            cell8.SetCellValue("薪酬总额");
            cell9.SetCellValue("复核意见");
            cell10.SetCellValue("备注");
            //查询信息
            salary_standard sa = await salaryDao.FindIDAsync(No);
            IRow row2 = sheet1.CreateRow(2);//添加第1行,注意行列的索引都是从0开始的
            ICell cell21 = row2.CreateCell(0);//给第1行添加第1个单元格
            ICell cell22 = row2.CreateCell(1);
            ICell cell23 = row2.CreateCell(2);
            ICell cell24 = row2.CreateCell(3);
            ICell cell25 = row2.CreateCell(4);
            ICell cell26 = row2.CreateCell(5);
            ICell cell27 = row2.CreateCell(6);
            ICell cell28 = row2.CreateCell(7);
            ICell cell29 = row2.CreateCell(8);
            ICell cell210 = row2.CreateCell(9);
            cell21.SetCellValue(sa.standard_id);//给单元格赋值
            cell22.SetCellValue(sa.standard_name);
            cell23.SetCellValue(sa.designer);//给单元格赋值
            cell24.SetCellValue(sa.register);
            cell25.SetCellValue(sa.checker);
            cell26.SetCellValue(sa.regist_time);
            cell27.SetCellValue(sa.check_time);
            cell28.SetCellValue(sa.salary_sum);
            cell29.SetCellValue(sa.Check_comment);
            cell210.SetCellValue(sa.remark);

            IRow row20 = sheet2.CreateRow(0);
            ICell cell20 = row20.CreateCell(0);
            cell20.SetCellValue("编号" + No + "薪酬标准信息");
            cell20.Row.HeightInPoints = 40;

            IRow row21 = sheet2.CreateRow(1);//添加第1行,注意行列的索引都是从0开始的
            ICell cel21 = row21.CreateCell(0);//给第1行添加第1个单元格
            ICell cel22 = row21.CreateCell(1);
            ICell cel23 = row21.CreateCell(2);
            cel21.SetCellValue("薪酬项目序号");//给单元格赋值
            cel22.SetCellValue("薪酬项目名称");
            cel23.SetCellValue("薪酬金额");//给单元格赋值

            IEnumerable<salary_standard_details> list = await ssdao.FindIDAsync(No);
            int i = 1;
            foreach (var item in list)
            {
                i++;
                IRow row = sheet2.CreateRow(i);
                for (int j = 0; j <= list.Count(); j++)
                {
                    string str = "";
                    if (j == 0)
                    {
                        str = item.sdt_id.ToString();
                    }
                    else if (j==1)
                    {
                        str = item.standard_name;
                    }
                    else
                    {
                        str = item.salary.ToString();
                    }

                    ICell cell = row.CreateCell(j);
                    
                    cell.SetCellValue(str);
                }
            }
            //写入文件
            using (FileStream file = new FileStream(@"E:\.net\HrExcel\"+ No + "薪酬标准.xls", FileMode.Create))
            {
                workbook.Write(file);

                return Content("打印成功");
            }
        }

        //薪酬标准变更 查询视图
        public ActionResult salarystandard_change_locate()
        {
            return View();
        }

        //薪酬标准 查询所有视图
        public ActionResult salarystandard_change_list(string where)
        {
            ViewBag.s = where;
            return View();
        }
        //薪酬标准 查询所有
        public async Task<ActionResult> salarystandard_change_listFind(FenYe<salary_standard> f, string where)
        {
            FenYe<salary_standard> list = await salaryDao.SelectAsync(f, where);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //薪酬标准 变更视图
        public async Task<ActionResult> salarystandard_change(string No)
        {
            salary_standard sa = await salaryDao.FindIDAsync(No);
            ViewBag.list = sa;
            return View();
        }
        //薪酬标准 复核
        public async Task<ActionResult> SSGenGai(salary_standard s, string item_ids, string salaries)
        {
            int dore = await salaryDao.Update_gengaiAsync(s, item_ids, salaries);
            return Content(dore.ToString());
        }
        //查询用户列表
        public async Task<ActionResult> UserList()
        {
            IEnumerable<users> list=await usersDao.FindAsyns();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}