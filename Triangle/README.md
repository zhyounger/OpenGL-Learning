> 本代码不包含必要的头文件，如果需要，请在 `OpenGL-Learning` -> `Example`中下载

## 一个三角形

```
// ..:: 初始化代码（只运行一次，除非物体频繁改变）::..
// 1、绑定VAO
glBindVertexArray(VAO);
// 2、把顶点数组复制到缓冲中供OpenGL使用
glBindBuffer(GL_ARRAY_BUFFER, VBO);
glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);
// 3、设置顶点属性指针
glVertexAttribPoint(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
glEnableVertexAttribArray(0);

[...]

// ..:: 绘制代码（渲染循环中） ::..
// 4、绘制物体
glUseProgram(shaderProgram);
glBindVertexArray(VAO);
someOpenGLFunctionThatDrawsTriangle();
```
