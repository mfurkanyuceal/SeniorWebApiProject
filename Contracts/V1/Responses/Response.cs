namespace SeniorWepApiProject.Contracts.V1.Responses
{
    public class Response<T>
    {
        public T Data { get; set; }

        public Response()
        {
        }

        public Response(T response)
        {
            Data = response;
        }
    }
}