
namespace BestGameEver
{
    /// <summary>
    /// Should be raised if we detect that the object that is needed for processing wasn't
    /// properly initialized before. The following exception is more 'generic' i.e. involves all 
    /// types: C#-Object, Unity-Object, Unity-GameObject etc.
    /// Also <see cref="CE_ExpectedElementNotFound"/>
    /// Also <see cref="CE_ComponentNotFullyInitialized"/>
    /// @author Rivenort
    /// </summary>
    [System.Serializable]
    public class CE_RequiredObjectNotInitialized : System.Exception
    {
        private const string DEFAULT_MESSAGE = "Required object is not initialized.";

        public CE_RequiredObjectNotInitialized()
            : base(DEFAULT_MESSAGE)
        {
        }

        public CE_RequiredObjectNotInitialized(string message)
            : base(message)
        {
        }


        public CE_RequiredObjectNotInitialized(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

    }

}