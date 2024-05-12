
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System;

public class DebugLine{

    public Vector2 p1;
    public Vector2 p2;

    Display display;

    public DebugLine(Vector2 p1, Vector2 p2, Shader shader)
    {
        this.p1 = p1;
        this.p2 = p2;
        display = new Display([0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f], [0,1]);
        display.shader = globalShader.GShader;
        
    }

    public DebugLine(float x1, float y1, float x2, float y2)
    {
        this.p1 = new Vector2(x1,y1);
        this.p2 = new Vector2(x2,y2);
        display = new Display([0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f], [0,1]);
        display.shader = globalShader.GShader;
    }

    Matrix4 genTrans()
    {
        Vector2 slopeVec = p1 - p2;

        Matrix4 orient = Matrix4.CreateRotationZ((float)Math.Atan2(slopeVec.Y, slopeVec.X) + (float)Math.PI);

        Matrix4 scale = Matrix4.CreateScale(Vector2.Distance(p1,p2));
        Matrix4 translate = Matrix4.CreateTranslation(p1.X, p1.Y, 0.0f);

        Matrix4 trans = scale * orient * translate;

        return trans;
        

    }

// return to this later for optimization maybe.



    public void draw()
    {
        display.draw(genTrans());

    }
}