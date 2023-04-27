using Microsoft.AspNetCore.Mvc;
using GRIDCO.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GRIDCO.Core.ProjectAggregate.Entity;
using GRIDCO.Infrastructure.Data;
using GRIDCO.SharedKernel.Interfaces;

namespace GRIDCO.Web.Controllers
{
    public class AdminController : Controller
    {
        AppDbContext con;
        private readonly IRepository<GRIDCO_USER_TYPE> usertype;
        private readonly IRepositoryInterface repositoryInterface;
        private readonly IRepository<GRIDCO_Manage_User> manageuser;
        private readonly IRepository<GRIDCO_User_Credential> UserCredential;
        private readonly IRepository<GRIDCO_Role_Mst> GRIDCO_Role_Mst;

        public AdminController(IRepository<GRIDCO_USER_TYPE> usertype,  IRepositoryInterface repositoryInterface, IRepository<GRIDCO_Manage_User> manageuser,
         IRepository<GRIDCO_User_Credential> UserCredential,   IRepository<GRIDCO_Role_Mst> APICOL_Role_Mst, AppDbContext obj)
        {

            this.usertype = usertype;
        
            this.repositoryInterface = repositoryInterface;
            this.manageuser = manageuser;
            this.UserCredential = UserCredential;
           this.GRIDCO_Role_Mst = GRIDCO_Role_Mst;
            this.con = obj;

        }
        [HttpGet]
        public async Task<IActionResult> Role(int? id = 0)
        {
            GRIDCO_Role_Mst RM = new GRIDCO_Role_Mst();
            var rlist = con.GRIDCO_Role_Mst.Where(x => x.Is_Active == true && x.Is_Deleted == false).ToList();
            if (id > 0)
            {

                var data = con.GRIDCO_Role_Mst.Where(x => x.User_Type_Id == id).FirstOrDefault();

                ViewBag.data = rlist;
                return View(data);
            }
            else
            {
                ViewBag.data = rlist;
                return View(RM);

            }


        }
        [HttpPost]
        public async Task<IActionResult> Role(GRIDCO_Role_Mst xx)
        {
            try
            {

                int count = repositoryInterface.SaveRole(xx);
                if (count == 1)
                {
                    TempData["MngUserMessage"] = " Role Created Successfully";
                    return RedirectToAction("Role", "Admin");

                }
                else
                {
                    TempData["MngUserMessage"] = "Role Updated Successfully";
                    return RedirectToAction("Role", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw;

            }
            return View();
        }
        public async Task<IActionResult> DeleteRole(int id)
        {
            var data = repositoryInterface.DeleteRole(id);
            TempData["Delete"] = "Role Details Delete Successfully";
            return RedirectToAction("Role", "Admin");

        }
    }
}
