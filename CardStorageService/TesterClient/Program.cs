using CardStorageService.Utils;

namespace TesterClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result = PasswordUtils.CreatePasswordHash("0987654321");
            Console.WriteLine(result.passwordSalt);
            Console.WriteLine(result.passwordHash);
            Console.ReadKey();
        }
    }
}