using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core.Framework.DomainModels
{
    public class Customer : Entity<Int64>
    {
        
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string AliasName { get; set; }
        public string AddressID { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string Other { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }


    public class DBAudit : Entity<Int64>
    {

        public string TableName { get; set; }

        public Int64 UserId { get; set; }
        public Int64 PrimaryKeyValue { get; set; }
        public string NewData { get; set; }
        public string OldData { get; set; }
        public AuditActions Actions { get; set; }

        public AccountRole Role { get; set; }
        public DateTime ActionDate { get; set; }

        //public string ChangedColumns { get; set; }
    }


    public enum AuditActions : int
    {
        Insert = 1,
        Update = 2,
        Delete = 3
    }
}
