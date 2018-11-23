using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HouseholdBudgeterFrontEnd.Helpers;
using HouseholdBudgeterFrontEnd.Models;
using HouseholdBudgeterFrontEnd.Models.Classes;
using Newtonsoft.Json;

namespace HouseholdBudgeterFrontEnd.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        // GET: Categories
        public ActionResult Index(int id)
        {
            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/category/view/" + id;
            var result = aspHelper.ASPHelperGet(url);
            var cookie = Request.Cookies["token"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;

                var categories = JsonConvert
                    .DeserializeObject<List<ViewCategoriesViewModel>>(jsonString);

                return View(categories);

            }

            return View();
        }

        [HttpGet]
        // GET: Categories/Create
        public ActionResult Create(int id)
        {
            
            var cookie = Request.Cookies["token"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, Categories categories)
        {
            
            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/category/create";
            var ViewCategoriesList = new List<ReqParameters>();
            var cookie = Request.Cookies["token"];
            
            var CategoriesName = new ReqParameters();
            CategoriesName.Name = "Name";
            CategoriesName.Value = categories.Name;
            ViewCategoriesList.Add(CategoriesName);

            var CategoriesId = new ReqParameters();
            CategoriesId.Name = "HouseHoldId";
            CategoriesId.Value = id.ToString();
            ViewCategoriesList.Add(CategoriesId);

            var result = aspHelper.ASPHelperPost(url, ViewCategoriesList);

            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Index/" + id);
            }
            
            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;

                var errorMessage = JsonConvert
                    .DeserializeObject<ApiError>(jsonString);

                foreach (var property in errorMessage.ModelState)
                {
                    foreach (var msg in property.Value)
                    {
                        ModelState.AddModelError("", msg);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. Please try again later.");
            }

            return RedirectToAction("Index/" + id);
            
        }
        
        [HttpGet]
        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            var cookie = Request.Cookies["token"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return RedirectToAction("Index", "HouseHold");
            }

            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/Categories/Edit/" + id;
            aspHelper.ASPHelperGet(url);

            return RedirectToAction("Index/" + id);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditCategoriesBindingModel categories)
        {
            var cookie = Request.Cookies["token"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/categories/edit";
            var EditCategoryList = new List<ReqParameters>();

            var CategoryDescription = new ReqParameters();
            CategoryDescription.Name = "Description";
            CategoryDescription.Value = categories.Description;
            EditCategoryList.Add(CategoryDescription);

            aspHelper.ASPHelperPost(url, EditCategoryList);

            return View(categories);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
