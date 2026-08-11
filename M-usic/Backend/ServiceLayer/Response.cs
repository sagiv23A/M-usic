using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ServiceLayer
{
    /// <summary>
    /// This class is responsible for encapsulating the response to actions that have been made, including error information or a returned object.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// The error message that will be shown.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// The return value of the response. If there is an error, a null will be returned; otherwise, the correct object will be returned.
        /// </summary>
        public object? ReturnValue { get; set; }

        /// <summary>
        /// Initializes a new response object with an error message and a return value.
        /// </summary>
        /// <param name="ErrorMessage">The error message that will be shown. Null if no error occurred.</param>
        /// <param name="ReturnValue">The return value of the response. Null if an error occurred.</param>
        /// <precondition> None. </precondition>
        /// <postcondition> A Response object is created with the specified ErrorMessage and ReturnValue. </postcondition>
        public Response(string ErrorMessage, object ReturnValue)
        {
            this.ErrorMessage = ErrorMessage;
            this.ReturnValue = ReturnValue;
        }

        /// <summary>
        /// Initializes a new response object with an error message and a null return value.
        /// </summary>
        /// <param name="ErrorMessage">The error message that will be shown.</param>
        /// <precondition> None. </precondition>
        /// <postcondition> A Response object is created with the specified ErrorMessage and a null ReturnValue. </postcondition>
        public Response(string ErrorMessage)
        {
            this.ErrorMessage = ErrorMessage;
            this.ReturnValue = null;
        }

        /// <summary>
        /// An empty constructor for the Response object.
        /// </summary>
        /// <precondition> None. </precondition>
        /// <postcondition> An empty Response object is created. </postcondition>
        public Response() { }
    }
}