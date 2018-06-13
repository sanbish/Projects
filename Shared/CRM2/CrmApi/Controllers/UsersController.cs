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

    public class UsersController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Users
        [Route("api/Users")]
        public List<Users> GetUsers()
        {
            List<Users> Users = new List<Users>();
            List<Models.BrandInfo> brandInfos;
            var allUsers = db.Users.Include(b => b.BrandInfoes);
            foreach (User User in allUsers)
            {
                Users tempUser = new Users();
                tempUser.userId = User.userId;
                tempUser.memebershipUserId = User.memebershipUserId;
                tempUser.userName = User.userName;
                tempUser.email = User.email;
                tempUser.password = User.password;
                tempUser.profilePicture = User.profilePicture;
                tempUser.signturePicture = User.signturePicture;
                tempUser.firstName = User.firstName;
                tempUser.lastName = User.lastName;
                tempUser.fullName = User.fullName;
                tempUser.phoneNumber = User.phoneNumber;
                tempUser.gender = User.gender;
                tempUser.birthday = User.birthday.ToString("dd/MM/yyyy");
                tempUser.memberSince = User.memberSince.ToString("dd/MM/yyyy");
                tempUser.location = User.location;
                tempUser.languageId = User.languageId;
                tempUser.languageName = User.languageName;
                tempUser.position = User.position;
                tempUser.job = User.job;
                tempUser.voIpExtension = User.voIpExtension;
                tempUser.voIpSip = User.voIpSip;
                tempUser.promoCode = User.promoCode;
                tempUser.depositSmsNotification = User.depositSmsNotification;
                tempUser.enabled = User.enabled;
                tempUser.isOnline = User.isOnline;
                brandInfos = new List<Models.BrandInfo>();
                foreach(Data.BrandInfo brandinfo in User.BrandInfoes)
                {
                    Models.BrandInfo brandInfoModel = new Models.BrandInfo
                    {
                        id = brandinfo.id,
                        userId = brandinfo.userId,
                        brandId = brandinfo.brandId,
                        brandName = brandinfo.branchName,
                        branchId = brandinfo.branchId,
                        branchName = brandinfo.branchName,
                        departmentId = brandinfo.departmentId,
                        departmentName = brandinfo.departmentName,
                        departmentManager = brandinfo.departmentManager,
                        workingHoursStart = brandinfo.workingHoursStart,
                        workingHoursEnd = brandinfo.workingHoursEnd,
                        branches = new List<IdText>(),
                        departments= new List<IdText>()
                    };
                    brandInfoModel.branches.Add(new IdText { id = brandinfo.Branch.branchId, text = brandinfo.Branch.branchName });
                    brandInfoModel.departments.Add(new IdText { id = brandinfo.Department.departmentId, text = brandinfo.Department.departmentName });
                    brandInfos.Add(brandInfoModel);
                }
                tempUser.BrandInfoes = brandInfos;
                Users.Add(tempUser);

            }
            return Users;
            
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        [Route("api/Users/GetCurrentUser")]
        public IHttpActionResult GetCurrentUser()
        {
            int id = 1;
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.userId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.userId }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.userId == id) > 0;
        }
    }
}