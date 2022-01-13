
# 鼠标

## 一,鼠标的回调事件

> 前提:游戏物体身上必须有碰撞器

1.当鼠标点击到对象时调用

```cs
void OnMouseDown(){
  Debug.Log("OnMouseDown");
}
```

2.当鼠标取消点击对象时调用,鼠标松开时在游戏对象范围内或不在范围内都会触发该方法

```cs
void OnMouseUp(){
  Debug.Log("OnMouseUp");
}
```

3.当鼠标按住点击对象不放手是调用,鼠标从外部点击 移动 到游戏对象身上 不会调用该方法

```cs
void OnMouseDrag(){
  Debug.Log("OnMouseDrag");
}
```

4.当鼠标进入到游戏对象内部时调用(和碰撞器大小有关)

```cs
void OnMouseEnter(){
  Debug.Log("OnMouseEnter");
}
```

5.当鼠标离开内部(碰撞器)时调用

```cs
void OnMouseExit(){
  Debug.Log("OnMouseExit");
}
```

6.当鼠标持续留在游戏对象身上(碰撞器)时调用

```cs
void OnMouseOver(){
  Debug.Log("OnMouseOver");
}
```

7.像点击按钮一样点击游戏对象,鼠标松开时必须在游戏对象范围内才会触发该方法

```cs
void OnMouseUpAsButton(){
  Debug.Log("OnMouseUpAsButton");
}
```

## 二,将屏幕上的鼠标的位置转换为世界坐标的位置

```cs
void OnMouseDrag(){
  //获取鼠标在屏幕上的位置
  Vector3 mousePos = Input.mousePosition;
}
```

##  三,案例

### 案例1:当鼠标进入游戏物体时,物体会放大,离开时会缩小

```cs
// 鼠标是否进入游戏对象
bool isEnter = false;

// 记录原来的缩放比例
Vector3 originSacle;

void Start(){
originSacle = transform.localScale;
}

void Update(){
  //  如果将originSacle*2替换为localScale*2那么在 放大/缩小
  //  过程中快速将鼠标放入的话将会替换localScale*2中的localScale的值为 放大/缩小 后的值
  //  进而将会造成物体的持续 放大或缩小
if(isEnter){
  transform.localScale = Vector3.Lerp(localScale,
        originSacle*2,Time.deltaTime*10f);
}else {
   transform.localScale = Vector3.Lerp (transform.localScale,
    originScale,
    Time.deltaTime * 10f);
  }
}
void OnMouseEnter ()
 {
  isEnter = true;
}

 void OnMouseExit ()
 {
  isEnter = false;
}
```

### 案例2:控制摄像头移动及缩放摄像头

```cs
//摄像机距离鼠标在屏幕上的鼠标位置的最近和最远距离
public float minDis = 45f;
public float maxDis = 120f;

//鼠标移动的偏移量
float x,y;

//摄像机的旋转速度
public float rotSpeed = 89f;

//摄像机的缩放速度
public float zoomSpeed = 10f;

void Update(){
  if (Input.GetMouseButton (0)) {
     x = Input.GetAxis ("Mouse X");
     y = Input.GetAxis ("Mouse Y");
     if (x != 0) {
      //左右绕y轴旋转
      transform.RotateAround (tank.position, Vector3.up, x * rotSpeed * Time.deltaTime);
     }
     if (y != 0) {
      //上下旋转
      //transform.right:绕摄像机的x轴旋转
      transform.RotateAround (tank.position, transform.right, y * rotSpeed * Time.deltaTime);
     }
    }
    //按下鼠标右键,让摄像机围绕坦克旋转,但是坦克跟着旋转
    if (Input.GetMouseButton (1)) {
     x = Input.GetAxis ("Mouse X");
     y = Input.GetAxis ("Mouse Y");
     //摄像机旋转,坦克旋转
     if (x != 0) {
      tank.Rotate (tank.up, x * rotSpeed * Time.deltaTime);
     }
     if (y != 0) {
      transform.Rotate(tank.position, transform.right, -y * Time.deltaTime * rotSpeed);
     }
    }

    //控制摄像机的远近
    if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
     //摄像机可观察的尺寸
     float f = Camera.main.fieldOfView;
     //改变可观察尺寸
     f += Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed;
     //做一个最远和最近的限定
     f = Mathf.Clamp (f, minDis, maxDis);
     //重新复制给摄像机
     Camera.main.fieldOfView = f;

    }
}
```
