//敌人切换状态继承抽象类
public abstract class BaseState
{
    //当前对象
    protected Enemy currentEnemy;
    //进入
    public abstract void OnEnter(Enemy enemy);
    //逻辑更新
    public abstract void LogicUpdate();
    //物理更新
    public abstract void PhysicsUpdate();
    //退出
    public abstract void OnExit();
}
