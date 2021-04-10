using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

using System.Dynamic;

using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace HansenAssignment9
{
    public class Function
    {
       public class Book
        {
            public string name;
        }

        static readonly HttpClient client = new HttpClient();
        public static async Task<string> FunctionHandlerAsync(string input, ILambdaContext context)
        {
            try
            {
                dynamic exp = new ExpandoObject();

                HttpResponseMessage response = await client.GetAsync("https://api.nytimes.com/svc/books/v3/lists/current/" + input + ".json?api-key=dEjlHGWSStNRAd925WGG2cNUS8GoGcxq");
                response.EnsureSuccessStatusCode();
                string res = await response.Content.ReadAsStringAsync();

                
                return res;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }
}
