# 前言
**IceCreamView是什么？**
IceCreamView（以下简称ICView）是一套针对Unity3D的小游戏UI快速构建框架。
同时ICView是一套面向组件开发模式的UI框架，以容器+组件+管理器+配置表构成，框架的目的在于让开发者尽量专注于游戏逻辑而不是UI逻辑，减少UI上面耗费的时间。
ICView的代码有高复用性且便于灵活组装，针对小游戏开发周期短经常修改功能等问题来说ICView能够很好的适应小游戏开发环境，在制作某些常规的UI页面可以做到不需要额外编写代码。

# 框架结构
#### 整体框架
![IceCreamView 框架结构图](https://fold.oss-cn-shanghai.aliyuncs.com/Geeit/IcecreamView/IcecreamFramework.png)
ICView主要由Manager、Config、View/ViewModule组成，其大致流程是生成Config文件，在游戏初始化时将控制器和UIroot节点传入控制器中以初始化UI控制器，最后通过UI控制器继续UI控制。

#### 关于UI控制器
ICView通过控制器（GameViewManager）来管理所有页面，控制器提供了几乎所有在小型游戏开发中可能用到方法，同时允许你创建多个控制器控制多套Canvas下的UI，他们可以互相独立使用，比如在AR\VR开发下会出现这种情况。

#### 关于模块与页面
在ICView中每一个UI功能都可以理解成一个模块，比如一个关闭功能、一个跳转功能，这些功能模块通过连接器（GameViewModuleConnector）组成一个完整的页面，同时所有模块都共用一个生命周期，以优先级来判断模块间的执行先后顺序。
以下是一个页面/模块组的生命周期：
![IceCreamView页面生命周期](https://fold.oss-cn-shanghai.aliyuncs.com/Geeit/IcecreamView/IceCreamView.jpg)
不论是页面（GameViewAbstract）还是模块（GameViewAbstractModule）都具备以上生命周期。
另外模块除了提供的基础模块功能外可以自定义模块，只需要继承**GameViewAbstractModule**即可，同时也允许开发者直接继承GameViewAbstract自定义页面，不建议直接创建页面，建议通过组件的方式增加自己想要的功能，可以和其他组件搭配混用提高其复用性和灵活性，在实际使用时只需要将需要的功能组件拖动到UI预制体上即可。

#### 关于配置表
你所创建的UI预制体需要注册到一张.Asset格式的配置表中，无需考虑要挨个拖动预制体到配置表中这一繁琐的步骤，你可以直接选中所有的UI预制体右键选中IceCreamView选项中的自动生成配置表功能，它会自动将所选中动UI添加到一个新创建的配置表文件中，同时配置表文件存放到当前选中的目录（注意：如果当前目录已有配置表且名称相同会覆盖掉旧配置表）。
以下是配置表生成演示：
![Config自动生成演示](https://fold.oss-cn-shanghai.aliyuncs.com/Geeit/IcecreamView/IceCreamAutoConfig.gif)
以下是配置表面板：
![配置表面板](https://fold.oss-cn-shanghai.aliyuncs.com/Geeit/IcecreamView/IceCreamViewConfig.jpg)

# 使用说明

#### 示例：创建一个帮助提示页面
* 搭建好UI页面
![HelpPanel](https://fold.oss-cn-shanghai.aliyuncs.com/Geeit/IcecreamView/HelpPanel.jpg)
* 添加功能模块，由于提示页面只需要关闭/跳转功能所以我只需要增加两个基础模块，无需写新的模块，如下图
![HelpPanelModels](https://fold.oss-cn-shanghai.aliyuncs.com/Geeit/IcecreamView/HelpPanelModel.jpg)
如此一来，一个简易的提示页面就搭建完成了，用以上的方式创建另外两个页面GamePanel和MainPanel，最终实现如下效果：
![IceCreamSimple.gif](https://fold.oss-cn-shanghai.aliyuncs.com/Geeit/IcecreamView/IceCreamSimple.gif)
此外，通过另外一款UI动效插件（EasyAnimation）可以实现更加美观漂亮的效果，实际效果如下：
![IceCreamSimple2](https://fold.oss-cn-shanghai.aliyuncs.com/Geeit/IcecreamView/IceCreamSimple2.gif)

附上几种代码中常见的操作：

* 打开某个页面
```
viewManager.OpenView("GameHome");
```
* 关闭指定页面
```
viewManager.CloseView("GameHome");
```
* 获取游戏中的某一个页面
```
viewManager.GetView<GameViewAbstract>("GameHome");
```
# 未来计划
* 吸收MVVM设计模式的的viewModel模块，增加数据层并提供自动生成的响应式属性，从而不再需要在模块中更新UI数据。
* 更加完善的动态加载/卸载资源机制。

# 地址
[IceCreamView](https://gitee.com/Foldcc/Ice-creamView)：文中介绍的UI框架[gitee]
[EasyAnimation](https://gitee.com/Foldcc/EasyAnimation)：一款快捷的UI动效工具
