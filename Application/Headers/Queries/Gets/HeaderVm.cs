using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Headers.Queries.Gets
{
    public class HeaderVm
    {
        public IList<HeaderDto> Lists { get; set; } = new List<HeaderDto>();        
    }
}
