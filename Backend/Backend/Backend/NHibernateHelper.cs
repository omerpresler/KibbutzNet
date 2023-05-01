using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using System.Configuration;
using Configuration = NHibernate.Cfg.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Backend;

public static class NHibernateHelper
{
    private static ISessionFactory _sessionFactory;

    public static ISessionFactory GetSessionFactory()
    {
        if (_sessionFactory == null)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["YourConnectionStringName"].ConnectionString;
            var configuration = new Configuration();
            var path= Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            configuration.Configure("C:/Users/rotem/RiderProjects/KibbutzNet/Backend/Backend/Backend/hibernate.cfg.xml");
            _sessionFactory = configuration.BuildSessionFactory();
        }

        return _sessionFactory;
    }
}
