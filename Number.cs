using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;

public class Number{
    private int value;

    Display display;
    public Shader shader;
    int[][] indices;

    public Number(int val, Shader shader)
    {
        value = val;

        indices = new int[10][];

        indices[0] = new int[] {0,1,1,5,5,4,4,0,0,1};
        indices[1] = new int[] {5,1};
        indices[2] = new int[] {0,1,1, 3,3, 2,2, 4,4, 5};
        indices[3] = new int[] {0,1,2,3,4,5,1,5};
        indices[4] = new int[] {5,1,1,3,3,2,2,0};
        indices[5] = new int[] {1,0,0,2,2,3,3,5,5,4};
        indices[6] = new int[] {1,5,5,4,4,2,2,3};
        indices[7] = new int[] {5,1,1,0};
        indices[8] = new int[] {0,4,1,5,0,1,2,3,4,5};
        indices[9] = new int[] {3,2,2,0,0,1,1,5};

        this.shader = shader;
        display = new Display([], []);
        display.shader = shader;

        generateDisplayData();

    }


    public void setVal(int value)
    {
        this.value = value;
        generateDisplayData();
    }


    float[] points = {-1f, 1f, 0f, 0f, 1f, 0f, -1f, 0f, 0f, 0f, 0f, 0f, -1f, -1f, 0f, 0f, -1f, 0f};

    public void draw()
    {

        Matrix4 trans = Matrix4.CreateTranslation(-.88f, .88f, 0f);

        display.drawLine(trans);

    }

    private void generateDisplayData()
    {
        string number = value.ToString();

        List<int> numIndices = new List<int>();
        List<float> numVertices = new List<float>();
        float offset = 0f;


        foreach (char c in number)
        {
            int[] currentNum = indices[c - '0'];
            foreach (int x in currentNum)
            {
                numVertices.Add((points[x * 3] + offset) / 10);
                numVertices.Add(points[x * 3 + 1] / 10);
                numVertices.Add(points[x * 3 + 2] / 10);
            }
            offset += 1.2f;
        }


        for (int k = 0; k < numVertices.Count / 3; k++)
        {
            numIndices.Add(k);
        }

        display.updateBuffer(numVertices.ToArray(), numIndices.ToArray());

    }


}