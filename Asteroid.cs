using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Runtime.CompilerServices;

public class Asteroid : Entity
{
    


    public Asteroid(int chunks) : base(genVertices(chunks), genIndices(chunks))
    {


        scale = .2f;
        orientation = 0;




    }

    private static float[] genVertices(int chunks)
    {
        float[] tempIndices = new float[(chunks + 1) * 3];
        //Establish center point
        tempIndices[0] = 0.0f;
        tempIndices[1] = 0.0f;
        tempIndices[2] = 0.0f;


        for (int i = 0; i < chunks; i ++)
        {
            float rad = genMod(.5f) * .3f;


            double angle = 2 * Math.PI * i / chunks;
            angle *= genMod(.1f);
            int index = (i + 1) * 3;

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
        int[] tempIndices = new int[chunks * 3];
        int currentVert = 1;

        for (int i = 0; i < chunks * 3; i += 3)
        {
            tempIndices[i] = 0;
            tempIndices[i+1] = currentVert;
            currentVert += 1;
            if (currentVert == chunks + 1) currentVert = 1;
            tempIndices[i+2] = currentVert;

        }




        return tempIndices;


    }

}

