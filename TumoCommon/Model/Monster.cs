using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TumoCommon.Model
{
    public class Monster
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Level { get; set; }
        public virtual bool IsMan { get; set; }
        public virtual User User { get; set; }
        public virtual int Exp { get; set; }
        public virtual int Diamond { get; set; }
        public virtual int Coin { get; set; }
        public virtual int Energy { get; set; }
        public virtual int Toughen { get; set; }


    }
}
