using System;
using System.Collections.Generic;
using System.Text;

namespace TumoCommon.Model
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
    }
}
