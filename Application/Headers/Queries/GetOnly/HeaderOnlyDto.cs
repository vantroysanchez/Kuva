using Application.Common.Mappings;
using Application.Headers.Queries.Gets;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Headers.Queries.GetOnly
{
    public class HeaderOnlyDto : IMapFrom<Header>
    {
        public int Code { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Status { get; set; }
    }
}
