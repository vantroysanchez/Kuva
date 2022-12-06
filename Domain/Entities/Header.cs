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
        //public bool Status { get; set; }

        private bool _status;

        public bool Status
        {
            get => _status;
            set
            {
                if (value == true && _status == false)
                {
                    AddDomainEvent(new HeaderCompletedEvent(this));
                }

                _status = value;
            }
        }

        public ICollection<Detail>? Details { get; set; }
    }
}
