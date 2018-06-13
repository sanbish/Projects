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
    [Route("api/entities/UserPositions")]
    public class UserPositionsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/UserPositions
        public IQueryable<UserPosition> GetUserPositions()
        {
            return db.UserPositions;
        }

        // GET: api/UserPositions/5
        [ResponseType(typeof(UserPosition))]
        public IHttpActionResult GetUserPosition(int id)
        {
            UserPosition userPosition = db.UserPositions.Find(id);
            if (userPosition == null)
            {
                return NotFound();
            }

            return Ok(userPosition);
        }

        // PUT: api/UserPositions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserPosition(int id, UserPosition userPosition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userPosition.id)
            {
                return BadRequest();
            }

            db.Entry(userPosition).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPositionExists(id))
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

        // POST: api/UserPositions
        [ResponseType(typeof(UserPosition))]
        public IHttpActionResult PostUserPosition(UserPosition userPosition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserPositions.Add(userPosition);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userPosition.id }, userPosition);
        }

        // DELETE: api/UserPositions/5
        [ResponseType(typeof(UserPosition))]
        public IHttpActionResult DeleteUserPosition(int id)
        {
            UserPosition userPosition = db.UserPositions.Find(id);
            if (userPosition == null)
            {
                return NotFound();
            }

            db.UserPositions.Remove(userPosition);
            db.SaveChanges();

            return Ok(userPosition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserPositionExists(int id)
        {
            return db.UserPositions.Count(e => e.id == id) > 0;
        }
    }
}