using System;
namespace QPlanAPI.Core.DTO
{
    public abstract class UseCaseResponse
    {
        public bool Success { get; }
        public string Message { get; }

        protected UseCaseResponse(bool success = false, string message = null)
        {
            Success = success;
            Message = message;
        }
    }
}
