using System;

namespace BestGameEver
{
    /// <summary>
    /// The following exception occures whenever game code cannot find specifed
    /// element (e.g. gameobject/component) in the scene.
    /// @author Rivenort
    /// </summary>
    [System.Serializable]
    public class CE_ExpectedElementNotFound : System.Exception
    {
        private const string DEFAULT_MESSAGE = "Couldn't find a specifed element! ";

        public CE_ExpectedElementNotFound()
            : base(DEFAULT_MESSAGE)
        {

        }

        public CE_ExpectedElementNotFound(string message)
            : base(DEFAULT_MESSAGE + message)
        {
        }

        public CE_ExpectedElementNotFound(string message, System.Exception innerException)
            : base(DEFAULT_MESSAGE + message, innerException)
        {
        }

    }

}