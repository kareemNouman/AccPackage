using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core.Framework.DomainModels
{
    public class Vendor : Entity<Int64>
    {
        
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string AliasName { get; set; }
        public Nullable<long> AddressId { get; set; }
        public string Notes { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string EmailId { get; set; }
        public string Website { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public Nullable<long> AccountNumber { get; set; }
        public Nullable<System.DateTime> AsOfDate { get; set; }
        public Nullable<long> TaxId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
