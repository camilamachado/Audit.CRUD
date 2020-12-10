using Audit.CRUD.Sample.Auth.Entities;
using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace Audit.CRUD.Sample.Auth
{
    public class AppClientsStore
    {
        public static ConcurrentDictionary<string, AppClient> AppClientsList = new ConcurrentDictionary<string, AppClient>();

        static AppClientsStore()
        {
            AppClientsList.TryAdd("099153c2625149bc8ecb3e85e03f0026",
                                   new AppClient
                                   {
                                       ClientId = "099153c2625149bc8ecb3e85e03f0026",
                                       Base64Secret = "nbwQ3HDjLNvOnuNyQkBxADEVEwGBEovFZKakYoBQRQo",
                                       Name = "Audit.CRUD.Sample.WebApi"
                                   });
        }

        public static AppClient AddClient(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = Convert.ToBase64String(key);

            AppClient newAudience = new AppClient { ClientId = clientId, Base64Secret = base64Secret, Name = name };
            AppClientsList.TryAdd(clientId, newAudience);
            return newAudience;
        }

        public static AppClient FindClient(string clientId)
        {
            AppClient audience = null;
            if (AppClientsList.TryGetValue(clientId, out audience))
            {
                return audience;
            }
            return null;
        }
    }
}