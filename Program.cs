using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using Newtonsoft;
using Newtonsoft.Json;

namespace httprequest_api
{
    internal class Program
    {
        const string BaseUrl = "http://127.0.0.1:5000/api";

        // the client we're going to use during this test 
        static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            // ok let's do some tests
            // 1- Let's try to get data from our API:
            // To do so, we'll make an async method to connect and receive the data.

            // Ok first try 
            //string container = await Request(BaseUrl);
            // It did not work because i forgot to change the port of communication to 5000 in the base url
            // 2nd try
            //string container = await GetRequest(BaseUrl);
            // This time i could successfully have access to the data in json format

            // now let's try to send a joyful message to our api with this costumed method

            // 1st try failed. why ? Because i cant read of course.
            // 7th try the charm. After some research, you have to wrap the json data in a string content. After that, you must prepare
            // the header with the encode type and media type. I chose UTF8 and application/json (still don't really know what they mean)
            //string message = "Je suis un client et j'essais de communiquer avec le server";
            //HttpContent ServerAnsw = await PostRequest(BaseUrl, message);


            // Brand new day brand new me. Today we're working on the interface and data format.
            // Let's ask our user what he want to do with our GUI and decide what to do with a switch case 
            int choice = Interface();

            switch (choice)
            {
                case 1:
                    // if he want to sign up, we display the sign up page and get his informations
                    break;

                case 2:
                    break;
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Does a GET request to the API and return the data as a string 
        /// </summary>
        /// <param name="url">target url of the API</param>
        /// <returns>the data from the api</returns>
        static async Task<string> GetRequest(string url)
        {
            string respond = await client.GetStringAsync(url);
            Console.WriteLine(respond);
            Console.ReadLine();
            return respond;
        }


        /// <summary>
        /// Send the given data to the url after turning the data into json strings and wrapping it with the http content class
        /// the method then check the api answer
        /// </summary>
        /// <param name="url">target url</param>
        /// <param name="data">data to send</param>
        /// <returns>something random. just there to take place</returns>
        static async Task<HttpContent> PostRequest(string url, object data)
        {
            // Change the data into json string
            string JsonData = JsonConvert.SerializeObject(data);
            // prepare the http content to send
            HttpContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            // Begin the request and wai for an answer
            HttpResponseMessage Response = await client.PostAsync(BaseUrl, content);
            // check the answer without throwing exception
            if (Response.IsSuccessStatusCode)
            {
                string JSONrespmessage = await Response.Content.ReadAsStringAsync();
                string respmessage = JsonConvert.DeserializeObject<string>(JSONrespmessage);
                Console.WriteLine("Le code est ok \n le message est : " + respmessage);
            }
            else
            {
                Console.Write(Response.ReasonPhrase);
            }
            // just junk
            return Response.Content;
        }

        /// <summary>
        /// Display a multi choice interface and return the int choice
        /// </summary>
        /// <returns>number of the selected option</returns>
        static int Interface()
        {
            Console.WriteLine("1- Sign Up \n2- Log In");
            Console.Write("Option: ");
            return int.Parse(Console.ReadLine());
        }

        /// <summary>
        /// Load the sign up page and get the user's informations to format it in a dictionary
        /// </summary>
        /// <returns> A dictionary with the user's informations</returns>
        static Dictionary<string, string> SignUpPage()
        {
            Console.Clear();
            Console.WriteLine("==========Sign Up page==========\n");
            Console.Write("mail: ");
            string usermail = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Dictionary<string, string> information = new Dictionary<string, string>();
            information.Add(password, usermail);
            return information;
        }

        // Look under for more thought 
    }
}



// Undercode diary:
// 1-
// Now we can safely communicate with the API and getting data from it
// Now we have to make some change to allow to API to get data from the user,
// a 2 way tunnel

// 2- 
// Holy shit i did it. I made the tunnel.
// With those two method and an api we can begin to code the authentification with API system.
// Prelude : The API (done)
// Chapter 1: The interface (begin)... (2025-12-19)

// 3-
//We can begin the multi choice interface to log in and sign up
// we'll no extra because we're on a console.