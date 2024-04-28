using OpenTK.Graphics.OpenGL;

public class Shader
    {
        private int Handle;

        public int getHandle(){
            return Handle;
        }

        public Shader(string vertexPath, string fragmentPath, string geoPath = "")
        {
            bool enableGeoShader = (geoPath != "");

            if (enableGeoShader) 
            {
                Console.WriteLine("Geo Shader enabled");

            } else
            {
                Console.WriteLine("Geo Shader disabled");
            }


            Handle = GL.CreateProgram();

            string VertexShaderSource = File.ReadAllText(vertexPath);
            int VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);
            GL.CompileShader(VertexShader);
            GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(VertexShader);
                Console.WriteLine(infoLog);
            }
            GL.AttachShader(Handle, VertexShader);


            string FragmentShaderSource = File.ReadAllText(fragmentPath);
            int FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);
            GL.CompileShader(FragmentShader);
            GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(FragmentShader);
                Console.WriteLine(infoLog);
            }
            GL.AttachShader(Handle, FragmentShader);


            int GeometryShader = 0;
            if (enableGeoShader)
            {
                string GeometryShaderSource = File.ReadAllText(geoPath);
                GeometryShader = GL.CreateShader(ShaderType.GeometryShader);
                GL.ShaderSource(GeometryShader, GeometryShaderSource);
                GL.CompileShader(GeometryShader);
                GL.GetShader(GeometryShader, ShaderParameter.CompileStatus, out success);
                if (success == 0)
                {
                    string infoLog = GL.GetShaderInfoLog(GeometryShader);
                    Console.WriteLine(infoLog);
                }
                GL.AttachShader(Handle, GeometryShader);

            }






            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(Handle);
                Console.WriteLine(infoLog);
            }

            GL.DetachShader(Handle, VertexShader);
            GL.DeleteShader(VertexShader);


            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);

            if (enableGeoShader){
            GL.DetachShader(Handle, GeometryShader);
            GL.DeleteShader(GeometryShader);
            }
            


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