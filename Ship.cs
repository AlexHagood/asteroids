public class Ship : Entity
{
    public Ship() : base(new float[]
        {
            0.0f, -0.3f, 0.0f,
            0.4f, -0.5f, 0.0f,
            0.0f, 0.5f, 0.0f,
            -0.4f,  -0.5f, 0.0f
        },
        new int[]
        {
            0, 1, 2,
            2, 3, 0
        })
    {
        scale = .1f;
    }
}
