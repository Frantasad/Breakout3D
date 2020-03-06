#version 430 core
       
in VertexData
{
    vec3 position_ws;
    vec3 normal_ws;
    vec2 tex_coord;
} inData;

layout (std140, binding = 2) uniform MaterialData
{
	vec3 ambient;
	float alpha;
	vec3 diffuse;
	float shininess;
	vec3 specular;
} material;

layout (std140, binding = 0) uniform CameraData
{
	mat4 projection;
	mat4 view;
	vec3 eye_position;
} camera;

void main() {
    gl_FragColor = vec4(material.ambient, 1.0);
}