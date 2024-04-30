
using OpenTK.Mathematics;

public class utilMath
{
    public static float Cross2D(Vector2 p1, Vector2 p2)
    {
        return p1.X * p2.Y - p1.Y * p2.X;

    }

    public static bool calcLineIntersectPoint(Vector2 a, Vector2 b, Vector2 c, Vector2 d, out Vector2 intersection) // ab, cd
    {
        Vector2 ac = c - a;
        Vector2 ab = b - a;
        Vector2 cd = d - c;



        
        float t1 = Cross2D(ac, cd) / Cross2D(ab, cd);

        if (0 < t1 && t1 < 1)
        {
            intersection = t1 * ab + a;
            return true;

        } else 
        {
            intersection = Vector2.Zero;
            return false;
        }

    }



}