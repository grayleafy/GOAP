using AStarCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar
{
    /// <summary>
    /// 已经探索过的节点以及需要的路径消耗,         
    /// </summary>
    Dictionary<IAStarNode, (float pathCost, IAStarEdge preEdge, IAStarNode preNode)> pathCostDict = new();
    /// <summary>
    /// 可达节点
    /// </summary>
    SortedSet<AccessibleNode> accessibleNodes = new SortedSet<AccessibleNode>();

    public List<IAStarEdge> Run(IAStarNode start, IAStarNode goal)
    {
        Reset();
        //放入起点
        PushNode(start, 0, null, null, start, goal);
        while (accessibleNodes.Count > 0)
        {
            AccessibleNode accessibleNode = accessibleNodes.First();
            accessibleNodes.Remove(accessibleNode);

            PushNode(accessibleNode.node, accessibleNode.pathCost, accessibleNode.edge, accessibleNode.preNode, start, goal);

            //到达终点,计算路径
            if (goal.IsFulfill(accessibleNode.node))
            {
                List<IAStarEdge> paths = new List<IAStarEdge>();
                IAStarNode cur = accessibleNode.node;
                while (cur != start)
                {
                    paths.Add(pathCostDict[cur].preEdge);
                    cur = pathCostDict[cur].preNode;
                }
                //翻转
                for (int i = 0; i * 2 < paths.Count; i++)
                {
                    var temp = paths[i];
                    paths[i] = paths[paths.Count - i - 1];
                    paths[paths.Count - i - 1] = temp;
                }

                return paths;
            }


        }

        return null;
    }

    //重置
    private void Reset()
    {
        pathCostDict.Clear();
        accessibleNodes.Clear();
    }

    //选择一个节点
    private void PushNode(IAStarNode node, float pathCost, IAStarEdge preEdge, IAStarNode preNode, IAStarNode start, IAStarNode goal)
    {
        pathCostDict.Add(node, (pathCost, preEdge, preNode));
        //更新可达节点
        List<IAStarEdge> edges = node.GetEdges();
        foreach (IAStarEdge edge in edges)
        {
            AccessibleNode accessibleNode = new AccessibleNode();
            accessibleNode.node = edge.GetNextNode(node);
            accessibleNode.preNode = node;
            accessibleNode.edge = edge;
            accessibleNode.pathCost = pathCost + edge.GetCost();
            accessibleNode.heuristicCost = accessibleNode.node.GetHeuristicCost(start, goal);

            accessibleNodes.Add(accessibleNode);
        }
    }
}


namespace AStarCore
{
    /// <summary>
    /// 可访问节点
    /// </summary>
    public class AccessibleNode : IComparable<AccessibleNode>
    {
        public IAStarNode node;
        public IAStarNode preNode;
        public IAStarEdge edge;
        public float pathCost;
        public float heuristicCost;

        public int CompareTo(AccessibleNode other)
        {
            float totalCost = pathCost + heuristicCost;
            float otherTotalCost = other.pathCost + other.heuristicCost;
            if (totalCost != otherTotalCost)
            {
                return totalCost.CompareTo(otherTotalCost);
            }
            if (node != other.node)
            {
                return node.GetHashCode().CompareTo(other.node.GetHashCode());
            }
            if (preNode != other.preNode)
            {
                return preNode.GetHashCode().CompareTo(other.preNode.GetHashCode());
            }
            if (edge != other.edge)
            {
                return edge.GetHashCode().CompareTo(other.edge.GetHashCode());
            }

            return 0;
        }
    }
}

