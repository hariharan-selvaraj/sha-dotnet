using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHA.Data.Models
{
    public class UserDetails
    {
        public long UserId { get; set; }
        public long Company { get; set; }
        public string AdminName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PassportNumber { get; set; }
        public string IcNumber { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; }
    }
    public class LoginDetails
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string IcNo { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
    public class MenuDetails
    {
        public long MenuId { get; set; }
        public string MenuCode { get; set; }
        public string MenuName { get; set; }
        public string Task { get; set; }
        public long ParentId { get; set; }
        public int RoleId { get; set; }
        public string MenuIcon { get; set; }
    }
    public class companyDetails
    {
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public int CompanyTypeId { get; set; }
        public string CompanyUen { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PostalCode { get; set; }
    }
    public class companyGridModel
    {
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
    }
}
