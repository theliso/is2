using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pt.isel.leic.si2.ConsoleApp.domain;

namespace pt.isel.leic.si2.ConsoleApp.dal
{
    public class StateProxy : State
    {
        private IContext context;

        public StateProxy(State s, IContext context) : base()
        {
            base.id = s.id;
            base.description = s.description;
            this.context = context;
        }
    }
}
