using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Detail: Audit
    {
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public int HeaderId { get; set; }
        public virtual Header? Header { get; set; }
    }
}
