> This is a OpenGL-Learning repository, I'm learning OpenGL from "https://learnopengl-cn.github.io", and I will put my code here .

# Learning OpenGL on Xcode

## 安装glfw

> glfw是一个OpenGL的应用框架，支持Linux和Windows。glfw主要用来处理特定操作系统下的特定任务，例如OpenGL窗口管理、分辨率切换、键盘、鼠标以及游戏手柄、定时器输入、线程创建等。

1、在终端中运行命令`brew install glfw3`

glfw将会被安装在`/usr/local/Cellar/glfw`

2、配置路径

在`/usr/local/Cellar/glfw`路径下找到`header`和`lib`对应路径，在`Xcode->Preferences->Localtion->Custom Paths`设置路径。

![image](https://github.com/Orient-ZY/OpenGL-Learning/blob/master/img/Localtion.png)

3、配置`header/library searcher path`

在项目`Build Settings`中搜索`header search`，添加如下配置：

![image](https://github.com/Orient-ZY/OpenGL-Learning/blob/master/img/header.png)

搜索`library search`，添加如下配置：

![image](https://github.com/Orient-ZY/OpenGL-Learning/blob/master/img/library.png)

4、在项目中导入需要的库文件，如下图示：

![image](https://github.com/Orient-ZY/OpenGL-Learning/blob/master/img/linked.png)

## 配置glad

> OpenGL只是一个标准/规范，具体的实现是有驱动开发商针对特定显卡实现的。由于OpenGL驱动版本众多，它大多数函数的位置都无法在编译时确定下来，需要在运行时查询。开发者需要在运行时获取函数地址并将其保存在一个函数指针中供以后使用。而取得地址的方法非常复杂、繁琐。glad库是目前最新、最流行的简化此过程的库。

1、打开glad的[在线服务](http://glad.dav1d.de/)

2、将语言设置为C/C++，在API选项中，选择3.3以上的OpenGL版本（我的电脑选择的是4.1版本，3.3及更新的版本也能正常工作）

3、将模式（Profile）设置为Core，保证生成加载器（Generate a loader）选项是选中的。

4、先暂时忽略拓展（Extensions）中内容。点击生成（Generate）

5、下载生成的zip包（包含`glad.c`、`glad,h`和`khrplatform.h`），解压添加到项目中。

# 至此Mac配置基本完成

# Learning OpenGL on Visual Studio 2017

## 配置OpenGL

1、新建Win32 控制台应用程序，勾选空项目。

2、在菜单：项目->管理NuGet程序包 中搜索`nupengl`安装`nupengl.core`

## 配置glad库

1、在glad[在线服务](http://glad.dav1d.de/)中配置glad，方法同Xcode。

2、将生成的zip文件下载解压。将其中的`include`与`src`文件夹复制到项目目录中，并将三个文件拖入项目中

![image](https://github.com/Orient-ZY/OpenGL-Learning/blob/    master/img/ScreenShot.jpg)

3、在项目->属性->VC++目录中，添加包含目录和源目录


![image](https://github.com/Orient-ZY/OpenGL-Learning/blob/    master/img/win-include.jpg)
![image](https://github.com/Orient-ZY/OpenGL-Learning/blob/    master/img/win-src.jpg)

# 至此Visual Studio配置基本完成
