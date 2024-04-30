#version 330 core

out vec4 FragColor;
in float color;

void main()
{
    //FragColor = vec4(pos.x, pos.y, pos.x * pos.y, 1.0); // Output white color
    if (color == 0)
    {
        FragColor = vec4(1.0);
    }
    else if (color < 0)
    {
    FragColor = vec4(0.0, 1.0, 0.0, 1.0);
    }
    else
    {
    FragColor = vec4(1.0, 0.0, 0.0, 1.0);
    }

}
