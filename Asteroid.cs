using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Runtime.CompilerServices;

public class Asteroid : Entity
{
    


    public Asteroid(int chunks) : base(genVertices(chunks), genIndices(chunks))
    {


        scale = .3f;
        orientation = 0;


    }

    private static float[] genVertices(int chunks)
    {
        float[] tempIndices = new float[(chunks + 1) * 3];


        for (int i = 0; i < chunks; i ++)
        {
            float rad = genMod(.5f) * .3f;


            double angle = 2 * Math.PI * i / chunks;
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
    

    private static int[] genIndices(int chunks)
    {
        Console.WriteLine("chunks");

        Console.WriteLine(chunks);
        int[] tempIndices = new int[chunks + 1];

        // for (int i = 0; i < chunks * 3; i += 3)
        // {
        //     tempIndices[i] = 0;
        //     tempIndices[i+1] = currentVert;
        //     currentVert += 1;
        //     if (currentVert == chunks + 1) currentVert = 1;
        //     tempIndices[i+2] = currentVert;

        // }

        for (int i = 0; i < chunks; i++)
        {
            tempIndices[i] = i;

        }

        tempIndices[chunks] = 0;


        return tempIndices;


    }

}

