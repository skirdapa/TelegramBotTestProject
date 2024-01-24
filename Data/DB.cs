using TelegramBotTestProject.Data.Table;

namespace TelegramBotTestProject.Data;
using SQLite;

public static class DB
{
    
    private static SQLiteConnection connection;
    private static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB");

    static DB()
    {
        connection = new SQLiteConnection(path);
        connection.CreateTable<Users>();
        Console.WriteLine(connection.Table<Users>().ToString());
    }

    public static List<Users> GetUsers()
    {
        return connection.Table<Users>().ToList();
    }

    public static int InsertUsers(Users user)
    {
        try
        {
            return connection.Insert(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
        }
    }

    public static Users? FindUser(long id)
    {
        return connection.Find<Users>(id);
    }
}