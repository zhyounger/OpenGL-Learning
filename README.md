> This is a OpenGL-Learning repository, I'm learning OpenGL from "https://learnopengl-cn.github.io", and I will put my code here .

## Learning OpenGL on Xcode

### 安装glfw

> glfw是一个OpenGL的应用框架，支持Linux和Windows。glfw主要用来处理特定操作系统下的特定任务，例如OpenGL窗口管理、分辨率切换、键盘、鼠标以及游戏手柄、定时器输入、线程创建等。

1、在终端中运行命令`brew install glfw3`

glfw将会被安装在`/usr/local/Cellar/glfw`

2、配置路径

在`/usr/local/Cellar/glfw`路径下找到`header`和`lib`对应路径，在`Xcode->Preferences->Localtion->Custom Paths`设置路径。

![image](https://github.com/Orient-ZY/OpenGL-Learning/blob/master/img/Localtion.png)

3、配置`header/library searcher path`


