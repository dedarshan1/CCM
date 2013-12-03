using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;

namespace CCM.DAL
{
    public class RepositoryBase<T> : NHibernateSessionProvider where T : class
    {
        public virtual T Get(int id)
        {
            using (ISession session = NHibernateSessionProvider.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                var a = session.Get<T>(id);
                return a;
            }

        }

        public virtual IQueryable<T> GetQuery()
        {
            using (ISession session = NHibernateSessionProvider.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                var query = session.Query<T>();
                return query;
            }
        }

        public virtual void Save(T obj)
        {
            using (ISession session = NHibernateSessionProvider.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(obj);
                transaction.Commit();
            }

        }

    }
}
