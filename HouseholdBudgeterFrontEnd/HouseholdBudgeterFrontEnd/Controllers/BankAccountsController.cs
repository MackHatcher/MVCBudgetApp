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
    public class BankAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        // GET: BankAccounts
        public ActionResult Index(int id)
        {
            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/bankaccounts/view/" + id;
            var result = aspHelper.ASPHelperGet(url);
            var cookie = Request.Cookies["token"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;

                var households = JsonConvert
                    .DeserializeObject<List<ViewBankAccountsViewModel>>(jsonString);

                return View(households);

            }

            return View();
        }

        [HttpGet]
        // GET: BankAccounts/Create
        public ActionResult Create(int id)
        {
            ASPHelper aspHelper = new ASPHelper();
            var urlCategoryList = "http://localhost:54102/api/transactions/view/getaccountlist/" + id;
            var result = aspHelper.ASPHelperGet(urlCategoryList);
            var cookie = Request.Cookies["token"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;

                var accounts = JsonConvert
                    .DeserializeObject<List<ViewBankAccountsViewModel>>(jsonString);

                var selectAccount = new SelectList(accounts, "Id", "Name");

                ViewBag.selectAccount = selectAccount;

            }

            return View();
        }

        // POST: BankAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, Account account)
        {

            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/bankaccounts/create";
            var CreateAccountList = new List<ReqParameters>();
            var cookie = Request.Cookies["token"];

            var Name = new ReqParameters();
            Name.Name = "Name";
            Name.Value = account.Name;
            CreateAccountList.Add(Name);

            var HouseholdId = new ReqParameters();
            HouseholdId.Name = "HouseHoldId";
            HouseholdId.Value = id.ToString();
            CreateAccountList.Add(HouseholdId);

            var Balance = new ReqParameters();
            Balance.Name = "Balance";
            Balance.Value = account.Balance.ToString();
            CreateAccountList.Add(Balance);

            var result = aspHelper.ASPHelperPost(url, CreateAccountList);

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

            return RedirectToAction("Index");
        
    }
        [HttpGet]
    // GET: BankAccounts/Edit/5
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
        var url = "http://localhost:54102/api/BankAccounts/Edit/" + id;
        aspHelper.ASPHelperGet(url);

        return View();
    }

       
    // POST: BankAccounts/Edit/5

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Account account)
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
        var url = "http://localhost:54102/api/bankaccounts/edit/" + id;
        var EditAccountsList = new List<ReqParameters>();

        var accountName = new ReqParameters();
        accountName.Name = "Name";
        accountName.Value = account.Name;
        EditAccountsList.Add(accountName);

        aspHelper.ASPHelperPost(url, EditAccountsList);

        return RedirectToAction("Index/" + id);
    }

        [HttpGet]
    //GET: BankAccounts/Balance/
    public ActionResult Balance(int id, Account bankAccount)
    {
        ASPHelper aspHelper = new ASPHelper();
        var url = "http://localhost:54102/api/bankaccounts/balance";
        var UpdateBalance = new List<ReqParameters>();

        var UserId = new ReqParameters();
        UserId.Name = "UserId";
        UserId.Value = id.ToString();
        UpdateBalance.Add(UserId);

        var BankAccountId = new ReqParameters();
        BankAccountId.Name = "HouseHoldId";
        BankAccountId.Value = id.ToString();
        UpdateBalance.Add(BankAccountId);

        var BankAccountBalance = new ReqParameters();
        BankAccountBalance.Name = "Balance";
        BankAccountBalance.Value = bankAccount.Balance.ToString();
        UpdateBalance.Add(BankAccountBalance);

        var TransactionId = new ReqParameters();
        TransactionId.Name = "TransactionId";
        TransactionId.Value = id.ToString();
        UpdateBalance.Add(TransactionId);

        var AccountId = new ReqParameters();
        AccountId.Name = "BankAccountId";
        AccountId.Value = id.ToString();
        UpdateBalance.Add(AccountId);

        var CreatorId = new ReqParameters();
        CreatorId.Name = "CreatorId";
        CreatorId.Value = id.ToString();
        UpdateBalance.Add(CreatorId);

        aspHelper.ASPHelperPost(url, UpdateBalance);

        return View(bankAccount);

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
