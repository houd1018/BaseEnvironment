﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemyTraceState:State
{
    private float speed;
    private float moveDuration;
    public override void onEnter(StateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        moveDuration = 0.5f;
        /// <summary>
        /// 该状态是普通敌人的状态，从配置表中获取普通敌人的速度
        /// </summary>
        speed = DataManager.Instance.Enemies[2].Speed * Game.Instance.Difficulty;

    }
    public override void onFixedUpdate()
    {
        if (moveDuration > 0)
        {
            moveDuration -= Time.deltaTime;
            base.onFixedUpdate();
            if (Vector3.Distance(stateMachine.transform.position, Player.Instance.Character.transform.position) > 0.1f)
            {
                stateMachine.transform.Translate((Player.Instance.Character.transform.position - stateMachine.transform.position).normalized * Time.deltaTime * speed);
            }
        }
        else
        {
            State nextState = (State)new SpiderEnemyIdleState();
            stateMachine.SetNextState(nextState);

        }

    }
}
