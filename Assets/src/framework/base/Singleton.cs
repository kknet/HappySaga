using System;
using UnityEngine;

/// <summary>
///  小游戏的框架包
/// </summary>
namespace com.zonglv.minigame.framework
{
    /// <summary>
    /// 整个游戏的单例基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [RequireComponent(typeof(GameRoot))]
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        public static T GetInstance()
        {
            return _instance;
        }

        public void SetInstance(T t)
        {
            if (_instance == null)
            {
                _instance = t;
            }
        }

        public virtual void Init()
        {
            return;
        }

        public virtual void Release()
        {
            return;
        }
    }
}