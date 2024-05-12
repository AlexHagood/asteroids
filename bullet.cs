using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
public class Bullet : Entity
{
    public bool active = false;
    public Bullet(Ship parent) : base(new List<Vector3>
            {
            new Vector3(0.0f, 0.00f, 0.0f),
            new Vector3(0.0f, 0.08f, 0.0f)
            })
    {
        pos = parent.pos;
        orientation = parent.orientation;
        speed.X +=  (float)Math.Cos(MathHelper.DegreesToRadians(orientation + 90));
        speed.Y +=  (float)Math.Sin(MathHelper.DegreesToRadians(orientation + 90));
        //display.shader = parent.display.shader;
    } 

    public bool ShouldCull()
    {
        if (pos.X > 1 || pos.X < -1 || pos.Y > 1 || pos.Y < -1)
        {
            return true;
        }
        return false;
    }
}