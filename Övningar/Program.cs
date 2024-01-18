using System.Net;
using System.Net.Sockets;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Övningar;

class Program
{
    static void Main(string[] args)
    {
        SocketExercise3.StartClient();
    }
}

public class DbExercise1
{
    public static void Start()
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017/db-exercise-1");
        IMongoDatabase db = client.GetDatabase("db-exercise-1");

        try
        {
            var result = db.RunCommand((Command<BsonDocument>)"{ping:1}");
            bool success = result["ok"].AsDouble == 1.0;
            if (success)
            {
                Console.WriteLine("Database connection is good!");
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Database connection failed!");
            Console.WriteLine(exception.Message);
        }
    }
}

public class Movie
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Title { get; set; }
    public double Rating { get; set; }
    public int Length { get; set; }
    public List<string> Actors { get; set; }

    public Movie(string title, double rating, int length)
    {
        this.Title = title;
        this.Rating = rating;
        this.Length = length;
        this.Actors = new List<string>();
    }
}

public class DbExercise2
{
    public static void Start()
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017/db-exercise-movies");
        IMongoDatabase db = client.GetDatabase("db-exercise-movies");
        IMongoCollection<Movie> moviesCollection = db.GetCollection<Movie>("movies");
    }
}

public class DbExercise3
{
    public static void Start()
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017/db-exercise-movies");
        IMongoDatabase db = client.GetDatabase("db-exercise-movies");
        IMongoCollection<Movie> moviesCollection = db.GetCollection<Movie>("movies");

        Movie movie = new Movie("Lord of the Rings", 8.0, 300);
        movie.Actors.Add("A");
        movie.Actors.Add("B");
        movie.Actors.Add("C");

        moviesCollection.InsertOne(movie);
    }
}

public class DbExercise4
{
    public static void Start()
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017/db-exercise-movies");
        IMongoDatabase db = client.GetDatabase("db-exercise-movies");
        IMongoCollection<Movie> moviesCollection = db.GetCollection<Movie>("movies");

        List<Movie> movies = new List<Movie>();
        movies.Add(new Movie("Ironman 1", 8.0, 180));
        movies.Add(new Movie("Avengers", 9.0, 100));
        movies.Add(new Movie("Superman", 5.0, 340));

        moviesCollection.InsertMany(movies);
    }
}

public class DbExercise5
{
    public static void Start()
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017/db-exercise-movies");
        IMongoDatabase db = client.GetDatabase("db-exercise-movies");
        IMongoCollection<Movie> moviesCollection = db.GetCollection<Movie>("movies");

        var filter = Builders<Movie>.Filter.Empty;
        List<Movie> movies = moviesCollection.Find(filter).ToList();

        foreach (Movie movie in movies)
        {
            Console.WriteLine($"Movie:\n Title: {movie.Title}\n Rating: {movie.Rating}");
        }
    }
}

public class DbExercise6
{
    public static void Start()
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017/db-exercise-movies");
        IMongoDatabase db = client.GetDatabase("db-exercise-movies");
        IMongoCollection<Movie> moviesCollection = db.GetCollection<Movie>("movies");

        Console.WriteLine("Please enter your search input:");
        string input = Console.ReadLine() ?? "";

        var filter = Builders<Movie>.Filter.Where(movie => movie.Title.Contains(input));
        List<Movie> movies = moviesCollection.Find(filter).ToList();
        if (movies.Count == 0)
        {
            Console.WriteLine($"Search results for {input} returned empty.");
            return;
        }

        Console.WriteLine($"Search results for {input}:");
        foreach (Movie movie in movies)
        {
            Console.WriteLine($"Movie:\n Title: {movie.Title}\n Rating: {movie.Rating}");
        }
    }
}

public class DbExercise7
{
    public static void Start()
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017/db-exercise-movies");
        IMongoDatabase db = client.GetDatabase("db-exercise-movies");
        IMongoCollection<Movie> moviesCollection = db.GetCollection<Movie>("movies");

        Console.WriteLine("Please enter your search input:");
        string input = Console.ReadLine() ?? "";
        double rating = Double.Parse(input);

        var filter = Builders<Movie>.Filter.Gt(movie => movie.Rating, rating);
        List<Movie> movies = moviesCollection.Find(filter).ToList();
        if (movies.Count == 0)
        {
            Console.WriteLine($"Search results for {input} returned empty.");
            return;
        }

        Console.WriteLine($"Search results for {input}:");
        foreach (Movie movie in movies)
        {
            Console.WriteLine($"Movie:\n Title: {movie.Title}\n Rating: {movie.Rating}");
        }
    }
}

// Borttagen (se boken)
public class DbExercise8 { }

public class DbExercise9
{
    public static void Start()
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017/db-exercise-movies");
        IMongoDatabase db = client.GetDatabase("db-exercise-movies");
        IMongoCollection<Movie> moviesCollection = db.GetCollection<Movie>("movies");

        // set
        var filter = Builders<Movie>.Filter.Eq(movie => movie.Title, "Ironman 1");
        var update = Builders<Movie>.Update.Set(movie => movie.Rating, 7.0);
        var result = moviesCollection.UpdateOne(filter, update);

