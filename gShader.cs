public static class globalShader
{
    public static Shader GShader { get; private set; }

    public static void Initialize(string vertexPath, string fragmentPath, string geoPath = "")
    {
        GShader = new Shader(vertexPath, fragmentPath, geoPath);
    }
}

