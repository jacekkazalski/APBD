﻿namespace kol2.Response
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
