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

        int xres = 1600;
        int yres = 1200;
        Display dotDebug;

        Number fps;

        protected override void OnLoad()
        {
            base.OnLoad();

            scene = new List<Entity>();
            asteroids = new List<Asteroid>();

            globalShader.Initialize("./shaders/polygon/shader.vert", "./shaders/polygon/shader.frag");

            shader = globalShader.GShader;



            dotDebug = new Display([0.0f, 0.0f, 0.0f]);
            dotDebug.shader = globalShader.GShader;



            fps = new Number(1234567890, shader);



            





            scene.Add(new Ship());
            scene[0].pos.X = .25f;
            scene.Add(new Ship());
            scene[1].pos.X = -.25f;
            scene.Add(new Ship());
            scene[2].pos.Y = .35f;


            asteroids.Add(new Asteroid(10));
            asteroids.Add(new Asteroid(10));
            asteroids.Add(new Asteroid(10));


            scene.Add(asteroids[0]);
            scene.Add(asteroids[1]);
            scene.Add(asteroids[2]);


            scene[3].speed = new Vector2(.01f, .03f);
            scene[4].speed = new Vector2(-.01f, .1f);
            scene[5].speed = new Vector2(.1f, -.01f);


            tests = new List<DebugLine>();
            tests.Add(new DebugLine(0.0f, 0.0f, 0.0f, 0.0f));
            tests.Add(new DebugLine(0.0f, 0.0f, 0.0f, 0.0f));
            tests.Add(new DebugLine(0.0f, 0.0f, 0.0f, 0.0f));
            tests.Add(new DebugLine(-1.0f, 1.0f, 1.0f, -1.0f));






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
        List<Asteroid> asteroids;
        List<DebugLine> tests;




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


            for (int i = 0; i < 3; i++)
            {
                tests[i].p1 = player.pos;
                tests[i].p2 = asteroids[i].pos;
            }


            player.calcPixelVertices();

            List<Vector3> dots = new List<Vector3>();
            foreach (Asteroid ast in asteroids)
            {
                if (player.checkCollision(ast)){
                    for (int i = 2; i < ast.display.vertices.Length; i += 3)
                    {
                        ast.display.vertices[i] = 1f;
                        ast.display.buildBuffer();
                    }
                }
                else{
                    for (int i = 2; i < ast.display.vertices.Length; i += 3)
                    {
                        ast.display.vertices[i] = 0f;
                        ast.display.buildBuffer();
                    }

                }
            }

            //dotDebug.updateBuffer(util.vecs2Floats(dots), Display.genIndices(dots.Count));
            //dotDebug.drawLine(Matrix4.Identity);






            foreach(DebugLine line in tests)
            {
                line.draw();
            }

            

            foreach (Entity ent in scene)
            {
                ent.calcMove(dT);
                
                if (Math.Abs(ent.pos.X) > 1f) ent.pos.X = Math.Sign(ent.pos.X) * -1;
                if (Math.Abs(ent.pos.Y) > 1f) ent.pos.Y = Math.Sign(ent.pos.Y) * -1;

                ent.draw();
            }





            


            SwapBuffers();

            
        }

    }
    
}



