
using System;

namespace BestGameEver
{
    /// <summary>
    /// The following exception is called whether we are calling a Singleton's static function while
    /// Singleton wasn't initialized before. Initialization is done by GetInstance() static function.
    /// <para>
    /// We do not the 'Lazy Initialization' which assumes the instance initialization exactly when the instance
    /// is needed for processing. We rather want to have already initialized Singleton at the game start.
    /// We don't want to use main loop processing time and causing game unstable.
    /// </para>
    /// @author Rivenort
    /// </summary>
    [System.Serializable]
    public class CE_SingletonNotInitialized : System.Exception
    {
        private const string DEFAULT_MESSAGE = "Singleton wasn't initialized before call! ";

        public CE_SingletonNotInitialized()
            : base(DEFAULT_MESSAGE)
        {
        }

        public CE_SingletonNotInitialized(string message)
            : base(DEFAULT_MESSAGE + message)
        {
        }

        public CE_SingletonNotInitialized(string message, System.Exception innerException)
            : base(DEFAULT_MESSAGE + message, innerException)
        {
        }

    }

}