# 纹理

在这份代码中我使用了下面这张纹理图片：


![texture](https://github.com/zhyounger/OpenGL-Learning/blob/master/Textures/src/wall.jpg)

## 纹理坐标：

![texture_coords](https://learnopengl-cn.github.io/img/01/06/tex_coords.png)

从上图中可以看出，纹理的坐标远点是从图片的左下方开始。绘制矩形/三角形时对应的纹理坐标如下：

```
//float vertices[] = {
//	// positions		// colors			// texture coords
//	 0.5f,  0.5f, 0.0f,  1.0f, 0.0f, 0.0f,  1.0f, 1.0f ,// top right
//	 0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,  1.0f, 0.0f,// bottom right
//	-0.5f, -0.5f, 0.0f,  0.0f, 0.0f, 1.0f,  0.0f, 0.0f,// bottom left
//	-0.5f,  0.5f, 0.0f,   1.0f, 1.0f, 0.0f,  0.0f, 1.0f// top left 
//};
//unsigned int indices[] = {  // note that we start from 0!
//	0, 1, 3,  // first Triangle
//	1, 2, 3   // second Triangle
//};
float vertices[] = {	// 渲染三角形形所需代码
	-0.5f, -0.5f, 0.0f,  0.0f, 0.0f, 1.0f,  0.0f, 0.0f,		// 渲染三角形所需代码
	 0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,  1.0f, 0.0f,		// 渲染三角形所需代码
	 0.0f,  0.5f, 0.0f,  1.0f, 0.0f, 0.0f,  0.5f, 1.0f		// 渲染三角形所需代码
};		// 渲染三角形所需代码
```
矩形的绘制是通过绘制两个三角形得到，我们只需给出4个顶点属性和一个索引数组即可绘制出矩形

## 接下来绑定VAO,VBO,EBO：

```
unsigned int VBO, VAO, EBO;
glGenVertexArrays(1, &VAO);
glGenBuffers(1, &VBO);
//glGenBuffers(1, &EBO);	// 渲染矩形所需代码
// bind the Vertex Array Object first, then bind and set vertex buffer(s), and then configure vertex attributes(s).
glBindVertexArray(VAO);

glBindBuffer(GL_ARRAY_BUFFER, VBO);
glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

//glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);	// 渲染矩形所需代码
//glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);	// 渲染矩形所需代码
```

## 解析顶点数据：

```
// 这是绘制的顶点坐标属性
glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
glEnableVertexAttribArray(0);

// 这是绘制的顶点颜色属性
glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(3 * sizeof(float)));
glEnableVertexAttribArray(1);

// 这是顶点对应的纹理坐标
glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(6 * sizeof(float)));
glEnableVertexAttribArray(2);
```

## 纹理设置

首先创建一个纹理ID并绑定

```
unsigned int texture;
glGenTextures(1, &texture);
glBindTexture(GL_TEXTURE_2D, texture);
```

设置环绕、过滤方式

```
// set the texture wrapping parameters
glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
// set texture filtering parameters
glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
```

生成并加载纹理

```
int width, height, nrChannels;
char *texturePath = "../src/wall.jpg";		// 这里填写你的图片路径
unsigned char *data = stbi_load(texturePath, &width, &height, &nrChannels, 0);		// stbi_load函数首先接受一个图像文件的位置作为输入。接下来它需要三个int作为它的第二、第三和第四个参数，stb_image.h将会用图像的宽度、高度和颜色通道的个数填充这三个变量。我们之后生成纹理的时候会用到的图像的宽度和高度的。
```

在这里使用到了[stb_image.h](https://github.com/zhyounger/OpenGL-Learning/blob/master/Textures/include/stb_image.h)

在使用`stb_image.h`之前，需要创建一个`stb_image.cpp`文件，并添加如下内容：
通过定义`STB_IMAGE_IMPLEMENTATION`，预处理器会修改头文件，让其只包含相关的函数定义源码。使用时只需要在程序中包含`stb_image.h`文件就可以了。
```
#define STB_IMAGE_IMPLEMENTATION
#include "stb_image.h"
```

接下来使用前面载入的图片生成纹理：

```
if (data)
{
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, data);
	glGenerateMipmap(GL_TEXTURE_2D);
}
else
{
	std::cout << "Failed to load texture" << std::endl;
}
// 释放图像内存
stbi_image_free(data);
```

当调用glTexImage2D时，当前绑定的纹理对象就会被附加上纹理图像。然而，目前只有基本级别(Base-level)的纹理图像被加载了，如果要使用多级渐远纹理，我们必须手动设置所有不同的图像（不断递增第二个参数）。或者，直接在生成纹理之后调用glGenerateMipmap。这会为当前绑定的纹理自动生成所有需要的多级渐远纹理。

## 最后应用纹理

```
while (!glfwWindowShouldClose(window))
	{
		// input
		// -----
		processInput(window);

		// render
		// ------
		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT);

		ourShader.use();
		glBindVertexArray(VAO); // seeing as we only have a single VAO there's no need to bind it every time, but we'll do so to keep things a bit more organized
								//glDrawArrays(GL_TRIANGLES, 0, 6);
		//glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);
		// glBindVertexArray(0); // no need to unbind it every time 
		
		glDrawArrays(GL_TRIANGLES, 0, 3);	// 渲染三角形所需代码

		//glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);	// 渲染矩形所需代码

		// glfw: swap buffers and poll IO events (keys pressed/released, mouse moved etc.)
		// -------------------------------------------------------------------------------
		glfwSwapBuffers(window);
		glfwPollEvents();
	}

	// optional: de-allocate all resources once they've outlived their purpose:
	// ------------------------------------------------------------------------
	glDeleteVertexArrays(1, &VAO);
	glDeleteBuffers(1, &VBO);
	glDeleteBuffers(1, &EBO);

	// glfw: terminate, clearing all previously allocated GLFW resources.
	// ------------------------------------------------------------------
	glfwTerminate();
	return 0;
}
```

你可以在[这里](https://github.com/zhyounger/OpenGL-Learning/tree/master/Textures)找到源码，最后渲染出的三角形与矩形（只需要将注释中三角形与矩形的代码互换即可）效果如下：
![triangle_texture](https://gitee.com/zhyounger/OpenGL-img/raw/master/triangleTexture.png)
![rectangle_texture](https://gitee.com/zhyounger/OpenGL-img/raw/master/rectangleTexture.png)
