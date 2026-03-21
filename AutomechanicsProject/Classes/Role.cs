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
            public virtual ICollection<Users> Users { get; set; } = new List<Users>();
            public override string ToString()
            {
                return Position;
            }
        }
    }
}
