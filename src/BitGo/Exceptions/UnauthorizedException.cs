using System;

namespace BitGo.Exceptions {
    public class UnauthorizedException: Exception {
        public UnauthorizedException(string message, Exception innerException = null) : base(message, innerException) {
            
        }
    }
}