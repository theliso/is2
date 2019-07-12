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
    public class UserProxy : User
    {
        private User u;

        private IContext ctx { set; get; }
        private int idInstitution { set; get; }

        public UserProxy(User usr, IContext context, int idInstitution) : base()
        {
            base.id = usr.id;
            base.institution = null;
            base.authors = null;
            base.registration = null;
            base.reviewers = null;
            base.mail = usr.mail;
            base.name = usr.name;
            this.idInstitution = idInstitution;
            ctx = context;
        }

        public UserProxy(User u, IContext context)
        {
            this.u = u;
            this.ctx = context;
        }

        public override Institution institution {
            get
            {
                if(base.institution == null)
                {
                    UserDataMapper usrMapper = new UserDataMapper(ctx);
                    base.institution = usrMapper.LoadInstitution(this);
                }
                return base.institution;
            }
            set => base.institution = value;
        }

        public override List<Submission> authors {
            get
            {
                if(base.authors == null)
                {
                    UserDataMapper usrMapper = new UserDataMapper(ctx);
                    base.authors = usrMapper.LoadAuthors(this);
                }
                return base.authors;
            }
            set => base.authors = value;
        }

        public override List<Submission> reviewers {
            get
            {
                if(base.reviewers == null)
                {
                    UserDataMapper usrMapper = new UserDataMapper(ctx);
                    base.reviewers = usrMapper.LoadReviewers(this);
                }
                return base.reviewers;
            }
            set => base.reviewers = value;
        }

        public override List<Conference> registration {
            get
            {
                if(base.registration == null)
                {
                    ConferenceDataMapper confMapper = new ConferenceDataMapper(ctx);
                    base.registration = confMapper.LoadConferences(this);
                }
                return base.registration;
            }
            set => base.registration = value;
        }
    }
}
