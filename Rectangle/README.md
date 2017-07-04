> 本代码不包含必要的头文件，如果需要，请在 `OpenGL-Learning` -> `Example`中下载

## 一个正方形（由两个三角形组成，）

```
// ..:: 初始化代码 ::..
// 1、绑定顶点数组对象
glBindVertexArray(VAO);
// 2、把顶点数组复制到一个顶点缓冲中供OpenGL使用
glBindBuffer(GL_ARRAY_BUFFER, VBO);
glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);
// 3、复制我们的索引数组到一个索引缓冲中供OpenGL使用
glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);
// 4、设置顶点属性指针
blVertexAttribPointer(0, 3, GL_FLOAT, 3 * sizeof(float), (void*)0);
glEnableVertexAttribArray(0);

[...]

// ..:: 绘制代码（渲染循环中） ::..
glUseProgram(shaderProgram);
glBindVertexArray(VAO);
glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);
glBindVertexArray(0);
```
