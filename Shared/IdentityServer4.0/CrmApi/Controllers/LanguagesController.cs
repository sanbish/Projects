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
    [Route("api/entities/Languages")]
    public class LanguagesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Languages
        //public IQueryable<Language> GetLanguages()
        //{
        //    return db.Languages;
        //}
        public List<Languages> GetLanguages()
        {
            List<Languages> lng = new List<Languages>();
            var languages = db.Languages;

            foreach(Language ln in languages)
            {
                Languages temp = new Languages();
                temp.id = ln.lngId;
                temp.name = ln.lngName;
                temp.code = ln.lngCode;
                temp.status = ln.status;
                lng.Add(temp);
            }

            return lng;
        }

        // GET: api/Languages/5
        [ResponseType(typeof(Language))]
        public IHttpActionResult GetLanguage(byte id)
        {
            Language language = db.Languages.Find(id);
            if (language == null)
            {
                return NotFound();
            }

            return Ok(language);
        }

        // PUT: api/Languages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLanguage(byte id, Language language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != language.lngId)
            {
                return BadRequest();
            }

            db.Entry(language).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageExists(id))
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

        // POST: api/Languages
        [ResponseType(typeof(Language))]
        public IHttpActionResult PostLanguage(Language language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Languages.Add(language);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = language.lngId }, language);
        }

        // DELETE: api/Languages/5
        [ResponseType(typeof(Language))]
        public IHttpActionResult DeleteLanguage(byte id)
        {
            Language language = db.Languages.Find(id);
            if (language == null)
            {
                return NotFound();
            }

            db.Languages.Remove(language);
            db.SaveChanges();

            return Ok(language);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LanguageExists(byte id)
        {
            return db.Languages.Count(e => e.lngId == id) > 0;
        }
    }
}