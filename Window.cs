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
        int VertexBufferObject;

        int VertexArrayObject;

        int ElementBufferObject;

        int rotationLoc;

        Matrix4 rotation;


        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 0.2f);

            int vao = genVAO(shipVerices);

            rotation = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(0.01f));



            




            shader = new Shader("shaders/shader.vert", "shaders/shader.frag");


            shader.Use();

            GL.EnableVertexAttribArray(0);

            //Code goes here
        }

        private readonly float[] shipVerices =
        {
            0.0f, -0.3f, 0.0f,
            0.4f, -0.5f, 0.0f,
            0.0f, 0.5f, 0.0f,
            -0.4f,  -0.5f, 0.0f

        };


        private readonly int[] shipIndices = 
        {
            0, 1, 2,
            2, 3, 0
        };


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

        private int genVAO(float[] _vertices)
        {


            VertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            VertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(VertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            GL.EnableVertexAttribArray(0);

            ElementBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);

            GL.BufferData(BufferTarget.ElementArrayBuffer, shipIndices.Length * sizeof(uint), shipIndices, BufferUsageHint.StaticDraw);

            return 1;

        }


        private Shader shader;

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();


            rotationLoc = GL.GetUniformLocation(shader.getHandle(), "transform");
            GL.UniformMatrix4(rotationLoc, false, ref rotation);

            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, shipIndices.Length, DrawElementsType.UnsignedInt, 0);

            rotation = rotation * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(0.01f));



            //Code goes here.


            SwapBuffers();
        }

    }
    public class Shader
    {
        private int Handle;

        public int getHandle(){
            return Handle;
        }

        public Shader(string vertexPath, string fragmentPath)
        {
            string VertexShaderSource = File.ReadAllText(vertexPath);
            string FragmentShaderSource = File.ReadAllText(fragmentPath);
            string GeometryShaderSource = File.ReadAllText("shaders/shader.geo");

            // var GeometryShader = GL.CreateShader(ShaderType.GeometryShader);
            // GL.ShaderSource(GeometryShader, GeometryShaderSource);


            var VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            var FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);



            // GL.CompileShader(GeometryShader);

            // GL.GetShader(GeometryShader, ShaderParameter.CompileStatus, out int success);
            // if (success == 0)
            // {
            //     string infoLog = GL.GetShaderInfoLog(GeometryShader);
            //     Console.WriteLine(infoLog);
            // }

            GL.CompileShader(VertexShader);

            GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(VertexShader);
                Console.WriteLine(infoLog);
            }

            GL.CompileShader(FragmentShader);

            GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(FragmentShader);
                Console.WriteLine(infoLog);
            }

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);
            // GL.AttachShader(Handle, GeometryShader);


            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(Handle);
                Console.WriteLine(infoLog);
            }

            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            // GL.DetachShader(Handle, GeometryShader);

            GL.DeleteShader(FragmentShader);
            // GL.DeleteShader(GeometryShader);

            GL.DeleteShader(VertexShader);

        }



        public void Use()
        {
            GL.UseProgram(Handle);
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            if (disposedValue == false)
            {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

