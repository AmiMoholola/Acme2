using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace AcmeFrontEnd.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }


    public class PersonModel
    {
        [Required]
        public int Personid { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Firstname { get; set; }  
       // [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [DataType(DataType.Date)]
        public DateTime TerminateDate { get; set; }
    }

    public class EmployeeModel
    {
        [Required]
        public int Personid { get; set; }
        public string EmployeeNum { get; set; }
        public DateTime EmployedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime TerminatedDate { get; set; }
    }
}
