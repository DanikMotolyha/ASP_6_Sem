using System;
using System.IO;
using System.Text;

namespace task4
{
    class Program
    {

        const string PATH_TO_API = "http://localhost:12243/task4";
        public static void sendPost()
        {
            Console.WriteLine("2. Test Post Query");
            Console.Write("Enter A param: ");
            int FirstNum = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter B param: ");
            int SecondNum = Convert.ToInt32(Console.ReadLine());

            var request = System.Net.WebRequest.Create(PATH_TO_API + "?x=" +
                                    Convert.ToInt32(FirstNum) + "&y=" +
                                    Convert.ToInt32(SecondNum));
            request.Method = "POST";
            request.ContentLength = 0;
            var response = request.GetResponse();

            using (var stream = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException(), Encoding.UTF8))
            {
                Console.WriteLine("Res: ");
                Console.WriteLine(stream.ReadToEnd());
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            sendPost();
        }
        
    }
}
