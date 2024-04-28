#version 330 core
layout (points) in;
layout (line_strip, max_vertices = 256) out;

void main()
{
    int numVertices = gl_in.length();

    gl_Position = gl_in[0].gl_Position;
    EmitVertex();

    for (int i = 1; i < numVertices; i++)
    {
        gl_Position = gl_in[i].gl_Position;
        EmitVertex();
    }

    EndPrimitive();
}