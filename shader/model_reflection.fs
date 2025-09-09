#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec3 Position;
in vec2 TexCoords;

uniform vec3 cameraPos;
uniform samplerCube skybox;
uniform sampler2D texture_diffuse1;
uniform sampler2D texture_specular1;
uniform sampler2D texture_reflection1;

void main()
{
    // 1. 获取基础材质属性
    vec3 diffuseColor = texture(texture_diffuse1, TexCoords).rgb;
    float specularIntensity = texture(texture_specular1, TexCoords).r;
    float reflectionIntensity = texture(texture_reflection1, TexCoords).r;
    
    // 2. 计算视线方向和表面法线
    vec3 viewDir = normalize(cameraPos - Position);
    vec3 norm = normalize(Normal);
    
    // 3. 计算反射向量（用于环境映射）
    vec3 reflectDir = reflect(-viewDir, norm);
    
    // 4. 从天空盒获取反射颜色
    vec3 reflectionColor = texture(skybox, reflectDir).rgb;
    
    // 5. 组合所有成分
    // 基础漫反射颜色
    vec3 result = diffuseColor;
    
    // 添加镜面反射高光
    // 这里使用简化的Blinn-Phong模型
    vec3 halfwayDir = normalize(viewDir + reflectDir);
    float spec = pow(max(dot(norm, halfwayDir), 0.0), 64.0);
    vec3 specular = specularIntensity * spec * reflectionColor;
    result += specular;
    
    // 添加环境反射
    vec3 environmentReflection = reflectionIntensity * reflectionColor;
    result = mix(result, environmentReflection, reflectionIntensity * 0.2);
    
    // 6. 输出最终颜色
    FragColor = vec4(result, 1.0);
}