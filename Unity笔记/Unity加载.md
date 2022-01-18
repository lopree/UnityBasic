
<!-- TOC -->

- 1. 一、资源加载
- 2. 二、资源卸载
- 3. 三、文件路径
- 4. 四、特殊文件夹

<!-- /TOC -->

## 1. 一、资源加载

1. Resources加载

Resources类可以在指定的路径查找或访问资源。 在编辑器，Resources.FindObjectsOfTypeAll能用来定位资源和场景对象。 所有资源必须在项目Assets文件内的任意Resources文件夹中，可以通过Resources.Load访问。允许有多个Resources文件夹，每次加载对象是会自动检查。 需要注意的是：资源放在”Resources”文件夹中时，所有在“Resources”文件夹中的资源都将被打包编译包含在游戏中。当资源不再需要时，可以使用*Resources.UnloadUnusedAssets*回收内存。

1.1 示例代码

    using UnityEngine;
    using System.Collections;
    public class ExampleClass : MonoBehaviour {
        void Start() {

            var go = GameObject.CreatePrimitive(PrimitiveType.Plane);

            var rend = go.GetComponent<Renderer>();

            rend.material.mainTexture = Resources.Load("glass") as Texture;
        }
    }

1.2 静态函数

|名称 | 功能|
|:--|:--|
FindObjectsOfTypeAll| 返回所有type类型对象的一个列表。
Load| 加载储存在Resources文件夹中path处的资源。
LoadAll| 加载Resources文件夹中的path文件夹或者文件中的所有资源。
LoadAsync| 异步加载Resources文件夹中的资源。
UnloadAsset| 从内存卸载指定的资源。
UnloadUnusedAssets| 卸载未使用的资源。

2. WWW加载

这是一个简单的访问网页的类，通过链接WWW（url）在后台下载，并返回一个WWW对象，用isDone属性可以查看是否下载完成，或者yield自动等待下载物体。 WWW类可以用来发送GET和POST请求到服务器，WWW类默认使用Get方法，如果想使用POST方法，需要用WWWFrom构建一个postData参数，postData参数就是构建的表单数据。

2.1 示例代码

    using UnityEngine;using System.Collections;
    public class ExampleClass : MonoBehaviour {
        public string url = "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg";
        IEnumerator Start() {
            WWW www = new WWW(url);
            yield return www;
            renderer.material.mainTexture = www.texture;
        }}

2.2 变量

变量 | 功能|
|:--|:--|
assetBundle | AssetBundle的数据流，可以包含项目文件夹中的任何类型资源
audioClip | 从下载的数据，返回一个AudioClip（只读）
bytesDownloaded | 以字节组的形式返回获取到的网络页面中的内容(只读)
error | 返回一个错误消息，在下载期间如果产生了一个错误的话(只读)
isDone | 判断下载是否已经完成(只读)
movie | 从下载的数据，返回一个MovieTexture（只读）
progress | 下载进度有多少(只读)
text | 通过网页获取并以字符串的形式返回内容(只读)
texture | 从下载的数据返回一个Texture2D（只读）
textureNonReadable | 从下载的数据返回一个非可读的Texture2D（只读）
threadPriority | AssetBundle解压缩线程的优先级
uploadProgress | 上传进度有多少(只读)
utl | 此WWW请求的URL（只读）

函数

名称 | 功能|
|:--|:--|
GetAudioClip | 从下载数据，返回一个AudioClip（只读）
LoadImageIntoTexture | 利用一个从下载数据中的图像来替换现有Texture2D
LoadUnityWeb | 加载新的web播放器数据文件

静态函数

名称 | 功能|
|:--|:--|
EscapeURL | 转义字符串中的字符。
LoadFromCacheOrDownload | 从缓存加载带有指定版本号的AssetBundle。如果AssetBundle不在当前缓存，它将自动下载并储存在缓存，以便以后从本地存储检索。
UnEscapeURL | 将URL的转义字符恢复正常的文本。

3. AssetBundle加载

AssetBundles让你通过WWW类流加载资源并在运行时实例化它们。AssetBundles通过BuildPipeline.BuildAssetBundle创建。 注意，资源包在平台间不完全兼容。资源包编译为standalone平台（包含webplayer）可以被任意平台加载，但不包括iOS和Android平台。此外，资源包编译为iOS是不兼容Android，反之亦然。
示例代码

    using UnityEngine;
    using System.Collections;
    public class ExampleClass : MonoBehaviour {
    IEnumerator getAsset () {
    WWW www = new WWW ("http://myserver/myBundle.unity3d");
    yield return www;
    // Get the designated main asset and instantiate it.
    Instantiate(www.assetBundle.mainAsset);
    }
    }

变量

变量 | 功能 |
|:--|:--|
isStreamedSceneAssetBundle| 如果该资源包是一个流化的场景资源包，则返回true
mainAsset|主资源是在构建资源包时指定（只读）

函数

