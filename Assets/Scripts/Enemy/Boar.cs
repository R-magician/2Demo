using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
    //复写父类中的方法
    protected override void Awake()
    {
        base.Awake();
        //new 一个野猪的巡逻模式
        patrolState = new BoarPatrolState();
    }
}
