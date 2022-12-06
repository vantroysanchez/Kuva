using Domain.Common;
using Domain.Events;
using Domain.Events.HeaderEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Header: Audit
    {
        public int Code { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Status { get; set; }

        [NotMapped]
        private bool _done;
        [NotMapped]
        public bool Done
        {
            get => _done;
            set
            {
                if (value == true && _done == false)
                {
                    AddDomainEvent(new HeaderCompletedEvent(this));
                }

                _done = value;
            }
        }

        public virtual ICollection<Detail>? Detail { get; set; }
    }
}
