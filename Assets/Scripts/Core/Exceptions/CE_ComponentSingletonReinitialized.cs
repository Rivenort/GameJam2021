
namespace BestGameEver
{
    /// <summary>
    /// In the project is defined a concept of MonoBehavior-Singleton. It is done due to having two
    /// features combined in one class i.e. being a Component (which we can assign to a GameObject) and being a Singleton
    /// (which ensures having static functions). But we cant completly get rid of the problem of having multiple instances of
    /// a Singleton at the same time (due to the MonoBehavior class behavior). So in order to be sure we didn't create an
    /// additional instance in the runtime, we throw the following exception if such event happens.
    /// @author Rivenort
    /// </summary>
    [System.Serializable]
    public class CE_ComponentSingletonReinitialized : System.Exception
    {
        private const string DEFAULT_MESSAGE = "You cannot call Start() more than once! ";

        public CE_ComponentSingletonReinitialized()
            : base(DEFAULT_MESSAGE)
        {
        }

        public CE_ComponentSingletonReinitialized(string message)
            : base(DEFAULT_MESSAGE + message)
        {
        }

        public CE_ComponentSingletonReinitialized(string message, System.Exception innerException)
            : base(DEFAULT_MESSAGE + message, innerException)
        {
        }

    }

}