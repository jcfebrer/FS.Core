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
        private IFirebaseClient client { get; set; }

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

        public async Task<T> SetAsync<T>(string path, T data)
        {
            SetResponse response = await client.SetAsync(path, data);
            return response.ResultAs<T>();
        }

        public async void PushAsync<T>(string path, T data)
        {
            PushResponse response = await client.PushAsync(path, data);
            Console.WriteLine(response.Result.name);
        }

        public async Task<T> GetAsync<T>(string path)
        {
            FirebaseResponse response = await client.GetAsync(path);
            return response.ResultAs<T>();
        }

        public async Task<T> UpdateAsync<T>(string path, T data)
        {
            FirebaseResponse response = await client.UpdateAsync(path, data);
            return response.ResultAs<T>();
        }

        public async Task<HttpStatusCode> DeleteAsync(string path)
        {
            FirebaseResponse response = await client.DeleteAsync(path);
            return response.StatusCode;
        }


        public T Set<T>(string path, T data)
        {
            SetResponse response = client.Set(path, data);
            return response.ResultAs<T>();
        }

        public string Push<T>(string path, T data)
        {
            PushResponse response = client.Push(path, data);
            return response.Result.name;
        }

        public virtual T Get<T>(string path)
        {
            FirebaseResponse response = client.Get(path);
            return response.ResultAs<T>();
        }

        public T Update<T>(string path, T data)
        {
            FirebaseResponse response = client.Update(path, data);
            return response.ResultAs<T>();
        }

        public HttpStatusCode Delete(string path)
        {
            FirebaseResponse response = client.Delete(path);
            return response.StatusCode;
        }

        public async void Listen(string path)
        {
            EventStreamResponse response = await client.OnAsync(path, (sender, args, context) => {
                System.Console.WriteLine(args.Data);
            });

            response.Dispose();
        }
    }
}
