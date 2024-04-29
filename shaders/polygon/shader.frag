#version 330 core

out vec4 FragColor;
in vec2 pos;

void main()
{
    FragColor = vec4(pos.x, pos.y, pos.x * pos.y, 1.0); // Output white color
    //FragColor = vec4(1.0);
}
