using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TumoCommon.Model
{
    public class SkillDB
    {
        public virtual int Id { get; set; }
        public virtual int SkilId { get; set; }
        public virtual Role Role { get; set; }
        public virtual int Level { get; set; }

    }
}
