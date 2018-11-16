using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HouseholdBudgeterFrontEnd.Models;
using HouseholdBudgeterFrontEnd.Models.Classes;

namespace HouseholdBudgeterFrontEnd.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Account).Include(t => t.Category).Include(t => t.EnteredBy);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.EnteredById = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }
        
        // POST: Transactions/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CreateTransactionBindingModel transactions)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            var CreateTransactionList = new List<ReqParameters>();

            var TransactionId = new ReqParameters();
            TransactionId.Name = "AccountId";
            TransactionId.Value = id.ToString();
            CreateTransactionList.Add(TransactionId);

            var TransactionDescription = new ReqParameters();
            TransactionDescription.Name = "Description";
            TransactionDescription.Value = transactions.Description;
            CreateTransactionList.Add(TransactionDescription);

            var TransactionDate = new ReqParameters();
            TransactionDate.Name = "Date";
            TransactionDate.Value = transactions.Date.ToString();
            CreateTransactionList.Add(TransactionDate);

            var TransactionAmount = new ReqParameters();
            TransactionAmount.Name = "Amount";
            TransactionAmount.Value = transactions.Amount.ToString();
            CreateTransactionList.Add(TransactionAmount);
            
            return View(transactions);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        // POST: Transactions/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Transactions transactions)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            var EditTransaction = new List<ReqParameters>();

            var TransactionId = new ReqParameters();
            TransactionId.Name = "TransactionId";
            TransactionId.Value = transactions.;
            EditTransaction.Add(TransactionId);

            return View(transactions);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transactions transactions = db.Transactions.Find(id);
            db.Transactions.Remove(transactions);
            db.SaveChanges();
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
