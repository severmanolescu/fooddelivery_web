using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using Firebase.Database;

namespace FirebaseDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Initialize the Firebase client
            var firebaseClient = new FirebaseClient("https://fooddelivery-564e8-default-rtdb.firebaseio.com/");

            // Retrieve data from the "users" node in the database
            var users = await firebaseClient.Child("qgT6LlkQ1pRmJDN9HXSxRWkBhzA2qgT6LlkQ1pRmJDN9HXSxRWkBhzA2").OnceAsync<Orders>();

            // Loop through the data and print it to the console
            foreach (var user in users)
            {
                Console.WriteLine($"User ID: {user.Key}");
            }
        }
    }
}

public class Orders
{
    public List<User> orders;
}

public class User
{
    public DateTime Date { get; set; }
    public List<string> Items { get; set; }
    public string Status { get; set; }
}