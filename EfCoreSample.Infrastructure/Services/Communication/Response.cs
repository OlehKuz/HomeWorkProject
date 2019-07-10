using EfCoreSample.Doman.Entities;

namespace EfCoreSample.Doman.Communication
{
    public class Response<TEntity> where TEntity:class
    {
        public TEntity Entity { get; private set; }
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        
        private Response(bool success, string message, TEntity project)
        {
            Success = success;
            Message = message;
            Entity = project;
        }
        public Response(TEntity entity) : this(true, string.Empty, entity) { }
        public Response(bool success) : this(success, string.Empty, null) { }

        /// Creates am error response.
        public Response(string message) : this(false, message, null) { }
    }
}

