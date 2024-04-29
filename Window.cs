using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Diagnostics;





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


        private Stopwatch timer = new Stopwatch();

        DebugLine test;
        DebugLine test2;
        DebugLine test3;


        Number fps;

        protected override void OnLoad()
        {
            base.OnLoad();

            scene = new List<Entity>();

            globalShader.Initialize("./shaders/polygon/shader.vert", "./shaders/polygon/shader.frag");

            shader = globalShader.GShader;




            fps = new Number(1234567, shader);

            test = new DebugLine(0.0f, 0.0f, 0.0f, 0.0f);
            test2 = new DebugLine(0.0f, 0.0f, 0.0f, 0.0f);
            test3 = new DebugLine(0.0f, 0.0f, 0.0f, 0.0f);


            





            scene.Add(new Ship());
            scene[0].pos.X = .25f;

            scene.Add(new Ship());
            scene[1].pos.X = -.25f;



            scene.Add(new Ship());
            scene[2].pos.Y = .35f;


            scene.Add(new Asteroid(10));
            scene.Add(new Asteroid(10));
            scene.Add(new Asteroid(10));

            scene[3].speed = new Vector2(.00001f, .00003f);
            scene[4].speed = new Vector2(-.0001f, .0001f);
            scene[5].speed = new Vector2(.0003f, -.0001f);



            foreach (Entity ent in scene)
            {
                ent.display.shader = shader;
            }


            player = (Ship)scene[2];




            GL.ClearColor(0.0f, 0.0f, 0.0f, 1f);
            GL.EnableVertexAttribArray(0);

            timer.Start();


        }



        List<Entity> scene;
        Ship player;

        double dT;




        // This function runs on every update frame.
        protected override void OnUpdateFrame(FrameEventArgs e)
        {


            // Check if the Escape button is currently being pressed.
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                // If it is, close the window.
                Close();
            }

            player.control(this.KeyboardState, (float)dT);





            base.OnUpdateFrame(e);
        }



        private Shader shader;
        int i = 0;
        protected override void OnRenderFrame(FrameEventArgs e)
        {

            dT = timer.Elapsed.TotalSeconds;
            timer.Restart();
            dT = Math.Min(dT, 0.1);
            
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            i++;
            if (i > 1000)
            {
                i = 0;
                fps.setVal((int) (1.0f / dT));
            }

            fps.draw();

            scene[0].orientation += .01f;
            scene[1].orientation -= .01f;

            test.p1 = player.pos;
            test2.p1 = player.pos;
            test3.p1 = player.pos;
            test.p2 = scene[3].pos;
            test2.p2 = scene[4].pos;
            test3.p2 = scene[5].pos;
            test.draw();
            test2.draw();
            test3.draw();
            

            foreach (Entity ent in scene)
            {
                ent.calcMove();
                
                if (Math.Abs(ent.pos.X) > 1f) ent.pos.X = Math.Sign(ent.pos.X) * -1;
                if (Math.Abs(ent.pos.Y) > 1f) ent.pos.Y = Math.Sign(ent.pos.Y) * -1;

                ent.draw();
            }





            


            SwapBuffers();

            
        }

    }
    
}



