
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

public class util
{
    public static double Cross2D(Vector2d p1, Vector2d p2)
    {
        return p1.X * p2.Y - p1.Y * p2.X;

    }

    public static bool calcLineIntersectPoint(Vector2 a, Vector2 b, Vector2 c, Vector2 d) // ab, cd
    {
        Vector2d ac = c - a;
        Vector2d ab = b - a;
        Vector2d cd = d - c;



        
        double t1 = -Cross2D(ac, ab) / Cross2D(ab, cd);
        //Console.WriteLine(t1);

        if (0 < t1 && t1 < .5)
        {
            //intersection = (Vector2)(t1 * ab + a);
            return true;

        } else 
        {
            //intersection = Vector2.Zero;
            return false;
        }

    }


    public static float[] vecs2Floats(List<Vector3> vertices)
    {
        float[] converted = new float[vertices.Count * 3];
        int i = 0;
        foreach (Vector3 vec in vertices)
        {
            converted[i] = vec.X;
            converted[i + 1] = vec.Y;
            converted[i + 2] = vec.Z;
            i += 3;
        }
        return converted;
    }

    public static List<Vector3> floats2Vecs(float[] vertices)
    {
        List<Vector3> converted = new List<Vector3>();
        int i = 0;
        int len = vertices.Length;
        for (i = 0; i < len; i += 3)
        {
            converted.Add(new Vector3(vertices[i], vertices[i+1], vertices[i+2]));
        }

        if (i > len)
        {
            Console.WriteLine("float array to vector size mismatch");
        }
        return converted;


    }



}