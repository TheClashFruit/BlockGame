#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;

out vec2 texCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform vec3 chunkPosition;

void main() {
    vec4 worldPosition = vec4(aPosition + chunkPosition, 1.0);

    gl_Position = worldPosition * model * view * projection;

    texCoord = aTexCoord;
}