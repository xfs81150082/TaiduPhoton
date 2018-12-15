using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TumoCommon.Model;

namespace TumoPhoton.DB.Mapping
{
    class TaskDBMap : ClassMap<TaskDB>
    {
        public TaskDBMap()
        {
            Id(x => x.Id).Column("id");
            Map(x => x.LastUpdateTime).Column("lastupdatetime");
            Map(x => x.State).Column("state");
            Map(x => x.TaskId).Column("taskid");
            Map(x => x.Type).Column("type");
            References(x => x.Role).Column("roleid");
            Table("taskdb");
        }



    }
}
