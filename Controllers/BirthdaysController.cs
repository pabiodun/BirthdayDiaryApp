﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BirthdayDiaryApp.Models;



namespace BirthdayDiaryApp.Controllers
{
    public class BirthdaysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Birthdays
        public ActionResult Index()
        {
            return View(db.Birthdays.ToList());
        }

        // GET: Birthdays/ChatRoom
        public ActionResult ChatRoom()
        {
            var comments = db.Comments.Include(x => x.Replies).ToList();
            return View(comments);
        }

        // POST: Birthdays/PostReply
        [HttpPost]
        
        public ActionResult PostReply(ReplyVM model)
        {
           
            Reply r = new Reply();
            r.Text = model.Reply;
            r.CommentId = model.CID;
            
            r.CreatedOn = DateTime.Now;

            db.Replies.Add(r);
            db.SaveChanges();
            return RedirectToAction("ChatRoom");
        }

        // POST: Birthdays/PostComment
        [HttpPost]
        
        public ActionResult PostComment(string CommentText)
        {
            
            Comment c = new Comment();
            c.Text = CommentText;
            c.CreatedOn = DateTime.Now;
            int Email = 0;
            c.UserId = Email;
            db.Comments.Add(c);
            db.SaveChanges();

            return RedirectToAction ("ChatRoom");
        }

        // GET: Birthdays/ShowSearchForm
        public ActionResult ShowSearchForm()
        {
            return View();
        }

        // POST: Birthdays/ShowSearchResults
        public ActionResult ShowSearchResults(String SearchPhrase)
        {
            return View("Index", db.Birthdays.Where(b => b.BirthName.Contains(SearchPhrase)).ToList());
        }

        // GET: Birthdays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Birthdays birthdays = db.Birthdays.Find(id);
            if (birthdays == null)
            {
                return HttpNotFound();
            }
            return View(birthdays);
        }

        // GET: Birthdays/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Birthdays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BirthName,BirthDate")] Birthdays birthdays)
        {
            if (ModelState.IsValid)
            {
                db.Birthdays.Add(birthdays);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(birthdays);
        }

        // GET: Birthdays/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Birthdays birthdays = db.Birthdays.Find(id);
            if (birthdays == null)
            {
                return HttpNotFound();
            }
            return View(birthdays);
        }

        // POST: Birthdays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BirthName,BirthDate")] Birthdays birthdays)
        {
            if (ModelState.IsValid)
            {
                db.Entry(birthdays).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(birthdays);
        }

        // GET: Birthdays/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Birthdays birthdays = db.Birthdays.Find(id);
            if (birthdays == null)
            {
                return HttpNotFound();
            }
            return View(birthdays);
        }

        // POST: Birthdays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Birthdays birthdays = db.Birthdays.Find(id);
            db.Birthdays.Remove(birthdays);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       // [HttpPost, ActionName("Remove")]
        //[ValidateAntiForgeryToken]
       // [Authorize]
       // public ActionResult RemoveConfirmed(int id, Comment comments)
      //  {
      //      Comment comment = db.Comments.Find(id);
       //     db.Comments.Remove(comments);
       //     db.SaveChanges();
      //      return RedirectToAction("ChatRoom");
     //   }

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
