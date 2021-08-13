
namespace BestGameEver
{
    /// <summary>
    /// The following Exception is supposed to be thrown in Start()/Awake() function of the 
    /// component if one of the members is not initialized/set (equals to null).
    /// The reasoning is that those members can be set through unity inspector, thus
    /// someone could forget to do so.
    /// @author Rivenort
    /// </summary>
    [System.Serializable]
    public class CE_ComponentNotFullyInitialized : System.Exception
    {
        private const string DEFAULT_MESSAGE = "Component is not fully initialized!";

        public CE_ComponentNotFullyInitialized()
            : base(DEFAULT_MESSAGE)
        {
        }

        public CE_ComponentNotFullyInitialized(string message)
            : base(message)
        {

        }


        public CE_ComponentNotFullyInitialized(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }


    }

}