名称 | 功能|
|:--|:--|
Contains |如果AssetBundle的名称中包含特定的对象则进行检索
GetAllAssetNames |返回所有资源包所有的的资源名字
GetAllScenePaths |返回资源包中所有的场景资源路径( *.unity 资源路径)
LoadAllAssets |加载所有包含在资源包中继承自type的对象
LoadAllAssetsAsync |异步加载所有资源包中的资源
LoadAsset |根据资源名加载资源包中资源
LoadAssetAsync |根据资源名异步加载资源包中资源
LoadAssetWithSubAssets |根据资源名加载和替换资源包中资源
LoadAssetWithSubAssetsAsync |根据资源名异步加载和替换资源包中资源
Unload |卸载所有包含在bundle中的对象。

静态函数

名称 | 功能|
|:--|:--|
LoadFromFile|从硬盘来加载资源包
LoadFromFileAsync|从磁盘上的文件异步加载一个AssetBundle
LoadFromMemory|从内存区异步创建资源包
LoadFromMemoryAsync|从内存区域异步创建一个AssetBundle
LoadFromMemoryImmediate|从内存区同步创建资源包

## 2. 二、资源卸载

名称 | 功能|
|:--|:--|
Resources.UnloadAsset(Object assetToUnload)|卸载指定的asset，只能用于从磁盘加载的；如果场景中有此asset的引用，Unity会自动重新加载它；
Resources.UnloadUnusedAssets|卸载所有未被引用的asset，可以在画面切换时调用，或定时调用释放全局未使用资源； AssetBundle.Unload(false)|卸载AssetBundle的压缩文件数据（文件内存映像）；
AssetBundle.Unload(true)|卸载AssetBundle文件内存映像，并且释放所有已加载的asset；如果asset在场景中被引用，会丢失； Object.Destroy|销毁一个GameObject、组件或asset；并不是立即销毁，而是在Update循环之后，渲染之前；
Object.DontDestroyOnLoad|标明一个对象在切换场景时不被销毁；
 GC.Collect|强制垃圾收集器立即回收内存，可以根据需要使用，比如切换画面调用或定时调用；

## 3. 三、文件路径

名称 | 功能|
|:--|:--|
Application.dataPath路径| 这个属性返回的是程序的数据文件所在文件夹的路径，例如在Editor中就是项目的Assets文件夹的路径，通过这个路径可以访问项目中任何文件夹中的资源，但是在移动端它是完全没用。
Application.streamingAssetsPath路径| 这个属性用于返回流数据的缓存目录，返回路径为相对路径，适合设置一些外部数据文件的路径。在Unity工程的Assets目录下起一个名为“StreamingAssets”的文件夹即可，然后用Application.streamingAssetsPath访问，这个文件夹中的资源在打包时会原封不动的打包进去，不会压缩，一般放置一些资源数据。在PC/MAC中可实现对文件的“增删改查”等操作，但在移动端是一个只读路径。
Application.persistentDataPath路径（推荐使用）| 此属性返回一个持久化数据存储目录的路径，可以在此路径下存储一些持久化的数据文件。这个路径可读、可写，但是只能在程序运行时才能读写操作，不能提前将数据放入这个路径。在IOS上是应用程序的沙盒，可以被iCloud自动备份，可以通过同步推送一类的助手直接取出文件；在Android上的位置是根据Project Setting里设置的Write Access路径，可以设置是程序沙盒还是sdcard，注意：如果在Android设置保存在沙盒中，那么就必须root以后才能用电脑取出文件，因此建议写入sdcard里。一般情况下，建议将获得的文件保存在这个路径下，例如可以从StreamingAsset中读取的二进制文件或者从AssetBundle读取的文件写入PersistentDatapath。
Application.temporaryCachePath路径 |此属性返回一个临时数据的缓存目录，跟Application.persistentDataPath类似，但是在IOS上不能被自动备份。
/sdcard/..路径 |表示Android手机的SD卡根目录。
/storage/emulated/0/..路径|表示Android手机的内置存储根目录。

>以下是各路径在各平台中的具体位置信息：

4.1 PC端

名称 | 路径|
|:--|:--|
Application.dataPath|/Assets Application.streamingAssetsPath|/Assets/StreamingAssets Application.persistentDataPath|C:/Users/xxxx/AppData/LocalLow/CompanyName/ProductName Application.temporaryCachePath |C:/Users/xxxx/AppData/Local/Temp/CompanyName/ProductName
4.2 安卓端

名称 | 路径 |
|:--|:--|
 Application.dataPath | /data/app/xxx.xxx.xxx.apk Application.streamingAssetsPath | jar:file:///data/app/xxx.xxx.xxx.apk/!/assets Application.persistentDataPath | /data/data/xxx.xxx.xxx/files Application.temporaryCachePath | /data/data/xxx.xxx.xxx/cache
4.3 IOS端

名称 | 路径  |
|:--|:--|
Application.dataPath | Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data Application.streamingAssetsPath | Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data/Raw Application.persistentDataPath | Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Documents Application.temporaryCachePath | Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Library/Caches
4.4 HTML端

