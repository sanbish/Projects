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

namespace CrmApi.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]

    public class DepartmentUsersController : ApiController
    {
       
        private Entities db = new Entities();

        // GET: api/DepartmentUsers
        public IQueryable<DepartmentUser> GetDepartmentUsers()
        {
            return db.DepartmentUsers;
        }

        // GET: api/DepartmentUsers/5
        [ResponseType(typeof(DepartmentUser))]
        public IHttpActionResult GetDepartmentUser(int id)
        {
            DepartmentUser departmentUser = db.DepartmentUsers.Find(id);
            if (departmentUser == null)
            {
                return NotFound();
            }

            return Ok(departmentUser);
        }

        // PUT: api/DepartmentUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartmentUser(int id, DepartmentUser departmentUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departmentUser.departmentId)
            {
                return BadRequest();
            }

            db.Entry(departmentUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentUserExists(id))
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

        // POST: api/DepartmentUsers
        [ResponseType(typeof(DepartmentUser))]
        public IHttpActionResult PostDepartmentUser(DepartmentUser departmentUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DepartmentUsers.Add(departmentUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DepartmentUserExists(departmentUser.departmentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = departmentUser.departmentId }, departmentUser);
        }

        // DELETE: api/DepartmentUsers/5
        [ResponseType(typeof(DepartmentUser))]
        public IHttpActionResult DeleteDepartmentUser(int id)
        {
            DepartmentUser departmentUser = db.DepartmentUsers.Find(id);
            if (departmentUser == null)
            {
                return NotFound();
            }

            db.DepartmentUsers.Remove(departmentUser);
            db.SaveChanges();

            return Ok(departmentUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentUserExists(int id)
        {
            return db.DepartmentUsers.Count(e => e.departmentId == id) > 0;
        }
    }
}