using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoapCore
{
    /// <summary>
    /// 世界参数
    /// </summary>
    public abstract class WorldParam : IDifference
    {
        /// <summary>
        /// 约束类型
        /// </summary>
        public enum ConstraintEnum
        {
            None,
            LessThan,       // 小于
            LessThanOrEqual, // 小于等于
            GreaterThan,    // 大于
            GreaterThanOrEqual, // 大于等于
            Equal // 等于
        }

        //键值
        public string key;
        //约束
        public ConstraintEnum constraint = ConstraintEnum.None;
        //值
        public abstract object ValueObj { get; set; }

        /// <summary>
        /// 是否满足约束
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract bool IsFulfill(WorldParam other);

        public abstract WorldParam Clone();
        public abstract float Difference(WorldParam param);
    }

    public abstract class WorldParam<T> : WorldParam
    {
        public T value;
        public override object ValueObj { get => value; set => this.value = (T)value; }

        public override bool IsFulfill(WorldParam o)
        {
            WorldParam<T> other = o as WorldParam<T>;
            if (other == null)
            {
                Debug.LogError("两个世界参数判断是否满足时类型不匹配");
                return false;
            }

            if (constraint == ConstraintEnum.None)
            {
                return true;
            }

            if (constraint == ConstraintEnum.Equal)
            {
                if (value as IEquatable<T> != null && other as IEquatable<T> != null)
                {
                    return (value as IEquatable<T>).Equals(other.value);
                }
                else
                {
                    return value.GetHashCode() == other.value.GetHashCode();
                }
            }

            if (o.ValueObj as IComparable == null || ValueObj as IComparable == null)
            {
                Debug.LogError($"世界参数类型不支持比较函数: {key}");
            }
            int cmp = (o.ValueObj as IComparable).CompareTo(ValueObj);
            if (constraint == ConstraintEnum.LessThan)
            {
                return cmp < 0;
            }
            if (constraint == ConstraintEnum.LessThanOrEqual)
            {
                return cmp < 0 || cmp == 0;
            }
            if (constraint == ConstraintEnum.GreaterThan)
            {
                return cmp > 0;
            }
            if (constraint == ConstraintEnum.GreaterThanOrEqual)
            {
                return cmp > 0 || cmp == 0;
            }


            return false;
        }
    }

    public class WorldParamBool : WorldParam<bool>
    {
        public override WorldParam Clone()
        {
            return new WorldParamBool() { key = this.key, value = this.value };
        }

        public override float Difference(WorldParam param)
        {
            float x = value == true ? 1 : 0;
            float y = (param as WorldParamBool).value == true ? 1 : 0;
            return x - y;
        }
    }

    public class WorldParamInt : WorldParam<int>, IAdd, ISubtract
    {
        public override WorldParam Clone()
        {
            return new WorldParamInt() { key = this.key, value = this.value };
        }
        public WorldParam Add(WorldParam param)
        {
            WorldParamInt res = new WorldParamInt();
            res.value = value + (int)param.ValueObj;
            return res;
        }



        public WorldParam Subtract(WorldParam param)
        {
            WorldParamInt res = new WorldParamInt();
            res.value = value - (int)param.ValueObj;
            return res;
        }

        public override float Difference(WorldParam param)
        {
            float x = value;
            float y = (param as WorldParamInt).value;
            return x - y;
        }
    }
}

