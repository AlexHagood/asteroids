using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

public class Ship : Entity
{



    public Ship() : base(new List<Vector3>
            {
            new Vector3(-0.0f, -0.03f, -0.0f),
            new Vector3(0.04f, -0.05f, -0.0f),
            new Vector3(-0.0f, 0.05f, -0.0f),
            new Vector3(-0.04f, -0.05f, -0.0f),
            })
    {}


    public void control(KeyboardState keyboardState, float dT){

            accel = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.R))
            {
                pos = Vector2.Zero;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                orientation += .01f;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                orientation -= .01f;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                accel.X += aSpeed*dT*(float)Math.Cos(MathHelper.DegreesToRadians(orientation + 90));
                accel.Y += aSpeed*dT*(float)Math.Sin(MathHelper.DegreesToRadians(orientation + 90));
            }
            else
            {
                accel.X += .5f *dT * -speed.X;
                accel.Y += .5f *dT* -speed.Y;
            }
            calcMove();
    }
}
