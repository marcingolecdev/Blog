using Blazored.SessionStorage;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blog.Client.Services
{
    public static class SessionStorageServiceExtension
    {
        public static async Task SaveItemEncryptedAsync<T> (this ISessionStorageService sessionStorageService, string key, T item)
        {
            var itemJson = JsonSerializer.Serialize (item);
            var itemJsonBytes = Encoding.UTF8.GetBytes (itemJson);
            var base64Json = Convert.ToBase64String (itemJsonBytes);
            await sessionStorageService.SetItemAsync(key, base64Json);
        }

        public static async Task<T> ReadEncryptedItemAsync<T> (this ISessionStorageService sessionStorageService, string key)
        {
            var base64Json = await sessionStorageService.GetItemAsync <string> (key);
            var itemJsonBytes = Convert.FromBase64String (base64Json);
            var itemJson = Encoding.UTF8.GetString (itemJsonBytes);
            var item = JsonSerializer.Deserialize <T> (itemJson);

            return item;
        }
    }
}

/*
A class that contains two methods responsible for adding information about the logged in user
user into memory with the "SetItemAsync()" method, this method is a method provided by
"ISessionStorageService", which requires the additional NuGet package "Blazored.SessionStorage" to be installed.
In the first method, the data is compressed before being added to memory. This reduces their size
and speeds up the application a bit. The methods used are standard methods in C#, but to
To use them, add their headers in the using section. For example, "using System.Text.Json".

Another method "ReadEncryptedItemAsync()" is reading information from the memory about the logged in user.
The use of these methods is related to the user's login and logout to the system.
*/