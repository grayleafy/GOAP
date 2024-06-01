using GoapCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoapPlanner
{
    AStar astar = new AStar();

    /// <summary>
    /// 规划计划，返回空则表示不存在能完成目标的计划
    /// </summary>
    /// <param name="memory"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
    public List<GoapAction> Plan(GoapMemory memory, GoapGoal goal)
    {
        var astarEdges = astar.Run(memory.wolrdState, goal);
        if (astarEdges == null) return null;
        List<GoapAction> goapActions = new List<GoapAction>();
        foreach (var edge in astarEdges)
        {
            goapActions.Add(edge as GoapAction);
        }
        return goapActions;
    }
}
