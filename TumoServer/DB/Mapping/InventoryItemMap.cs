using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TumoCommon.Model;

namespace TumoPhoton.DB.Mapping
{
    class InventoryItemMap : ClassMap<InventoryItemDB>
    {
        public InventoryItemMap()
        {
            Id(x => x.Id).Column("id");
            Map(x => x.InventoryId).Column("InventoryId");
            Map(x => x.Level).Column("Level");
            Map(x => x.Count).Column("Count");
            Map(x => x.IsDressed).Column("IsDressed");
            References(x => x.Role).Column("roleid");
            Table("inventoryitemdb");
        }


    }
}
