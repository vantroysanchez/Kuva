using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events.HeaderEvents
{
    public class HeaderDeletedEvent : BaseEvent
    {
        public HeaderDeletedEvent(Header header)
        {
            Header = header;
        }

        public Header Header { get; }
    }
}
