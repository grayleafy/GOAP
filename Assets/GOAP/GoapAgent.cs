using GoapCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoapAgent : MonoBehaviour
{
    public GoapPlanner goapPlanner = new GoapPlanner();
    public List<GoapAction> goapActions = new List<GoapAction>();

    public void Start()
    {
        Test();
    }
    public void Test()
    {
        //起点
        GoapMemory memory = new GoapMemory();
        memory.wolrdState.agent = this;
        memory.wolrdState.SetWorldParam(new WorldParamBool() { key = "拥有剑", value = false });
        memory.wolrdState.SetWorldParam(new WorldParamBool() { key = "拥有钱", value = true });
        memory.wolrdState.SetWorldParam(new WorldParamBool() { key = "附近有剑", value = true });
        memory.wolrdState.SetWorldParam(new WorldParamInt() { key = "敌人生命值", value = 1000 });

        //动作
        goapActions.Add(new GetSword() { cost = 1 });
        goapActions.Add(new BuySword() { cost = 2 });
        goapActions.Add(new HitEnemy() { cost = 30 });

        //目标
        GoapGoal goal = new GoapGoal();
        goal.SetWorldParam(new WorldParamInt() { key = "敌人生命值", constraint = WorldParam.ConstraintEnum.LessThanOrEqual, value = 0 });

        var plan = goapPlanner.Plan(memory, goal);
    }
}
