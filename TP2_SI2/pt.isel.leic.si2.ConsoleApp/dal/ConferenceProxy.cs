using pt.isel.leic.si2.ConsoleApp.concrete;
using pt.isel.leic.si2.ConsoleApp.dal;
using pt.isel.leic.si2.ConsoleApp.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp
{
    public class ConferenceProxy : Conference
    {
        
        private IContext ctx { set; get; }
        private int idPresident { set; get; }

        public ConferenceProxy(Conference conf, IContext ctx, int idPresident) : base()
        {
            base.id = conf.id;
            base.president = null;
            base.minGrade = conf.minGrade;
            base.name = conf.name;
            base.year = conf.year;
            base.realizationDate = conf.realizationDate;
            base.registration = null;
            base.acronym = conf.acronym;
            base.limitDate = conf.limitDate;
            this.ctx = ctx;
            this.idPresident = idPresident;
        }

        public override int minGrade {
            get
            {
                if(base.minGrade == 0)
                {
                    base.minGrade = 50;
                }
                return base.minGrade;
            }
            set =>  base.minGrade = value;
        }

        public override List<User> registration {
            get
            {
                if(base.registration == null)
                {
                    ConferenceDataMapper confMapper = new ConferenceDataMapper(ctx);
                    base.registration = confMapper.LoadAllUsers(this);
                }
                return base.registration;
            }

            set => base.registration = value;
        }

        public override User president {
            get
            {
                if(base.president == null)
                {
                    ConferenceDataMapper userMapper = new ConferenceDataMapper(ctx);
                    base.president = userMapper.LoadPresident(this);
                }
                return base.president;
            }

            set => base.president = value;
        }
    }
}
