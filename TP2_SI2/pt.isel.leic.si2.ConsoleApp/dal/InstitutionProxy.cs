using pt.isel.leic.si2.ConsoleApp.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp.dal
{
    public class InstitutionProxy : Institution
    {

        private IContext ctx;

        public InstitutionProxy(Institution inst, IContext context) : base()
        {
            base.id = inst.id;
            base.name = inst.name;
            base.address = inst.address;
            base.country = inst.country;
            this.ctx = context;
        }
    }
}
