﻿#version 430 core
       
in VertexData
{
    vec3 position_ws;
    vec3 normal_ws;
} inData;

layout (std140, binding = 0) uniform CameraData
{
	mat4 projection;
	mat4 view;
	vec3 eye_position;
} camera;

layout (std140, binding = 2) uniform MaterialData
{
	vec3 ambient;
	float alpha;
	vec3 diffuse;
	float shininess;
	vec3 specular;
} material;

layout (std140, binding = 3) uniform SunLight
{
	vec3 position;
	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
} sun;


void main() {
	vec3 N = normalize(inData.normal_ws);
	vec3 Eye = normalize(camera.eye_position - inData.position_ws);
    vec3 L = normalize(sun.position);
    vec3 H = normalize(L + Eye);

	float Idif = max(dot(N, L), 0.0);
	float Ispe = (Idif > 0.0) ? pow(max(dot(N, H), 0.0), material.shininess) : 0.0;
	
    vec3 amb = sun.ambient;
    vec3 dif = Idif * sun.diffuse;
    vec3 spe = Ispe * sun.specular;

	vec3 final_color = material.ambient * amb + material.diffuse * dif + material.specular * spe;

    gl_FragColor = vec4(final_color, material.alpha);
}