        // inc
        var filter2 = Builders<Movie>.Filter.Eq(movie => movie.Title, "Superman");
        var update2 = Builders<Movie>.Update.Inc(movie => movie.Rating, 1);
        var result2 = moviesCollection.UpdateOne(filter2, update2);
    }
}

public class DbExercise10
{
    public static void Start()
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017/db-exercise-movies");
        IMongoDatabase db = client.GetDatabase("db-exercise-movies");
        IMongoCollection<Movie> moviesCollection = db.GetCollection<Movie>("movies");

        var filter = Builders<Movie>.Filter.Eq(movie => movie.Rating, 8);
        var result = moviesCollection.DeleteOne(filter);
    }
}

public class DbExercise11
{
    public static void Start()
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017/db-exercise-movies");
        IMongoDatabase db = client.GetDatabase("db-exercise-movies");
        IMongoCollection<Movie> moviesCollection = db.GetCollection<Movie>("movies");

        var filter = Builders<Movie>.Filter.Eq(movie => movie.Title, "Ironman 1");
        var update = Builders<Movie>.Update.Push(movie => movie.Actors, "Robert Downey Jr");
        var result = moviesCollection.UpdateOne(filter, update);
    }
}

public class DbExercise12
{
    public static void Start()
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017/db-exercise-movies");
        IMongoDatabase db = client.GetDatabase("db-exercise-movies");
        IMongoCollection<Movie> moviesCollection = db.GetCollection<Movie>("movies");

        Console.WriteLine("Please enter your search input:");
        string input = Console.ReadLine() ?? "";

        var filter = Builders<Movie>.Filter.AnyEq(movie => movie.Actors, input);

        List<Movie> movies = moviesCollection.Find(filter).ToList();
        if (movies.Count == 0)
        {
            Console.WriteLine($"Search results for {input} returned empty.");
            return;
        }

        Console.WriteLine($"Search results for {input}:");
        foreach (Movie movie in movies)
        {
            Console.WriteLine($"Movie:\n Title: {movie.Title}\n Rating: {movie.Rating}");
        }
    }
}

public class SocketExercise1
{
    public static void Start()
    {
        IPAddress ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
        IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, 25000);

        Socket serverSocket = new Socket(
            ipAddress.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        serverSocket.Bind(ipEndpoint);
        serverSocket.Listen();

        Socket clientSocket = serverSocket.Accept();
        Console.WriteLine("A client has connected!");

        byte[] buffer = new byte[10000];
        int read = clientSocket.Receive(buffer);

        string content = System.Text.Encoding.UTF8.GetString(buffer, 0, read);
        Console.WriteLine(content);
    }
}

// Innehåller kommentarer som jämför sockets med telefoner.
public class SocketExercise2
{
    public static void StartServer()
    {
        // Bestämmer vilket telefonnummer vi vill ha.
        // Telefonnummer = ip address
        IPAddress ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
        IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, 25000);

        // Öppna upp telefonen.
        Socket serverSocket = new Socket(
            ipAddress.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        // Kopplar våran telefon till telefonnummret (som vi valde ovanför).
        serverSocket.Bind(ipEndpoint);

        // Tillåter att andra kan ringa till telefonen.
        serverSocket.Listen();

        // Väntar på att någon ska ringa.
        Socket clientSocket = serverSocket.Accept();
        Console.WriteLine("A client has connected!");

        // Prata med den andra personen som har ringt.
        byte[] buffer = new byte[10000];
        int read = clientSocket.Receive(buffer); // Lyssna på vad den andra personen säger

        clientSocket.Send(buffer[0..read]); // Säg något till den andra personen
    }

    public static void StartClient()
    {
        // Bestämmer vilket telefonnummer vi vill ringa till.
        IPAddress ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
        IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, 25000);

        // Öppna upp telefonen.
        Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        // Ring till telefonnummret och vänta på koppling.
        socket.Connect(ipEndpoint);

        // Börja prata med den andra personen.
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Hej!");
        socket.Send(buffer); // Säg något till den andra personen.

        buffer = new byte[10000];
        int read = socket.Receive(buffer); // Lyssna på vad den andra personen säger.

        string content = System.Text.Encoding.UTF8.GetString(buffer, 0, read);
        Console.WriteLine("Echo: " + content);
    }
}

public class SocketExercise3
{
    public static void StartServer()
    {
        IPAddress ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
        IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, 25000);

        Socket serverSocket = new Socket(
            ipAddress.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        serverSocket.Bind(ipEndpoint);
        serverSocket.Listen();

        Socket clientSocket = serverSocket.Accept();
        Console.WriteLine("A client has connected!");

        while (true)
        {
            byte[] buffer = new byte[10000];
            int read = clientSocket.Receive(buffer);

            clientSocket.Send(buffer[0..read]);
        }
    }

    public static void StartClient()
    {
        IPAddress ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
        IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, 25000);

        Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        socket.Connect(ipEndpoint);

        while (true)
        {
            string input = Console.ReadLine() ?? "";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(input);
            socket.Send(buffer);

            buffer = new byte[10000];
            int read = socket.Receive(buffer);

            string content = System.Text.Encoding.UTF8.GetString(buffer, 0, read);
            Console.WriteLine("Echo: " + content);
        }
    }
}
