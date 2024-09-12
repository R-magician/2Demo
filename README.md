# 2D横板-勇士传说

![image.png](Assets%2FArtAssets%2FMarkDom%2Fimage.png)

> 主要使用像素动画进行动作设计
使用`ScriptableObject` 进行页面的设计的 **广播、监听**
> 

### 使用插件

1. **Input System 新版玩家输入系统**
2. **Cinemachine 摄像机工具(相机跟随,相机边界限定)**
3. **Addressables一个打包的工具，灵活处理场景或其他物体的加载**
4. **DOTween第三方动画插件**

### **涉及到的设计模式**

1. 单例模式（DataManger）
2. 发布订阅模式（跨对象调用事件DataSO→Events）

### 钩子函数

`OnDrawGizmosSelected` 在编辑模式下绘制范围

`OnValidate` 当脚本的属性在编辑器中被修改时调用

### C#

```csharp
[Header("基本参数")]//面板上进行切割

[HideInInspector]public Animator anim;//不需要暴露在面板的可以隐藏

[CreateAssetMenu(fileName = "文件名", menuName = "编辑器中右键菜单下的路径", order = 排序)]

[SerializeField]//将私有（private）字段在 （检查器）中可见和可编辑，而无需将字段公开

[DefaultExecutionOrder(-100)]//确保脚本按照指定的顺序执行,整数值越小，越先执行。
默认情况下，所有脚本的执行顺序为 0。
```

1. unity中使用数学方法MathF，Math是.NET使用的方法
2. `normalized` 归一化，防止数字过大；
3. `UnityEvent` 自定义unity中的事件栈，可以调用其他组件中的方法
4. `public virtual void Move()`  virtual 虚的不确定，指可以在子类访问来修改（重写方法）
5. 携程

```csharp
//在其他地方开启携程
StartCoroutine(OnHurt(dir));

//使用携程的方式使攻击后重新走起来--返回一个迭代器
private IEnumerator OnHurt(Vector2 dir)
{
     //添加力,Impulse--冲击力
     rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
     //等待条件
     yield return new WaitForSeconds(0.5f);
     //受伤关闭
     isHurt = false;
 }
```

6、switch，更具不同值，赋值

```csharp
//调用切换
 		SwitchState(NPCState.Chase)
 		
 		//切换状态  state值为NPCState.Chase->chaseState
    public void SwitchState(NPCState state)
    {
        //更具传入的状态赋值
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null//默认的状态
        };
        
        //状态退出
        currentState.OnExit();

        //切换传入的状态
        currentState = newState;
    }
```

7、轻量级的对象类型（ScriptableObject），发布订阅事件

> `ScriptableObject` 是一种**轻量级的对象类型**，主要用于存储**独立于场景**的共享数据或配置信息。与 `MonoBehaviour` 不同，`ScriptableObject` 不需要附加到游戏对象上，而是作为独立的资源文件存在于项目中，可以被多个游戏对象共享使用。
> 

8、GUID（Global Unique Identifier）**—全局唯一标识符（可以作为物体的ID）**
`System.Guid.NewGuid().ToString()`

9、Time.timeScale，控制游戏速度的一种方式，`Time.timeScale = 0` 游戏暂停；
`Time.timeScale = 1` 游戏正常运行；`Time.timeScale = 0.5;`   游戏0.5倍数。
