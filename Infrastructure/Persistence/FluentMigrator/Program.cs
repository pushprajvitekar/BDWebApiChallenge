using DatabaseMigrations.SqlServer;

namespace FluentMigrator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            try
            {
                Database.RunMigrations();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception:{e}");
            }


        }
    }
}
