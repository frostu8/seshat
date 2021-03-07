namespace Seshat
{
    /// <summary>
    /// Helper class for singleton types, like registrars.
    /// </summary>
    /// <typeparam name="T">A class to make a singleton.</typeparam>
    public class Singleton<T> where T: class, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new T();
                return _instance;
            }
        }
    }
}
