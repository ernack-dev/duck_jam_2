using System;

namespace Duck_Jam_2
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Neptunia())
                
                game.Run();

        }
    }
}
