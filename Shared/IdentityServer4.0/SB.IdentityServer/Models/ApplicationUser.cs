

using SB.IdentityNPocoStores.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServerWithAspNetIdentity.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
            
        }
        //public bool IsAdmin { get; set; }
        public int UserType { get; set; } = 1; //default UserType=Customer, 0 -Customer, 11-Employee, 99-Admin
       
        //public int UserId { get; set; }
        public int AppId { get; set; } = 1; // default for chow choice app as per db

        public int AppRoleId { get; set; } = 6; // default for customer as per db
        public string DataEventRecordsRole { get; set; }
        public string SecuredFilesRole { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; } = 1;
        public DateTime? ModifyDate { get; set; }
        public int ModifyBy { get; set; }
        public bool IsActive { get; set; }
        //public bool Primary { get; set; }
        public string CustomerPhoto { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }
        //public int Q1Id { get; set; }
        //public string Q1Answer { get; set; }

        //public int Q2Id { get; set; }
        //public string Q2Answer { get; set; }

        //public int Q3Id { get; set; }
        //public string Q3Answer { get; set; }
        
    }
}

//new Claim(JwtClaimTypes.Role, "admin"),
//new Claim(JwtClaimTypes.Role, "dataEventRecords.admin"),
//new Claim(JwtClaimTypes.Role, "dataEventRecords.user"),
//new Claim(JwtClaimTypes.Role, "dataEventRecords"),
//new Claim(JwtClaimTypes.Role, "securedFiles.user"),
//new Claim(JwtClaimTypes.Role, "securedFiles.admin"),
//new Claim(JwtClaimTypes.Role, "securedFiles")