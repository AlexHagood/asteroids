using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Runtime.CompilerServices;

public class Asteroid : Entity
{
    


    public Asteroid(int verticeCount) : base(genVertices(verticeCount), Display.genIndices(verticeCount))
    {


        scale = .3f;
        orientation = 0;
    }





    private static float[] genVertices(int verticeCount)
    {
        float[] tempIndices = new float[(verticeCount + 1) * 3];


        for (int i = 0; i < verticeCount; i ++)
        {
            float rad = genMod(.5f) * .3f;


            double angle = 2 * Math.PI * i / verticeCount;
            angle *= genMod(0.05f);
            int index = (i) * 3; // (i + 1) with center

            tempIndices[index] = rad * (float)Math.Cos(angle);
            tempIndices[index + 1] = rad * (float)Math.Sin(angle);
            tempIndices[index + 2] = 0.0f;
        }

        return tempIndices;

    }

            // Generates a random float 1 +- range
    public static float genMod(float range)
    {

        Random random = new Random();
        double modifier = random.NextDouble() * range * 2 - range;
        return (float)modifier + 1;
    } 
    



}

