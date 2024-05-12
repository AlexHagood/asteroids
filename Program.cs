// See https://aka.ms/new-console-template for more information
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;


namespace asteroids
{
class Program
{
    static void Main(string[] args)
    {

    
        Console.WriteLine("Drawing window!");


            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(1600, 1200),
                Title = "Asteroids 2",
            };

            using (var window = new Window(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
    

    }
}
}


