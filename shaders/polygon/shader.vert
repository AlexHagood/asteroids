#version 330 core

layout (location = 0) in vec3 aPos;

uniform mat4 trans;
out float color;
// can we pack a color into the Z position of the vertice? lets try?

void main()
{

    gl_Position = trans * vec4(aPos.xy, 0.0, 1.0);
    color = aPos.z;
}
