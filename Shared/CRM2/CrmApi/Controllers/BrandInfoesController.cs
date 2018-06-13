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

    [Route("api/brands/GetBrandsInfo")]
    public class BrandInfoesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/BrandInfoes
        public List<Models.BrandInfo> GetBrandInfoes()
        {
            List<Models.BrandInfo> brandInfos = new List<Models.BrandInfo>();

            foreach (Data.BrandInfo brandinfo in db.BrandInfoes)
            {
                Models.BrandInfo brandInfoModel = new Models.BrandInfo
                {
                    id = brandinfo.id,
                    userId = brandinfo.userId,
                    brandId = brandinfo.brandId,
                    brandName = brandinfo.brandName,
                    branchId = brandinfo.branchId,
                    branchName = brandinfo.branchName,
                    departmentId = brandinfo.departmentId,
                    departmentName = brandinfo.departmentName,
                    departmentManager = brandinfo.departmentManager,
                    workingHoursStart = brandinfo.workingHoursStart,
                    workingHoursEnd = brandinfo.workingHoursEnd,
                    branches = new List<IdText>(),
                    departments = new List<IdText>()
                };
                brandInfoModel.branches.Add(new IdText { id = brandinfo.Branch.branchId, text = brandinfo.Branch.branchName });
                brandInfoModel.departments.Add(new IdText { id = brandinfo.Department.departmentId, text = brandinfo.Department.departmentName });
                brandInfoModel.departments.Add(new IdText { id = 2, text = "Sales" });
                brandInfoModel.departments.Add(new IdText { id = 3, text = "Marketing" });
                brandInfoModel.departments.Add(new IdText { id = 4, text = "Compliance" });
                brandInfos.Add(brandInfoModel);
            }

            return brandInfos;
        }

        // GET: api/BrandInfoes/5
        [ResponseType(typeof(Data.BrandInfo))]
        public IHttpActionResult GetBrandInfo(int id)
        {
            Data.BrandInfo brandInfo = db.BrandInfoes.Find(id);

            

            if (brandInfo == null)
            {
                return NotFound();
            }

            return Ok(brandInfo);
        }

        // PUT: api/BrandInfoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBrandInfo(int id, Data.BrandInfo brandInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != brandInfo.id)
            {
                return BadRequest();
            }

            db.Entry(brandInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandInfoExists(id))
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

        // POST: api/BrandInfoes
        [ResponseType(typeof(Data.BrandInfo))]
        public IHttpActionResult PostBrandInfo(Data.BrandInfo brandInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BrandInfoes.Add(brandInfo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = brandInfo.id }, brandInfo);
        }

        // DELETE: api/BrandInfoes/5
        [ResponseType(typeof(Data.BrandInfo))]
        public IHttpActionResult DeleteBrandInfo(int id)
        {
            Data.BrandInfo brandInfo = db.BrandInfoes.Find(id);
            if (brandInfo == null)
            {
                return NotFound();
            }

            db.BrandInfoes.Remove(brandInfo);
            db.SaveChanges();

            return Ok(brandInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BrandInfoExists(int id)
        {
            return db.BrandInfoes.Count(e => e.id == id) > 0;
        }
    }
}