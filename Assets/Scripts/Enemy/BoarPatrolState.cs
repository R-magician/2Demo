//野猪巡逻状态

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }

    public override void LogicUpdate()
    {
        //发现player切换到 chase(追击状态)
        if (currentEnemy.FoundPlayer())
        {
            //切换
            currentEnemy.SwitchState(NPCState.Chase);
        }
        else
        {
            //关闭跑步
            currentEnemy.anim.SetBool("run", false);
            //设置默认速度
            currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        }
        //如果碰到两侧的墙或者不是地面，角色翻转
        if (!currentEnemy.physicsCheck.isGround ||
            (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) ||
            (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            //关闭走路，开始等待
            currentEnemy.anim.SetBool("walk", false);
        }
        else
        {   //关闭等待，开启走路
            currentEnemy.wait = false;
            currentEnemy.anim.SetBool("walk", true);
        }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk", false);
    }
}