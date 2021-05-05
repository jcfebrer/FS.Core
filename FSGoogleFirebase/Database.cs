using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FSGoogleFirebase
{
    /// <summary>
    /// Pruebas con firebase database sin finalizar
    /// </summary>
    public class Database
    {
        IFirebaseClient client;

        public Database()
        { }

        public Database(string basePath, string authSecret)
        {
            Init(basePath, authSecret);
        }

        public void Init(string basePath, string authSecret)
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = authSecret,
                BasePath = basePath // "https://yourfirebase.firebaseio.com/"
            };

            client = new FirebaseClient(config);
        }

        public async void SetAsync(string path)
        {
            var register = new Register
            {
                name = "Execute SET",
                value = "2"
            };
            SetResponse response = await client.SetAsync(path, register);
            Register result = response.ResultAs<Register>(); //The response will contain the data written
        }

        public async void PushAsync(string path)
        {
            var register = new Register
            {
                name = "Execute PUSH",
                value = "2"
            };
            PushResponse response = await client.PushAsync(path, register);
            Console.WriteLine(response.Result.name); //The result will contain the child name of the new data that was added
        }

        public async Task<Register> GetAsync(string path)
        {
            FirebaseResponse response = await client.GetAsync(path);
            return response.ResultAs<Register>(); //The response will contain the data being retreived
        }

        public async void UpdateAsync(string path)
        {
            var register = new Register
            {
                name = "Execute UPDATE!",
                value = "1"
            };

            FirebaseResponse response = await client.UpdateAsync(path, register);
            register = response.ResultAs<Register>(); //The response will contain the data written
        }

        public async void DeleteAsync(string path)
        {
            FirebaseResponse response = await client.DeleteAsync(path); //Deletes todos collection
            Console.WriteLine(response.StatusCode);
        }


        public void Set(string path)
        {
            var register = new Register
            {
                name = "Execute SET",
                value = "2"
            };
            SetResponse response = client.Set(path, register);
            Register result = response.ResultAs<Register>(); //The response will contain the data written
        }

        public void Push(string path)
        {
            var register = new Register
            {
                name = "Execute PUSH",
                value = "2"
            };
            PushResponse response = client.Push(path, register);
            Console.WriteLine(response.Result.name); //The result will contain the child name of the new data that was added
        }

        public Register Get(string path)
        {
            FirebaseResponse response = client.Get(path);
            return response.ResultAs<Register>(); //The response will contain the data being retreived
        }

        public void Update(string path)
        {
            var register = new Register
            {
                name = "Execute UPDATE!",
                value = "1"
            };

            FirebaseResponse response = client.Update(path, register);
            register = response.ResultAs<Register>(); //The response will contain the data written
        }

        public void Delete(string path)
        {
            FirebaseResponse response = client.Delete(path); //Deletes todos collection
            Console.WriteLine(response.StatusCode);
        }

        public async void Listen(string path)
        {
            EventStreamResponse response = await client.OnAsync(path, (sender, args, context) => {
                System.Console.WriteLine(args.Data);
            });

            //Call dispose to stop listening for events
            response.Dispose();
        }
    }

    public class Register
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
