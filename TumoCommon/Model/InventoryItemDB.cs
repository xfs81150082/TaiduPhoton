using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TumoCommon.Model
{
    public class InventoryItemDB
    {
        public virtual int Id { get; set; }
        public virtual int InventoryId { get; set; }
        public virtual int Level { get; set; }
        public virtual int Count { get; set; }
        public virtual bool IsDressed { get; set; }
        public virtual Role Role { get; set; }

    }
}
