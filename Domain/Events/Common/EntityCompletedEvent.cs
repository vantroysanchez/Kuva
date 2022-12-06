using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events.Common
{
    public class EntityCompletedEvent<T> : BaseEvent where T : class
    {
        public EntityCompletedEvent(T entity)
        {
            Entity = entity;
        }

        public T Entity { get; }
    }
}
