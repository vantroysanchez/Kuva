using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events.Common
{
    public class EntityCreatedEvent<T> : BaseEvent where T : class
    {
        public EntityCreatedEvent(T entity)
        {
            Entity = entity;
        }

        public T Entity { get; }
    }
}
