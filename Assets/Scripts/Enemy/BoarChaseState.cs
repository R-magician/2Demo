using UnityEngine;

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        //更改追击速度
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        //播放动画
        currentEnemy.anim.SetBool("run",true );
    }

    public override void LogicUpdate()
    {
        //检测丢失掉范围玩家
        if (currentEnemy.lostTimeConter <= 0)
        {
            //切换巡逻
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        //如果碰到两侧的墙或者不是地面，角色翻转
        if (!currentEnemy.physicsCheck.isGround ||
            (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) ||
            (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        //currentEnemy.lostTimeConter = currentEnemy.lostTime;
        currentEnemy.anim.SetBool("run", false);
    }
}
