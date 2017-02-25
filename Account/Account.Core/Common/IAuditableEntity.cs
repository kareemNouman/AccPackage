using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core.Common
{
   public interface IAuditableEntity
    {
        DateTime CreatedDate { get; set; }

        Int64 CreatedBy { get; set; }

        DateTime UpdatedDate { get; set; }

        Int64 UpdatedBy { get; set; }
    }
}
