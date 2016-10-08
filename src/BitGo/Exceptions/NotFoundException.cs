using System;

namespace BitGo.Exceptions {
    public class NotFoundException: Exception {
        public NotFoundException(string message, Exception innerException = null) : base(message, innerException) {
            
        }
    }
}