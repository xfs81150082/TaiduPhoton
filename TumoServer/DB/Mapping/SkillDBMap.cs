using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TumoCommon.Model;

namespace TumoPhoton.DB.Mapping
{
    class SkillDBMap : ClassMap<SkillDB>
    {
        public SkillDBMap()
        {
            Id(x => x.Id).Column("id");
            Map(x => x.SkilId).Column("skillid");
            Map(x => x.Level).Column("level");
            References(x => x.Role).Column("roleid");
            Table("skilldb");
        }
    }
}
