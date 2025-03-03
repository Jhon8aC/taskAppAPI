﻿
namespace Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {

        }
        // Constructor to initialize the response with data
        public Response(T data)
        {
            Succeeded = true;
            Data = data;
        }
        // Constructor to initialize the response with data and a message
        public Response(T data, string message)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        // Constructor to initialize the response with an error message when the operation fails
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }

    }
}
