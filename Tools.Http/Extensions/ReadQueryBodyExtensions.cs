using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Http.Extensions
{
    public static class ReadQueryBodyExtensions
    {
        public async static Task<T> ReadContentAsAsync<T>(this HttpContext context)
        {
            var initialBody = context.Request.Body;

            context.Request.EnableRewind();
            var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
            await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            var json = Encoding.UTF8.GetString(buffer);

            context.Request.Body = initialBody;

            T retValue = JsonConvert.DeserializeObject<T>(json);
            return retValue;
        }

        public async static Task<string> ReadContentAsString(this HttpRequest request)
        {
            var initialBody = request.Body;

            request.EnableRewind();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var body = Encoding.UTF8.GetString(buffer);
            request.Body = initialBody;
            return body;
        }

        public async static Task<T> ReadContentAsAsync<T>(this HttpRequest request)
        {
            var json = await request.ReadContentAsString();
            T retValue = JsonConvert.DeserializeObject<T>(json);
            return retValue;
        }
    }
}
