using System;

namespace BeeSouls
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var tmp = ScoreManager.LoadScore();
            tmp.AddScore("tst", 1222);
            tmp.SaveScores();

            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
