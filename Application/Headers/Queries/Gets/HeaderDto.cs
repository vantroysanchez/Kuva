using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Headers.Queries.Gets
{
    public class HeaderDto: IMapFrom<Header>
    {
        public HeaderDto()
        {
            Details = new List<DetailDto>();
        }

        public int Code { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Status { get; set; }

        public List<DetailDto> Details { get; set; }
    }
}
