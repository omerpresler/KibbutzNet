using NHibernate;
using NHibernate.Cfg;

namespace Backend;

public static class NHibernateHelper
{
    private static ISessionFactory _sessionFactory;

    public static ISessionFactory GetSessionFactory()
    {
        if (_sessionFactory == null)
        {
            var configuration = new Configuration();
            configuration.Configure("hibernate.cfg.xml");
            _sessionFactory = configuration.BuildSessionFactory();
        }

        return _sessionFactory;
    }
}
