using System.Runtime.CompilerServices;
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
        List<Vector2i> pixelVertices = new List<Vector2i>();

    }

    public Entity(List<Vector3> vertices) : this()
    {
        scale = 1f;
        display = new Display(vertices);
        List<Vector2> pixelVertices = new List<Vector2>();

    }

    

    public List<Vector2> pixelVertices;

    //Normalized?? idk yet
    public Vector2 pos; 
    public Vector2 speed;
    public Vector2 accel; 


    public float aSpeed = 120f;



    // in radians
    public float orientation;

    public void calcMove(double dT)
    {
            speed.X += accel.X * (float)dT;
            speed.Y += accel.Y * (float)dT;

            pos.X += speed.X * (float)dT;
            pos.Y += speed.Y * (float)dT;
    }

    public bool checkCollision(Entity target)
    {
        if (Vector2.Distance(pos, target.pos) > .5)
        {
            return false;
        }

        calcPixelVertices();
        target.calcPixelVertices();
        int myEdges = this.pixelVertices.Count;
        int theirEdges = target.pixelVertices.Count;

        for (int i = 0; i < myEdges; i++)
        {
            for (int j = 0; j < theirEdges; j++)
            {
                if (GFG.doIntersect(this.pixelVertices[i], this.pixelVertices[(i + 1) % myEdges], target.pixelVertices[j], target.pixelVertices[(j+1)%theirEdges])) return true;

            }

        }
        return false;

    }


    public void draw()
    {
        Matrix4 orientMatrix = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(orientation));
        Matrix4 translateMatrix = Matrix4.CreateTranslation(pos.X, pos.Y, 0f);
        Matrix4 scaleMatrix = Matrix4.CreateScale(scale);
        Matrix4 trans = scaleMatrix * orientMatrix * translateMatrix;
        display.draw(trans);
    }

    public void calcPixelVertices()
    {
        pixelVertices = new List<Vector2>();
        Vector2 resolution = new Vector2(1600, 1200);

        foreach (Vector3 abs in util.floats2Vecs(display.vertices))
        {
            pixelVertices.Add(((abs.Xy * scale + pos) + Vector2.One) / 2 * resolution);
        }

    }

    
    
}