using asteroids;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

public abstract class Entity()
{


    //Graphics of our entities
    private float[] vertices;

    private int[] indices;

    public float scale;

    public Entity(float[] inVertices, int[] inIndices) : this()
    {
        scale = .1f;
        vertices = inVertices;
        indices = inIndices;
        buildBuffer();
    }




    //Normalized?? idk yet
    public Vector2 pos; 

    // in radians
    public float orientation;

        // References to our buffers
    private int VertexBufferObject;
    private int VertexArrayObject;
    private int ElementBufferObject;


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

    public void draw(Shader shader)
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
        GL.BindVertexArray(VertexArrayObject);


        shader.Use();

        Matrix4 orientMatrix = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(orientation));
        Matrix4 translateMatrix = Matrix4.CreateTranslation(pos.X, pos.Y, 0f);
        Matrix4 scaleMatrix = Matrix4.CreateScale(scale);

        Matrix4 trans = scaleMatrix * orientMatrix * translateMatrix;

        int transLoc = GL.GetUniformLocation(shader.getHandle(), "trans");
        
        GL.UniformMatrix4(transLoc, false, ref trans);

        GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);






    }

    ~Entity()
    {
        GL.DeleteBuffer(VertexBufferObject);
        GL.DeleteBuffer(VertexArrayObject);
        GL.DeleteBuffer(ElementBufferObject);
    }


    
}