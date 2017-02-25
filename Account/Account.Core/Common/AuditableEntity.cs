using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core.Common
{
   public class AuditableEntity<T> : Entity<T>, IAuditableEntity
    {
        public DateTime CreatedDate { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public Int64 UpdatedBy { get; set; }
    }
}
