using asteroids;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

public abstract class Entity()
{


    //Graphics of our entities
    public Display display;

    public float scale;

    public Entity(float[] inVertices, int[] inIndices) : this()
    {
        scale = 1f;
        display = new Display(inVertices, inIndices);
    }




    //Normalized?? idk yet
    public Vector2 pos; 
    public Vector2 speed;
    public Vector2 accel; 


    public float aSpeed = .00005f;



    // in radians
    public float orientation;

        // References to our buffers
    private int VertexBufferObject;
    private int VertexArrayObject;
    private int ElementBufferObject;

    public void calcMove()
    {
            speed.X += accel.X;
            speed.Y += accel.Y;

            pos.X += speed.X;
            pos.Y += speed.Y;
    }


    public void draw()
    {
        Matrix4 orientMatrix = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(orientation));
        Matrix4 translateMatrix = Matrix4.CreateTranslation(pos.X, pos.Y, 0f);
        Matrix4 scaleMatrix = Matrix4.CreateScale(scale);
        Matrix4 trans = scaleMatrix * orientMatrix * translateMatrix;
        display.draw(trans);
    }
    
}