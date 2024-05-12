using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

public class Ship : Entity
{



    public Ship() : base(new List<Vector3>
            {
            new Vector3(-0.0f, -0.03f, -0.0f),
            new Vector3(0.04f, -0.05f, -0.0f),
            new Vector3(-0.0f, 0.05f, -0.00f),
            new Vector3(-0.04f, -0.05f, -0.0f),
            })
    {
        bullets = new Bullet[10];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = new Bullet(this);
        }
    }
    public Bullet[] bullets = new Bullet[10];


    public void drawBullets(float dT)
    {
        //Console.WriteLine(bullets.Count);
        foreach (Bullet bullet in bullets)
        {  
            if (bullet.active){
            //Console.Write("1");
            bullet.calcMove(dT);
            bullet.draw();
            }
            else {
            //Console.Write("0");
            }
        }
        //Console.Write("\n");

    }

    

    public void cullBullets()
    {
        foreach (Bullet bullet in bullets)
        {
            if (bullet.active && bullet.ShouldCull()){

                bullet.active = false;
            }
        }
    }

    private float Cooldown = 0.0f;


    private void fire(){
        //fire bullet
        foreach (Bullet bullet in bullets)
        {
            if (!bullet.active)
            {

                bullet.active = true;
                bullet.pos = pos;
                bullet.orientation = orientation;
                bullet.speed.X =  (float)Math.Cos(MathHelper.DegreesToRadians(orientation + 90));
                bullet.speed.Y =  (float)Math.Sin(MathHelper.DegreesToRadians(orientation + 90));
                bullet.display.shader = display.shader;
                break;
            }
        }
    }
    



    public void control(KeyboardState keyboardState, float dT){

            accel = Vector2.Zero;


            if (keyboardState.IsKeyDown(Keys.R))
            {
                pos = Vector2.Zero;
                speed = Vector2.Zero;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                orientation += dT * 360;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                orientation -= dT * 360;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                accel.X += aSpeed*dT*(float)Math.Cos(MathHelper.DegreesToRadians(orientation + 90));
                accel.Y += aSpeed*dT*(float)Math.Sin(MathHelper.DegreesToRadians(orientation + 90));
            }
            // fire bullet
            Cooldown += dT;
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (Cooldown > .5f)
                {
                    fire();
                    Cooldown = 0.0f;
                }
            }
            else
            {
                accel.X += .5f * dT * -speed.X;
                accel.Y += .5f * dT* -speed.Y;
            }
            calcMove(dT);
    }
}
