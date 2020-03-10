#version 430 core

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 normal;
layout (location = 2) in vec2 tex_coord;

out VertexData
{
    vec3 position_ws;
	vec3 normal_ws;
	vec2 tex_coord;
} outData;

layout (std140, binding = 0) uniform CameraData
{
	mat4 projection;
	mat4 view;
	vec3 eye_position;
} camera;

layout (std140, binding = 1) uniform Transform
{
	mat3 scale;
	mat3 rotation;
	vec3 position;
} transform;	

void main() {
    vec3 newPos = (transform.scale * transform.rotation * position) + transform.position;
    
    outData.position_ws = newPos;
	outData.normal_ws = transform.rotation * normal;
	outData.tex_coord = tex_coord;    
    
	gl_Position = camera.projection * camera.view * vec4(newPos, 1.0);
}