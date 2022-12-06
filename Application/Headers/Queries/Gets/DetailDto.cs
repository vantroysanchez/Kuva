using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Headers.Queries.Gets
{
    public class DetailDto: IMapFrom<Detail>
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }
}
