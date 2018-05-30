using CrmApi.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;

namespace CrmApi.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    
    public class BrandsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Brands
        public IQueryable<Brand> GetBrands()
        {
            return db.Brands;
        }

        // GET: api/Brands/5
        [ResponseType(typeof(Brand))]
        public IHttpActionResult GetBrand(int id)
        {
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        // PUT: api/Brands/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBrand(int id, Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != brand.brandId)
            {
                return BadRequest();
            }

            db.Entry(brand).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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

        // POST: api/Brands
        [ResponseType(typeof(Brand))]
        public IHttpActionResult PostBrand(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Brands.Add(brand);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = brand.brandId }, brand);
        }

        // DELETE: api/Brands/5
        [ResponseType(typeof(Brand))]
        public IHttpActionResult DeleteBrand(int id)
        {
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }

            db.Brands.Remove(brand);
            db.SaveChanges();

            return Ok(brand);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BrandExists(int id)
        {
            return db.Brands.Count(e => e.brandId == id) > 0;
        }
    }
}