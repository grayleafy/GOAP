using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarCore
{
    public interface IAStarNode
    {
        /// <summary>
        /// 可行边
        /// </summary>
        /// <returns></returns>
        List<IAStarEdge> GetEdges();

        /// <summary>
        /// 到达终点还需要的启发式消耗
        /// </summary>
        /// <returns></returns>
        float GetHeuristicCost(IAStarNode start, IAStarNode goal);

        /// <summary>
        /// node节点的世界参数是否满足自己的约束
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool IsFulfill(IAStarNode node);
    }
}

