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
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        // GET: Transactions
        public ActionResult Index(int id)
        {
            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/transactions/view/" + id;
            var result = aspHelper.ASPHelperGet(url);
            var cookie = Request.Cookies["token"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;

                var transactions = JsonConvert
                    .DeserializeObject<List<ViewTransactionsViewModel>>(jsonString);

                return View(transactions);
            }
            return View();
        }

        [HttpGet]
        // GET: Transactions
        public ActionResult IndexAccounts(int id, int AccountId)
        {
            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/transactions/viewaccounts/" + id + "?accountId=" + AccountId;
            var result = aspHelper.ASPHelperGet(url);
            var cookie = Request.Cookies["token"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;

                var transactions = JsonConvert
                    .DeserializeObject<List<ViewTransactionsViewModel>>(jsonString);

                return View(transactions);
            }
            return View();
        }

        [HttpGet]
        // GET: Transactions
        public ActionResult IndexCategory(int id, int CategoryId)
        {
            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/transactions/viewaccounts/" + id + "?categoryId=" + CategoryId;
            var result = aspHelper.ASPHelperGet(url);
            var cookie = Request.Cookies["token"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;

                var transactions = JsonConvert
                    .DeserializeObject<List<ViewTransactionsViewModel>>(jsonString);

                return View(transactions);
            }
            return View();
        }

        [HttpGet]
        // GET: Transactions/Create
        public ActionResult Create(int id)
        {
            ASPHelper aspHelper = new ASPHelper();

            var urlCategoryList = "http://localhost:54102/api/transactions/view/getcategorylist/" + id;
            var urlAccountList = "http://localhost:54102/api/transactions/view/getaccountlist/" + id;

            var accountResult = aspHelper.ASPHelperGet(urlAccountList);
            var categoryResult = aspHelper.ASPHelperGet(urlCategoryList);

            if (accountResult.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = accountResult.Content.ReadAsStringAsync().Result;

                var transactions = JsonConvert
                    .DeserializeObject<List<ViewBankAccountsViewModel>>(jsonString);

                var selectAccount = new SelectList(transactions, "Id", "Name");

                ViewBag.selectAccount = selectAccount;

            }

            if (categoryResult.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = categoryResult.Content.ReadAsStringAsync().Result;

                var transactions = JsonConvert
                    .DeserializeObject<List<ViewCategoriesViewModel>>(jsonString);

                var selectCategory = new SelectList(transactions, "Id", "Name");

                ViewBag.selectCategory = selectCategory;
            }

            return View();
        }

        // POST: Transactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, Transactions transactions)
        {

            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/transactions/create";
            var CreateTransactionList = new List<ReqParameters>();
            var cookie = Request.Cookies["token"];


            var TransactionId = new ReqParameters();
            TransactionId.Name = "Id";
            TransactionId.Value = id.ToString();
            CreateTransactionList.Add(TransactionId);

            var AccountId = new ReqParameters();
            AccountId.Name = "AccountId";
            AccountId.Value = transactions.AccountId.ToString();
            CreateTransactionList.Add(AccountId);

            var Description = new ReqParameters();
            Description.Name = "Description";
            Description.Value = transactions.Description;
            CreateTransactionList.Add(Description);

            var CategoryId = new ReqParameters();
            CategoryId.Name = "CategoryId";
            CategoryId.Value = transactions.CategoryId.ToString();
            CreateTransactionList.Add(CategoryId);

            var TransactionDate = new ReqParameters();
            TransactionDate.Name = "Date";
            TransactionDate.Value = transactions.Date.ToString();
            CreateTransactionList.Add(TransactionDate);

            var TransactionAmount = new ReqParameters();
            TransactionAmount.Name = "Amount";
            TransactionAmount.Value = transactions.Amount.ToString();
            CreateTransactionList.Add(TransactionAmount);

            var result = aspHelper.ASPHelperPost(url, CreateTransactionList);

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
                return RedirectToAction("Index");
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
        // GET: Transactions/Edit/5
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
            var url = "http://localhost:54102/api/Transactions/Edit/" + id;
            aspHelper.ASPHelperGet(url);

            return RedirectToAction("Index/" + id);
        }

        
        // POST: Transactions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditTransactionBindingModel transactions)
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
            var url = "http://localhost:54102/api/transactions/edit";
            var EditTransaction = new List<ReqParameters>();

            var TransactionDescription = new ReqParameters();
            TransactionDescription.Name = "Description";
            TransactionDescription.Value = transactions.Description;
            EditTransaction.Add(TransactionDescription);

            var TransactionAmount = new ReqParameters();
            TransactionAmount.Name = "Amount";
            TransactionAmount.Value = transactions.Amount.ToString();
            EditTransaction.Add(TransactionAmount);

            aspHelper.ASPHelperPost(url, EditTransaction);

            return View(transactions);
        }

        [HttpPost]
        //POST: Transactions/Void
        public ActionResult Void(int id)
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

            if (ModelState.IsValid)
            {
                ASPHelper aspHelper = new ASPHelper();
                var url = "http://localhost:54102/api/transactions/void/" + id;
                var VoidTransaction = new List<ReqParameters>();

                var VoidId = new ReqParameters();
                VoidId.Name = "TransactionId";
                VoidId.Value = id.ToString();
                VoidTransaction.Add(VoidId);

                aspHelper.ASPHelperPost(url, VoidTransaction);

                return RedirectToAction("Index/" + id);
            }
            return RedirectToAction("Index");
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
