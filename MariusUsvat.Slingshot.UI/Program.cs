using System;

namespace MariusUsvat.Slingshot.UI
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
            using (var game = new SlingshotGame())
                game.Run();
        }
    }
#endif
}
