using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using HouseholdBudgeterFrontEnd.Helpers;
using HouseholdBudgeterFrontEnd.Models;
using HouseholdBudgeterFrontEnd.Models.Classes;
using Newtonsoft.Json;

namespace HouseholdBudgeterFrontEnd.Controllers
{
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Households
        public ActionResult Index()
        {
            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/household/ViewHousehold";
            var result = aspHelper.ASPHelperGet(url);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;

                var households = JsonConvert
                    .DeserializeObject<List<ViewHouseholdViewModel>>(jsonString);

                return View(households);

            }
            return View();
        }

        public ActionResult IndexMembers()
        {
            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/household/ViewMembers";
            var result = aspHelper.ASPHelperGet(url);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;

                var members = JsonConvert
                    .DeserializeObject<List<HouseholdMembersViewModel>>(jsonString);

                return View(members);

            }
            return View();
        }

        // GET: Households/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // GET: Households/Create
        public ActionResult Create()
        {
            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/household/create";
            aspHelper.ASPHelperGet(url);
            
            return View();
        }

        // POST: Households/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Household household)
        {
            if (ModelState.IsValid)
            {
                ASPHelper aspHelper = new ASPHelper();
                var url = "http://localhost:54102/api/household/create";

                var createHousehold = new List<ReqParameters>();

                var householdName = new ReqParameters();
                householdName.Name = "Name";
                householdName.Value = household.Name;
                createHousehold.Add(householdName);
                
                var result = aspHelper.ASPHelperPost(url, createHousehold);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = result.Content.ReadAsStringAsync().Result;

                    var households = JsonConvert
                        .DeserializeObject<ViewHouseholdViewModel>(jsonString);

                    return View(households);
                }
                
                return RedirectToAction("Index");
            }

            return View(household);
        }

        //GET: Households/Invite
        public ActionResult Invite(InviteHouseholdBindingModel household)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/household/invite";

            var inviteHousehold = new List<ReqParameters>();

            var householdName = new ReqParameters();
            householdName.Name = "Name";
            householdName.Value = household.Email;
            inviteHousehold.Add(householdName);

            var householdId = new ReqParameters();
            householdId.Name = "Id";
            household.HouseHoldId = Convert.ToInt32(householdId.Value);
            inviteHousehold.Add(householdId);
            
            aspHelper.ASPHelperPost(url, inviteHousehold);

            return RedirectToAction("Index");
        }

        //POST: Households/Join
        public ActionResult Join(JoinHouseholdBindingModel household)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/household/join";

            var JoinHousehold = new List<ReqParameters>();

            var householdId = new ReqParameters();
            householdId.Name = "InviteId";
            household.InviteId = Convert.ToInt32(householdId.Value);
            JoinHousehold.Add(householdId);

            aspHelper.ASPHelperPost(url, JoinHousehold);

            return RedirectToAction("Index");
        }

        //POST: Households/Leave
        public ActionResult Leave(LeaveHouseholdBindingModel household)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/household/leave";

            var LeaveHousehold = new List<ReqParameters>();

            var householdId = new ReqParameters();
            householdId.Name = "Id";
            household.HouseHoldId = Convert.ToInt32(householdId.Value);
            LeaveHousehold.Add(householdId);

            aspHelper.ASPHelperPost(url, LeaveHousehold);

            return RedirectToAction("Index");
        } 

        //GET: Households/ViewMembers
        public ActionResult ViewMembers(int id, HouseholdMembersViewModel householdMember)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/household/view";

            var Members = new List<ReqParameters>();

            var MemberId = new ReqParameters();
            MemberId.Name = "Id";
            MemberId.Value = householdMember.Id;
            Members.Add(MemberId);

            var MemberName = new ReqParameters();
            MemberName.Name = "Name";
            MemberName.Value = householdMember.Name;
            Members.Add(MemberName);

            var MemberEmail = new ReqParameters();
            MemberEmail.Name = "Email";
            MemberEmail.Value = householdMember.Email;
            Members.Add(MemberEmail);

            var IsCreator = new ReqParameters();
            IsCreator.Name = "IsCreator";
            IsCreator.Value = Convert.ToString(householdMember.IsCreator);
            Members.Add(IsCreator);

            aspHelper.ASPHelperGet(url);

            return RedirectToAction("Index");

        }

        //GET: Households/ViewHouseholds
        public ActionResult ViewHouseholds(int id, HouseHoldViewModel household)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/household/view";

            var Members = new List<ReqParameters>();

            var HouseholdName = new ReqParameters();
            HouseholdName.Name = "Name";
            HouseholdName.Value = household.Name;
            Members.Add(HouseholdName);

            var householdMembers = new ReqParameters();
            householdMembers.Name = "Members";
            householdMembers.Value = household.Members.ToString();
            Members.Add(householdMembers);

            aspHelper.ASPHelperGet(url);

            return RedirectToAction("Index");
        }

        // GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/household/edit";
            aspHelper.ASPHelperGet(url);
            
            return RedirectToAction("Index");
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatorId")] Household household)
        {
            if (ModelState.IsValid)
            {
                ASPHelper aspHelper = new ASPHelper();
                var url = "http://localhost:54102/api/household/edit";

                var editHousehold = new List<ReqParameters>();

                var householdName = new ReqParameters();
                householdName.Name = "Name";
                householdName.Value = household.Name;
                editHousehold.Add(householdName);

                var householdId = new ReqParameters();
                householdId.Name = "Id";
                household.Id = Convert.ToInt32(householdId.Value);
                editHousehold.Add(householdId);

                var householdCreatorId = new ReqParameters();
                householdCreatorId.Name = "CreatorId";
                household.Id = Convert.ToInt32(householdCreatorId.Value);
                editHousehold.Add(householdCreatorId);

                aspHelper.ASPHelperPost(url, editHousehold);
                
                return RedirectToAction("Index");
            }
            
            return View(household);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/household/delete";
            aspHelper.ASPHelperGet(url);
            
            return RedirectToAction("Index");
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, Household household)
        {
            ASPHelper aspHelper = new ASPHelper();
            var url = "http://localhost:54102/api/household/delete";

            var deleteHousehold = new List<ReqParameters>();

            var householdName = new ReqParameters();
            householdName.Name = "Name";
            householdName.Value = household.Name;
            deleteHousehold.Add(householdName);

            var householdId = new ReqParameters();
            householdId.Name = "Id";
            household.Id = Convert.ToInt32(householdId.Value);
            deleteHousehold.Add(householdId);

            var householdCreatorId = new ReqParameters();
            householdCreatorId.Name = "CreatorId";
            household.Id = Convert.ToInt32(householdCreatorId.Value);
            deleteHousehold.Add(householdCreatorId);

            aspHelper.ASPHelperPost(url, deleteHousehold);

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
