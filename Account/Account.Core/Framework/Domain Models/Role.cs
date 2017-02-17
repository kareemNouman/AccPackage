using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core.Framework.Domain_Models
{
  public  class Role
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> RoleType { get; set; }
        public Nullable<bool> IsSystemRole { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
    }
}
