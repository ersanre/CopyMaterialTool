# CopyMaterialTool

Unity 编辑器工具，一键复制并引用材质球。

### 作用:

* 简化编辑美术效果时复制材质球，引用材质球的过程。
* 之前：

  1. 点击 inspector 面板上的材质球定位到 project 中的位置
  2. 点击 project 中的材质球复制，
  3. 找到复制出来的材质球，并拖入面板中的材质球槽

      > 不爽的点：如果不小心点击了材质球 inspector 面板将会改变，又要重写选中 Gameobject，来回折腾被打断，可能就忘了复制出来的是哪个材质球，很糟心。
      >

* 现在：

  点击复制按钮即可

### 功能：

* 复制当前材质球并赋予。

![1](https://user-images.githubusercontent.com/50767444/206858646-2a4b2fee-eaf2-4260-83c7-943ee3e624a3.gif)

* 关联收藏夹，可以选择复制的目标目录， 默认复制到当前材质球目录。

  ![2](https://user-images.githubusercontent.com/50767444/206858669-3d2def84-53e4-495d-8c89-fa31f3c79528.gif)


### 使用说明：

下载后放入工程的 Editor 目录，如果没有可以在 Aseets 下创建 Editor 目录。

开发版本：unity 2019 4.16
