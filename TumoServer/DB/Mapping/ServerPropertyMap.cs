using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TumoCommon.Model;

namespace TumoPhoton.DB.Mapping
{
    class ServerPropertyMap : ClassMap<ServerProperty>
    {
        public ServerPropertyMap()
        {
            Id(x => x.Id).Column("id");
            Map(x => x.Name).Column("name");
            Map(x => x.Ip).Column("ip");
            Map(x => x.Count).Column("count");
            Table("serverproperty"); 
        }
        
    }
}
