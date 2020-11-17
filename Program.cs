using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bomber
{
    class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();

            var client = new HttpClient();
            var numberOfRequests = 100;
            var url = "https://localhost:44367/purchase?eventId=1&ticketId=1&numberOfTickets=2";

            char userInput;
            var stopwatch = new Stopwatch();

            do
            {
                stopwatch.Start();
                CommitTheBombardment(url, numberOfRequests, client);

                Console.WriteLine($"Bombing finished. It took {stopwatch.ElapsedMilliseconds}ms. Press any key to repeat or q to exit...");
                
                stopwatch.Stop();
                stopwatch.Reset();

                userInput = Console.ReadKey().KeyChar;
            } while (userInput != 'q');

        }

        private static void CommitTheBombardment(string url, int numberOfRequests, HttpClient httpClient)
        {
            var tasks = new List<Task>();
            Console.WriteLine($"Bombing {url} with {numberOfRequests} request");

            for (int i = 0; i < numberOfRequests; i++)
            {
                var id = i;

                // Console.WriteLine($"Bomber {id} started");

                var task = httpClient.PostAsync(url, new StringContent(""));

                tasks.Add(task.ContinueWith(response => { Console.WriteLine($"Bomber {id} finished"); }));
            }

            Task.WaitAll(tasks.ToArray());
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to the bomber app");

            Console.WriteLine(@"

        __|__
  *---o--(_)--o---*
");

            Console.WriteLine(@"
        --_--
     (  -_    _).
   ( ~       )   )
 (( )  (    )  ()  )
  (.   )) (       )
    ``..     ..``
         | |
       (=| |=)
         | |         
     (../( )\.))
");

            Console.WriteLine("Press any key to start the bombing...");
            Console.ReadKey();
        }
    }
}
