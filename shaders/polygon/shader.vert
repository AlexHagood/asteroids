#version 330 core

layout (location = 0) in vec3 aPos;

uniform mat4 trans;
out vec2 pos;

void main()
{
    gl_Position = trans * vec4(aPos, 1.0);
    pos = gl_Position.xy;
}
