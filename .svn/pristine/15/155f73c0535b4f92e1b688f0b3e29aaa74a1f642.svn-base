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
    public class UserController : Controller
    {
        // GET: User
        //用户dao层
        usersDao udao=new usersDao();
        //角色 dao
        RolesDao rdao = new RolesDao();
        //用户列表

        //权限表
        jurisdictionDao judao=new jurisdictionDao();
        //角色权限表
        RolesJurisdictionDao rjdao=new RolesJurisdictionDao();
        public ActionResult user_list()
        {
            return View();
        }
        //查询用户列表
        public async Task<ActionResult> userList(FenYe<users> fen)
        {
            FenYe<users> fy = await udao.FindFyAsyns(fen);
            return Json(fy, JsonRequestBehavior.AllowGet);
        }
        //添加用户视图
      public ActionResult user_add()
        {
            return View();  
        }
        //查询 角色信息
        public async Task<ActionResult> FindRoles()
        {
            IEnumerable<Rolese> list = await rdao.FindAsncy();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //添加用户
        public async Task<ActionResult> AddUsers(users u)
        {
            int dore =await udao.AddAsync(u);
            return Content(dore.ToString());
        }
        //删除用户 
        public async Task<ActionResult> DelUsers(string  uid)
        {
            int dore = await udao.DelAsync(uid);
            return Content(dore.ToString());
        }
        //修改 用户 视图
        public async Task<ActionResult> user_edit(string uid)
        {
            users u=await udao.FindIdAsync(uid);
            ViewBag.u = u;
            return View();
        }
        //修改 用户
        public async Task<ActionResult> UpdateUser(users u)
        {
            int dore = await udao.UpdateAsync(u);
            return Content(dore.ToString());
        }

        //权限 视图
        public ActionResult right_list()
        {
            return View();
        }
        //查询角色 分页
        public async Task<ActionResult> FindFyRoles(FenYe<Rolese> r)
        {
            FenYe<Rolese> fy = await rdao.FenYeAsncy(r);
            return Json(fy, JsonRequestBehavior.AllowGet);
        }
        //添加 角色 视图
        public ActionResult right_add()
        {
            return View();
        }
        //添加 
        public async Task<ActionResult> Add_Roles(Rolese r)
        {
            int dore = await rdao.Add_RolesAsncy(r);
            return Content(dore.ToString());
        }
        public async Task<ActionResult> right_list_information(string rid)
        {
            Rolese r=await rdao.FindIDAsncy(rid);
            ViewBag.r=r; 
            return View();
        }
        //显示树状图
        public async Task<ActionResult> ThreeGet()
        {
            IEnumerable<Menu> list=await judao.GetTherrs();
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        //查询已存在的权限
        public async Task<ActionResult> FindRJ(string id)
        {
            IEnumerable<RolesJurisdiction> list=await judao.FindRJAsync(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //添加角色权限
        public async Task<ActionResult> AddRJ(string rid,string jid)
        {
            int dore = await rjdao.AddAscny(rid, jid);
            return Content(dore.ToString());
        }
        //删除角色权限
        public async Task<ActionResult> DELRJ(string rid, string jid)
        {
            int dore = await rjdao.DelAscny(rid, jid);
            return Content(dore.ToString());
        }
        //修改角色
        public async Task<ActionResult> UpdateR(Rolese r)
        {
            int dore = await rdao.UpdateAync(r);
            return Content(dore.ToString());
        }
        //删除
        public async Task<ActionResult> DelR(string id)
        {
            int dore=await rdao.DelAync(id);
            return Content(dore.ToString());
        }

        //left 
        public async Task<ActionResult> Menu_find(int id)
        {
            List<Menu> list =await  rjdao.GetList(id);
            return Json(list, JsonRequestBehavior.AllowGet);

        }
    }
}