using System;
using System.Collections.Generic;
using System.Text;

namespace TakeIt.Domain.Entities
{
    public abstract class BaseEntity
    {
        public virtual int ID { get; set; }
        public virtual string ImagePath { get; set; }
    }
}
