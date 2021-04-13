using System;
using System.IO;
using DbUp;

namespace DoIt.DbMigrator
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No connection string supplied");
                Console.ResetColor();
                return 1;
            }


            var connectionString = args[0];
            var path = args[1] ?? Path.Combine(Environment.CurrentDirectory, "Sql");
            var dropIfExists = args.Length > 1 || args[2] == "drop-if-exists";

            return RunDbUpdate(connectionString, path, dropIfExists);
        }
        
        private static int RunDbUpdate(string connectionString, string path, bool dropIfExists)
        {
            if (dropIfExists)
            {
                EnsureDatabase.For.PostgresqlDatabase(connectionString);
            }
            var upgradeEngine = DeployChanges.To
                .PostgresqlDatabase(connectionString)
                .WithScriptsFromFileSystem(path)
                .LogToConsole()
                .Build();

            var upgradeResult = upgradeEngine.PerformUpgrade();
            if (!upgradeResult.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(upgradeResult.Error);
                Console.ResetColor();
                return 1;
            }
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
    }
}