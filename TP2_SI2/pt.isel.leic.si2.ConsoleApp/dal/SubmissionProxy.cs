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
    public class SubmissionProxy : Submission
    {
        private IContext ctx { set; get; }

        public SubmissionProxy(Submission sub, IContext context) : base()
        {
            base.id = sub.id;
            base.registration = null;
            base.resume = sub.resume;
            base.state = null;
            base.reviewers = null;
            base.submissionDate = sub.submissionDate;
            ctx = context;
        }

        public override List<User> authors {
            get
            {
                if(base.authors == null)
                {
                    SubmissionDataMapper sdm = new SubmissionDataMapper(ctx);
                    base.authors = sdm.LoadAuthors(this);
                }
                return base.authors;
            }
            set => base.authors = value;
        }
        public override State state {
            get
            {
                if(base.state == null)
                {
                    SubmissionDataMapper subMapper = new SubmissionDataMapper(ctx);
                    base.state = subMapper.LoadState(this);
                }
                return base.state;
            }

            set => base.state = value;
        }
        public override List<User> reviewers {
            get
            {
                if(base.reviewers == null)
                {
                    SubmissionDataMapper subMapper = new SubmissionDataMapper(ctx);
                    base.reviewers = subMapper.LoadReviewers(this);
                }
                return base.reviewers;
            }
            set => base.reviewers = value;
        }
        public override List<Conference> registration {
            get
            {
                if (base.registration == null)
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
