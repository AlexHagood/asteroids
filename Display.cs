using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;


public class Display {
    private int VertexBufferObject;
    private int VertexArrayObject;
    private int ElementBufferObject;

    public float[] vertices;

    public int[] indices;

    public Shader shader;

    public Display(float[] inVertices, int[] inIndices){
        vertices = inVertices;
        indices = inIndices;
        buildBuffer();
    }

    public Display(float[] inVertices){
        vertices = inVertices;
        indices = genIndices(inVertices.Length);
        buildBuffer();
    }

    public Display(List<Vector3> inVertices){
        vertices = util.vecs2Floats(inVertices);
        indices = genIndices(inVertices.Count);
        buildBuffer();
    }

    public void updateBuffer(float[] inVertices, int[] inIndices)
    {
        vertices = inVertices;
        indices = inIndices;
        buildBuffer();
    }




    public static int[] genIndices(int verticeCount)
    {
        int[] tempIndices = new int[verticeCount + 1];
        for (int i = 0; i < verticeCount; i++)
        {
            tempIndices[i] = i;

        }
        tempIndices[verticeCount] = 0;
        return tempIndices;


    }



    public void buildBuffer()
    {
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

    }

    public void draw(Matrix4 trans)
    {
        GL.BindVertexArray(VertexArrayObject);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);

        shader.Use();

        int transLoc = GL.GetUniformLocation(shader.getHandle(), "trans");
        
        GL.UniformMatrix4(transLoc, false, ref trans);
        GL.DrawElements(PrimitiveType.LineStrip, indices.Length, DrawElementsType.UnsignedInt, 0);

    }

    public void drawLine(Matrix4 trans)
    {
        GL.BindVertexArray(VertexArrayObject);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);

        shader.Use();

        int transLoc = GL.GetUniformLocation(shader.getHandle(), "trans");
        
        GL.UniformMatrix4(transLoc, false, ref trans);
        GL.DrawElements(PrimitiveType.Lines, indices.Length, DrawElementsType.UnsignedInt, 0);
    }
    public void drawPoints(Matrix4 trans)
    {
        GL.BindVertexArray(VertexArrayObject);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);

        shader.Use();

        int transLoc = GL.GetUniformLocation(shader.getHandle(), "trans");
        
        GL.UniformMatrix4(transLoc, false, ref trans);
        GL.DrawElements(PrimitiveType.Points, indices.Length, DrawElementsType.UnsignedInt, 0);
    }


    ~Display()
    {
        GL.DeleteBuffer(VertexBufferObject);
        GL.DeleteBuffer(VertexArrayObject);
        GL.DeleteBuffer(ElementBufferObject);
    }



}