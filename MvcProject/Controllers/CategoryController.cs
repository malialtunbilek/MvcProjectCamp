using BusinessLayer.Concrete;
using BusinessLayer.FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProject.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        CategoryManager cm = new CategoryManager(new EfCategoryDAL());
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCategoryList()
        {
            var categoryValues = cm.GetList();
            return View(categoryValues);
        }


        //Sayfaya ilk yüklendiği zaman çalışacak ActionResult
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }



        //Bir butona tıkladığımda sayfaya bir şey post edildiği zaman bu ActionResult çalışsın
        [HttpPost]
        public ActionResult AddCategory(Category cat)
        {
            //cm.CategoryAddBL(cat);
            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult results = categoryValidator.Validate(cat);
            if (results.IsValid)
            {
                cm.CategoryAdd(cat);
                return RedirectToAction("GetCategoryList");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return RedirectToAction("GetCategoryList");
        }
    }
}