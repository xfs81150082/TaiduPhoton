using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Threading;
using TumoCommon.Model;

namespace TumoPhoton.DB.Manager
{
    public class SkillDBManager
    {
        public void Add(SkillDB skillDb)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(skillDb);
                    transaction.Commit();
                }
            }


        }

        public void Update(SkillDB skillDb)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(skillDb);
                    transaction.Commit();
                }
            }


        }

        public void Upgrade(SkillDB skillDb, Role role)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(skillDb);
                    session.Update(role);
                    transaction.Commit();
                }
            }

        }

        public List<SkillDB> Get(Role role)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var res = session.QueryOver<SkillDB>().Where(x => x.Role == role);
                    transaction.Commit();
                    return (List<SkillDB>) res.List<SkillDB>();
                }
            }

        }

    }
}