名称 | 路径 |
|:--|:--|
Application.dataPath | file:///D:/MyGame/WebPlayer (即导包后保存的文件夹，html文件所在文件夹)

## 4. 四、特殊文件夹

1. Editor

Editor文件夹可以在根目录下，也可以在子目录里，只要名子叫Editor就可以。比如目录：/xxx/xxx/Editor 和 /Editor 是一样的，无论多少个叫Editor的文件夹都可以。Editor下面放的所有资源文件或者脚本文件都不会被打进发布包中，并且脚本也只能在编辑时使用。一般呢会把一些工具类的脚本放在这里，或者是一些编辑时用的DLL。 比如我们现在要做类似技能编辑器，那么编辑器的代码放在这里是再好不过了，因为实际运行时我们只需要编辑器生成的文件，而不需要编辑器的核心代码。

2. Editor Default Resources

Editor Default Resources注意中间是有空格的，它必须放在Project视图的根目录下，如果你想放在/xxx/xxx/Editor Default Resources 这样是不行的。你可以把编辑器用到的一些资源放在这里，比如图片、文本文件、等等。它和Editor文件夹一样都不会被打到最终发布包里，仅仅用于开发时使用。你可以直接通过EditorGUIUtility.Load去读取该文件夹下的资源。

    TextAsset text =  EditorGUIUtility.Load("test.txt")as TextAsset;
    Debug.Log(text.text);

3. Gizmos

我觉得这个文件夹其实没什么用处，如下代码所示它可以在Scene视图里给某个坐标绘制一个icon。它的好处是可以传一个Vecotor3 作为图片显示的位置。 参数2就是图片的名子，当然这个图片必须放在Gizmos文件夹下面。

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "0.png", true);
    }

这里还是要说说OnDrawGizmos()方法，只要脚本继承了MonoBehaviour后，并且在编辑模式下就会每一帧都执行它。发布的游戏肯定就不会执行了，它只能用于在scene视图中绘制一些小物件。比如要做摄像机轨迹，那么肯定是要在Scene视图中做一个预览的线，那么用Gizmos.DrawLine 和Gizmos.DrawFrustum就再好不过了。

4. Plugins

如果做手机游戏开发一般 andoird 或者 ios 要接一些sdk 可以把sdk依赖的库文件 放在这里，比如 .so .jar .a 文件。这样打完包以后就会自动把这些文件打在你的包中。

5. Resources

可以在根目录下，也可以在子目录里，只要名子叫Resources就可以。比如目录：/xxx/xxx/Resources 和 /Resources 是一样的，无论多少个叫Resources的文件夹都可以。Resources文件夹下的资源不管你用还是不用都会被打包进.apk或者.ipa

Resource.Load ：编辑时和运行时都可以通过Resource.Load来直接读取。

Resources.LoadAssetAtPath() ：它可以读取Assets目录下的任意文件夹下的资源，它可以在编辑时或者编辑器运行时用，它但是它不能在真机上用，它的路径是”Assets/xx/xx.xxx” 必须是这种路径，并且要带文件的后缀名。

AssetDatabase.LoadAssetAtPath()：它可以读取Assets目录下的任意文件夹下的资源，它只能在编辑时用。它的路径是”Assets/xx/xx.xxx” 必须是这种路径，并且要带文件的后缀名。

我觉得在电脑上开发的时候尽量来用Resource.Load() 或者 Resources.LoadAssetAtPath() ，假如手机上选择一部分资源要打assetbundle，一部分资源Resource.Load().那么在做.apk或者.ipa的时候 现在都是用脚本来自动化打包，在打包之前 可以用AssetDatabase.MoveAsset()把已经打包成assetbundle的原始文件从Resources文件夹下移动出去在打包，这样打出来的运行包就不会包行多余的文件了。打完包以后再把移动出去的文件夹移动回来。

6. StreamingAssets

这个文件夹下的资源也会全都打包在.apk或者.ipa 它和Resources的区别是，Resources会压缩文件，但是它不会压缩原封不动的打包进去。并且它是一个只读的文件夹，就是程序运行时只能读 不能写。它在各个平台下的路径是不同的，不过你可以用Application.streamingAssetsPath 它会根据当前的平台选择对应的路径。

有些游戏为了让所有的资源全部使用assetbundle，会把一些初始的assetbundle放在StreamingAssets目录下，运行程序的时候在把这些assetbundle拷贝在Application.persistentDataPath目录下，如果这些assetbundle有更新的话，那么下载到新的assetbundle在把Application.persistentDataPath目录下原有的覆盖掉。

因为Application.persistentDataPath目录是应用程序的沙盒目录，所以打包之前是没有这个目录的，直到应用程序在手机上安装完毕才有这个目录。

StreamingAssets目录下的资源都是不压缩的，所以它比较大会占空间，比如你的应用装在手机上会占用100M的容量，那么你又在StreamingAssets放了一个100M的assetbundle，那么此时在装在手机上就会在200M的容量。
