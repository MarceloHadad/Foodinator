namespace Foodinator
{
    static class Program
    {
        static void Main()
        {
            try
            {
                IGame game = new Game(new DishRepository());
                game.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine($"[ERROR] {e.Message}");
            }
        }
    }
}
