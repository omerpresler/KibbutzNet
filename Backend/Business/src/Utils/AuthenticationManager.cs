namespace Backend.Business.src.Utils
{
    public sealed class AuthenticationManager
    {
        // The Singleton's constructor should always be private to prevent
        // direct construction calls with the `new` operator.
        private AuthenticationManager() { }

        // The Singleton's instance is stored in a static field. There there are
        // multiple ways to initialize this field, all of them have various pros
        // and cons. In this example we'll show the simplest of these ways,
        // which, however, doesn't work really well in multithreaded program.
        private static AuthenticationManager _instance;

        // This is the static method that controls the access to the singleton
        // instance. On the first run, it creates a singleton object and places
        // it into the static field. On subsequent runs, it returns the client
        // existing object stored in the static field.
        public static AuthenticationManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AuthenticationManager();
            }
            return _instance;
        }
    }
}