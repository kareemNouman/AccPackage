using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core.Framework.DomainModels
{
  public  class Employee :Entity<Int64>
    {
       
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public Nullable<System.DateTime> DOJ { get; set; }
        public string AddressID { get; set; }
        public string Department { get; set; }
        public Nullable<decimal> Allowance { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public Nullable<long> EmployeeNo { get; set; }
    }
}
