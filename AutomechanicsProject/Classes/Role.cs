using System;
using System.Collections.Generic;

namespace AutomechanicsProject.Classes
{
    using System;
    using System.Collections.Generic;

    namespace AutomechanicsProject.Classes
    {
        public class Role
        {
            public Guid Id { get; set; }
            public string Position { get; set; }
            public virtual ICollection<User> Users { get; set; } = new List<User>();
            public override string ToString()
            {
                return Position;
            }
        }
    }
}
