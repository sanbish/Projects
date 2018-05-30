using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrmApi.Data;

namespace CrmApi.Controllers
{
    public class TradingServersController : Controller
    {
        private Entities db = new Entities();

        // GET: TradingServers
        public ActionResult Index()
        {
            return View(db.TradingServers.ToList());
        }

        // GET: TradingServers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TradingServer tradingServer = db.TradingServers.Find(id);
            if (tradingServer == null)
            {
                return HttpNotFound();
            }
            return View(tradingServer);
        }

        // GET: TradingServers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TradingServers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "serverId,serverName,url")] TradingServer tradingServer)
        {
            if (ModelState.IsValid)
            {
                db.TradingServers.Add(tradingServer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tradingServer);
        }

        // GET: TradingServers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TradingServer tradingServer = db.TradingServers.Find(id);
            if (tradingServer == null)
            {
                return HttpNotFound();
            }
            return View(tradingServer);
        }

        // POST: TradingServers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "serverId,serverName,url")] TradingServer tradingServer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tradingServer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tradingServer);
        }

        // GET: TradingServers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TradingServer tradingServer = db.TradingServers.Find(id);
            if (tradingServer == null)
            {
                return HttpNotFound();
            }
            return View(tradingServer);
        }

        // POST: TradingServers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TradingServer tradingServer = db.TradingServers.Find(id);
            db.TradingServers.Remove(tradingServer);
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
