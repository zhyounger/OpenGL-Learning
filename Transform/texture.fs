#version 430 core
out vec4 FragColor;

in vec3 ourColor;
in vec2 TexCoord;

// texture sampler
uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
	// mix函数根据第三个参数进行线性插值，若第三个值为0.0，它返回第一个输入，若是1.0则返回第二个。0.2会返回80%第一个输入的颜色和20%第二个输入的颜色
	FragColor = mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.2);
	//FragColor = texture(texture1, TexCoord) * vec4(ourColor, 1.0);
}