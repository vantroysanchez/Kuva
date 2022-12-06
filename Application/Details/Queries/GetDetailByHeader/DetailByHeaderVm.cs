using Application.Headers.Queries.Gets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Details.Queries.GetDetailByHeader
{
    public class DetailByHeaderVm
    {
        public IList<DetailDtoByHeader> Lists { get; set; } = new List<DetailDtoByHeader>();
    }
}
