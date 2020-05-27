AudioPlayer
===========================

该工程是一个基于Unity2018.3.13开发的简易音频播放器，支持歌词同步+音频可视化+MV同步……
![AudioPlayer](https://github.com/xuchaoqvn/AudioPlayer/blob/master/Textures/AudioPlayer.gif "AudioPlayer")  

****
## 目录
* [前言](#前言)
* [格式说明](#格式说明)
* [配置文件和设置](#配置文件和设置)
* [歌词的编码](#歌词的编码)
* [MP3](#MP3)
* [DrawCall](#DrawCall)
* [收获和遗憾](#收获和遗憾)

### 前言
在我Unity工作的第一年里，通过学习和研究积累了一些比较有意思的小功能，比如：  
解析后缀名为.lrc的歌词文件、  
获取音频数据、
```C#
audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
```  
歌词同步……  
当时就有个想法能不能把这些小功能组合起来做成一个完整的程序，不过那个时候自身代码水平有点差功力不足，不足以构建一个完整的程序；经过一年多的学习和积累，有了一定信心，把这些小功能融合成完整的程序，在这个过程中有收获当然也有遗憾……

### 格式说明
在StreamingAssets文件夹下四个文件夹：
![StreamingAssets](https://github.com/xuchaoqvn/AudioPlayer/blob/master/Textures/StreamingAssets.jpg "StreamingAssets")  
其中，
Audios存放的是音频，支持的格式为：`.wav`、`.mp3`、`.ogg`、`.aif`，你没看错，支持mp3，后边说明；  
Configs文件夹的是配置文件（xml格式）；  
Lyrics文件夹存放的是后缀名为.lrc的歌词文件；  
Movies文件夹存放时的后缀名为.MP4的MV视频;  
需要注意的是歌曲、歌词、和视频必须同名，其中视频文件可以没有，但歌词文件一定要存在。否则，程序会一直在加载状态下。  

### 配置文件和设置
配置文件如下：  
```XML
<?xml version="1.0" encoding="utf-8"?>
<items>
  <!--半径-->
  <item name="radius" value="3.5" />
  <!--x轴偏移-->
  <item name="offsetX" value="0" />
  <!--y轴偏移-->
  <item name="offsetY" value="0.7" />
  <!--可视化数据数量-->
  <item name="audioDataCount" value="64" />
  <!--线宽-->
  <item name="lineWidth" value="0.02" />
  <!--最小高度-->
  <item name="minHight" value="0.14" />
  <!--最大高度-->
  <item name="maxHight" value="1.4" />
  <!--亮度-->
  <item name="colorBrightness" value="3.415" />
  <!--主颜色-->
  <item name="AlphaEdge" value="#0000FF" />
  <!--次要颜色-->
  <item name="viceColor" value="#00FF00" />
  <!--更新间隔-->
  <item name="updateInterval" value="0.08" />
  <!--放大因子-->
  <item name="amplificationFactor" value="14" />
  <!--旋转速度-->
  <item name="rotateSpeed" value="10" />
  <!--基础字体颜色-->
  <item name="baseFontColor" value="#FF45AC" />
  <!--当前字体颜色-->
  <item name="currentFontColor" value="#A3FF06" />
  <!--字体大小-->
  <item name="fontSize" value="30" />
</items>
```
注意：可视化数量只能是2的幂（64、128、256、512）；颜色使用的是16进制表示的（#FFFFFF）
设置如下：
![Setting](https://github.com/xuchaoqvn/AudioPlayer/blob/master/Textures/Setting.gif "Setting")  

### 歌词的编码
关于.lrc歌词文件读取的编码，在unity中读取后显示的是一堆乱码，由于Unity的中文是utf-8,在查找解决方案时，大部分时通过另存来改变编码；但假如文件过多，基本不太可能去单独一个一个去处理，我尝试通过utf-8和默认的编码去处理，但结果依然是乱码，最后的解决方法如下：  
```C#
Encoding.GetEncoding("gb2312").GetString
```
gb2312,关于编码的问题，本人非科班出身，基础稍微薄弱。
另外说明一点，由于使用gb2312编码去解析歌词文件，在编辑器下运行没有问题，不过在打包后运行，会报`NotSupportedException: Encoding 936 data could not be found. Make sure you have correct international codeset assembly installed and enabled.`这样一个错误，
是由于独立播放器中缺少I18N.dll和I18N.CJK.dll，可以在Unity的安装目录`Unity\Editor\Data\Mono\lib\mono\unity`中找到。

### MP3
unity2018依然不支持外部读取MP3文件，为了解决这个问题，通过[NAudio](https://github.com/naudio/NAudio "NAudio")对MP3进行处理；由于unity的音频文件是AudioClip类型，因此通过NAudio有两种处理方式：  
1. 通过NAudio读取MP3，通过额外的类去处理成AudioClip,不过和原音频相比，音质稍差；
2. 通过NAudio直接把MP3转化为WAV，不过转换之后体积大概会变成原来的10倍左右。

由于第一种方式的实现在一次U盘报废的过程中随风而起，只能通过第二种方法实现。

### DrawCall
在数据可视化的相关代码中，最初代码是：  
```C#
for (int i = 0; i < parameter.AudioDataCount; i++)
{
	audioDatas[i] = GameObject.Instantiate<GameObject>(audioData, audioDatalayer.transform, false).transform;
	audioDatas[i].localPosition = new Vector3(Mathf.Sin(angle * i) * parameter.Radius, Mathf.Cos(angle * i) * parameter.Radius, 0.0f);
	audioDatas[i].localEulerAngles = new Vector3(0.0f, 0.0f, -i * eulerAngle);
	audioDatas[i].localScale = new Vector3(parameter.LineWidth, parameter.MinHight, 1.0f);
	material = audioDatas[0].GetComponent<MeshRenderer>().material;
	material.SetColor("_MainColor", mainColor);
	material.SetColor("_ViceColor", viceColor);
	material.SetFloat("_Brightness", brightness);
	audioDatas[i].gameObject.name = name + i.ToString();
}
```
这样的后果是导致每生成一个，就多一个DrawCall,最后查询到通过Renderer.Material进行参数传递，导致Unity会自动克隆出一个新的Material,避免方法是通过Renderer.SharedMaterial进行传参，同时修改PlayerSettings下的Dynamic Batching。
![Dynamic Batching](https://github.com/xuchaoqvn/AudioPlayer/blob/master/Textures/DynamicBatching.jpg "Dynamic Batching")  

### 收获和遗憾
这个工程是本人比较认真去写的，毕竟是刚工作时想写的程序，在这个工程里比较完善了一套简单的用于交互的UI框架（虽然现在没写），收获还是可以的；
我本人比较奇怪的是为什么读取音频和视频文件后，运行内存会变得很大，不太清楚一个2m、3m的音频加载之后会内存占用近10倍的？？？……
由于本人对于程序的架构能力太弱，导致工程经历了两次较大的脚本变更、一次命名空间的变更，几乎推倒重来，仅仅是想写出一个比较满意的程序架构，虽说现在写的也不怎么样……

