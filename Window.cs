using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;



namespace asteroids
{
    // This is where all OpenGL code will be written.
    // OpenToolkit allows for several functions to be overriden to extend functionality; this is how we'll be writing code.
    public class Window : GameWindow
    {
        // A simple constructor to let us set properties like window size, title, FPS, etc. on the window.
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            gameWindowSettings.UpdateFrequency = 10d;
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }




        protected override void OnLoad()
        {
            base.OnLoad();

            scene = new List<Entity>();

            shader = new Shader("./shaders/polygon/shader.vert", "./shaders/polygon/shader.frag");





            scene.Add(new Ship());
            scene[0].pos.X = .25f;

            scene.Add(new Ship());
            scene[1].pos.X = -.25f;



            scene.Add(new Ship());
            scene[2].pos.Y = .35f;


            scene.Add(new Asteroid(10));






            GL.ClearColor(0.0f, 0.0f, 0.0f, 1f);
            GL.EnableVertexAttribArray(0);

        }



        List<Entity> scene;




        // This function runs on every update frame.
        protected override void OnUpdateFrame(FrameEventArgs e)
        {

            // Check if the Escape button is currently being pressed.
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                // If it is, close the window.
                Close();
            }

            base.OnUpdateFrame(e);
        }



        private Shader shader;

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            scene[0].orientation += .01f;
            scene[1].orientation -= .01f;


            foreach (Entity ent in scene){
                ent.draw(shader);
            }


            


            SwapBuffers();
        }

    }
    
}



