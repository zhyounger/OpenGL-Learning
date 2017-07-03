## 着色器(shaders)

> 结构(structure)

```
#version version_number
in type in_variable_name;
in type in_variable_name;

out type out_variable_name;

uniform type uniform_name;

int main()
{
    // 处理输入并进行一些图形操作
    ...
    // 输出处理的结果到输出变量
    out_variable_name = weird_stuff_we_processed;
}
```

> 查询顶点属性上限

查询 GL_MAX_VERTEX_ATTRIBS 获取能申明的顶点属性上限（一般由硬件决定，OpenGL确保至少有16个包含4分量的顶点属性可用）

```
unsigned int nrAttributes;
glGetIntegerv(GL_MAX_VERTEX_ATTRIBS, &nrAttributes);
std::cout << "Maximun nr of vertex attributes supported:" << nrAttributes << std::endl;
```

> vector（大多时候我们使用vecn，float足够满足大多数要求）

```
vecn：包含n个float分量的默认向量
bvecn：包含n个bool分量的向量
ivecn：包含n个int 分量的向量
uvecn：包含n个unsigned int分量的向量
dvecn：包含n个double分量的向量
```

> 分量获取

向量的分量可通过`vec.x`这种方式获取，`vec.x`、`vec.y`、`vec.z`、`vec.w`获取第1、2、3、4个分量。GLSL也允许对颜色使用`rgba`，或是对纹理坐标使用`stpq`访问相同的分量

> 向量重组（Swizzling）

```
vec2 someVec;
vec4 differentVec = someVec.xyxx;
vec3 anotherVec = differentVec.zyw;
vec4 otherVec = someVec.xxxx + anotherVec.yxzy
```

```
vec2 vect = vec2(0.5, 0.7);
vec4 result = vec4(vect, 0.0, 0.0);
vec4 otherResult = vec4(result.xyz, 1.0);
```

> 输入与输出

GLSL定义了`in`和`out`关键字来实现着色器的输入输出，只要一个输出变量与下一个着色器阶段的输入匹配，它就会传递下棋，但在顶点和片段着色器中会有点不同

顶点着色器的输入特殊在它从顶点数据中直接接收输入。为了定义定点数据该如何管理，使用`location`这一元数据指定输入变量，这样就可以在CPU上配置顶点属性。顶点着色器需要为它的输入提供一个额外的`layout`标识，这样才能把它链接到顶点数据。

> 改动程序中的着色器

顶点着色器：

```
#version 410 core
layout (location = 0) in vec3 aPos; // 位置变量的属性值为0

out vec4 vertexColor;   // 为片段着色器指定一个颜色输出

void main()
{
    gl_Position = vec4(aPos, 1.0);  // 注意，这里把一个vec3作为vec4的构造器参数
    vertexColor = vec4(0.5, 0.0, 0.0, 1.0); // 把输出变量设置为暗红色
}
```

片段着色器：

```
#version 410 core
out vec4 FragColor;

in vec4 vertexColor;    // 从顶点着色器传来的输入变量（名称、类型相同）

void main()
{
    FragColor = vertexColor;
}
```

这样完成了从顶点着色器向片段着色器发送数据，改变了三角形的颜色

> Uniform

Uniform是一种从CPU中的应用向GPU中的着色器发送数据的方式，但它和定点属性有些不同。

1、Uniform是全局的，Uniform变量必须在每个着色器程序对象中都是独一无二的，而且它可以被着色器程序的任意着色器在任意阶段访问。
2、无论把Uniform值设置成什么，它会一直保存它们的数据，直到它们被重置或更新。

在一个着色器中添加`Uniform`关键字至类型和变量名前来生命一个GLSL的Uniform。

```
#version 410 core
out vec4 FragColor;

uniform vec4 ourColor;  // 在OpenGL程序代码中设定这个变量

void main()
{
    FragColor = ourColor;
}
```
**如果申明了一个uniform却在GLSL代码中没用过，编译器会静默移除这个变量，导致最后编译出的版本中不会包含它，这可能导致几个非常麻烦的错误！**

接下来给uniform添加数据，使得三角形颜色随时间而改变

```
// 获取运行的秒数
float timeValue = glfwGetTime();
// sin函数（引入cmath）让颜色在0.0到1.0之间改变
float greenValue = sin(timeValue) / 2.0f + 0.5f;
// 查询uniform ourColor的位置值，如果返回-1则表示没有找到这个位置值
int vertexColorLocation = glGetUniformLocation(shaderProgram, "ourColor");
glUseProgram(shaderProgram);
// 设置uniform值。
glad_glUniform4f(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);
```

**注意，查询uniform地址不要求你之前使用过的着色器程序，但更新一个uniform之前必须先使用程序（调用glUseProgram），因为它是在当前激活的着色器中设置uniform的**

因为OpenGL其核心是一个C库，所以它不支持类型重载，在函数参数不同的时候就要为其定义新的函数；`glUniform`就是一个典型的例子：

```
后缀  含义
f    函数需要一个float作为它的值
i    函数需要一个int作为它的值
ui   函数需要一个unsigned int作为它的值
3f   函数需要3个float作为它的值
fv   函数需要一个float向量/数组作为它的值
```
