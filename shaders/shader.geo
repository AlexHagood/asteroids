#version 330 core

layout(triangles) in;
layout(line_strip, max_vertices = 6) out;

void main()
{
    for (int i = 0; i < 3; i++) {
        gl_Position = gl_in[i].gl_Position;
        EmitVertex();
    }
    
    for (int i = 0; i < 3; i++) {
        gl_Position = gl_in[i].gl_Position;
        EmitVertex();
        gl_Position = gl_in[(i + 1) % 3].gl_Position;

        // Check if the edge is not shared by adjacent triangles
        if (gl_in[i].gl_Position != gl_in[(i + 2) % 3].gl_Position &&
            gl_in[(i + 1) % 3].gl_Position != gl_in[(i + 2) % 3].gl_Position) {
            EmitVertex();
        }
    }
    
    EndPrimitive();
}
