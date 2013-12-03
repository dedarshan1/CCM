using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using FluentNHibernate;

namespace CCM.DAL
{
    public class NHibernateSessionProvider
    {
        private static ISessionFactory sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                    sessionFactory = SetSessionFactory();
                return sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            ISession s = SessionFactory.OpenSession();
            return s;
        }

        public static ISessionFactory SetSessionFactory()
        {
            // Set default to SQL 2005 database                            
            var config = Fluently.Configure()
                //.Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.("EmployeeConnectConnectionString")))
                                         .Database(MsSqlConfiguration.MsSql2008.ConnectionString("Integrated Security=True;Initial Catalog=CCM;Data Source=DARSHANDESAI-PC\\SQLEXPRESS"))
                                         .ExposeConfiguration(c => c.SetProperty(NHibernate.Cfg.Environment.ProxyFactoryFactoryClass, typeof(NHibernate.ByteCode.Castle.ProxyFactoryFactory).AssemblyQualifiedName))
                                         .ExposeConfiguration(c => c.SetProperty(NHibernate.Cfg.Environment.ReleaseConnections, "on_close"))
                                         .BuildConfiguration();

            var persistenceModel = new PersistenceModel();
            //persistenceModel.AddMappingsFromAssembly(typeof(EmployeeMap).Assembly);
            //persistenceModel.AddMappingsFromAssembly(typeof(RequestStatusMap).Assembly);
            persistenceModel.Configure(config);

            sessionFactory = config.BuildSessionFactory();

            return sessionFactory;
        }

    }
}
