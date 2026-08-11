using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.Exceptions
{
    public class MusicException : Exception
    {
        /// <summary>
        /// Initializes a new default instance of the KanbanException class.
        /// </summary>
        /// <precondition>There is no precondition.</precondition>
        /// <postcondition>A new instance of KanbanException is successfully created with default properties.</postcondition>
        public MusicException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the KanbanException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <precondition>There is no precondition.</precondition>
        /// <postcondition>A new instance of KanbanException is successfully created containing the provided error message.</postcondition>
        public MusicException(string message) : base(message)
        {
        }
        /// <summary>
        /// Initializes a new instance of the KanbanException class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <precondition>There is no precondition.</precondition>
        /// <postcondition>A new instance of KanbanException is successfully created with the specified message and inner exception data.</postcondition>
        public MusicException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}