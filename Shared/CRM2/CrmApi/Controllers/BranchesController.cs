using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using CrmApi.Data;
using CrmApi.Models;

namespace CrmApi.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class BranchesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Branches
        //public IQueryable<Branch> GetBranches()
        //{
        //    return db.Branches.Include(t=>t.Brand).Include(b=>b.Branch2);
        //}
        public List<Branches> GetBranches()
        {
            List<Branches> branches = new List<Branches>();
            var allBranches= db.Branches.Include(b => b.Branch2);
            foreach(Branch branch in allBranches)
            {
                Branches tempBranch = new Branches();
                tempBranch.branchId = branch.branchId;
                tempBranch.affiliateTag = branch.affiliateTag;
                tempBranch.branchName = branch.branchName;
                tempBranch.brandId = branch.brandId;
                tempBranch.brandName = "<Brand Name>";
                tempBranch.code = branch.code;
                tempBranch.enabled = branch.enabled;
                tempBranch.language = branch.language;
                tempBranch.location = branch.location;
                if (branch.Branch2 != null)
                {
                    tempBranch.parentBranchName = branch.Branch2.branchName;
                }
                tempBranch.parentId = branch.parentId;
                tempBranch.timeZone = branch.timeZone;
                branches.Add(tempBranch);

            }
            return branches;
        }

        // GET: api/Branches/5
        [ResponseType(typeof(Branch))]
        public IHttpActionResult GetBranch(int id)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }

            return Ok(branch);
        }

        // PUT: api/Branches/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBranch(int id, Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != branch.branchId)
            {
                return BadRequest();
            }

            db.Entry(branch).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Branches
        [ResponseType(typeof(Branch))]
        public IHttpActionResult PostBranch(Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Branches.Add(branch);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = branch.branchId }, branch);
        }

        // DELETE: api/Branches/5
        [ResponseType(typeof(Branch))]
        public IHttpActionResult DeleteBranch(int id)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }

            db.Branches.Remove(branch);
            db.SaveChanges();

            return Ok(branch);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BranchExists(int id)
        {
            return db.Branches.Count(e => e.branchId == id) > 0;
        }
    }
}