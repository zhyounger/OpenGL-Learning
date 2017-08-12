# 纹理单元

> 纹理单元主要目的是让我们在着色器中可以使用多于一个的纹理。通过把纹理单元赋值给采样器，我们可以一次绑定多个纹理，只要我们首先激活对应的纹理单元。就像`glBindTexture`一样，我们可以使用管理`ActiveTexture`激活纹理单元，传入我们需要使用的纹理单元。


```
glActiveTexture(GL_TEXTURE0):
glBindTexture(GL_TEXTURE_2D, texture);
```

激活纹理单元后，使用glBindTexture函数绑定这个纹理到当前激活的纹理单元，纹理单元GL_TEXTURE0默认总是被激活。

*OpenGL至少保证16个纹理单元供使用，也就是说可以激活从GL_TEXTURE0到GL_TEXTURE15。*

我们仍需要编辑片段着色器来接受另一个采样器：

```
#version 410 core
···

uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
    FragColor = mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.2);
}
```

*最终输出颜色是两个纹理的结合。第三个参数是对两个纹理颜色进行线性插值。*

载入第二张纹理步骤与之前一样。为了使用两个纹理，需改变渲染流程。先绑定两个纹理到对应的纹理单元，然后定义哪个uniform采样器对应哪个纹理单元：

```
glActiveTexture(GL_TEXTURE0);
glBindTexture(GL_TEXTURE_2D, texture1);
glActiveTexture(GL_TEXTURE1);
glBindTexture(GL_TEXTURE_2D, texture2);

glBindVertexArray(VAO);
glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);
```

还需要通过使用`glUniform1i`设置每个采样器的方式告诉OpenGL每个着色器采样器属于哪个纹理单元。只需要设置一次即可，并且在渲染循环之前设置：

```
ourShader.use();
glUniform1i(glGenUniformLocation(ourShader.ID, "texture1"), 0); // 手动设置
ourShader.setInt("texture2", 1); // 或使用着色器类设置
```

最终结果如下图：
![image](http://git.oschina.net/orient01/OpenGL-img/raw/master/texture_unit1.png)

因为OpenGL要求y轴0.0坐标是在图片的底部的，但是图片的y轴0.0坐标通常在顶部，所以纹理上下颠倒了，我们只需要在加载任何图像之前加入以下语句：

```
stbi_set_flip_vertically_on_load(true);
```

结果如下图：

![image](http://git.oschina.net/orient01/OpenGL-img/raw/master/texture_unit2.png)
