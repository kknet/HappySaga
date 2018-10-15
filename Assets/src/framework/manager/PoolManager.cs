using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.zonglv.minigame.bases;

/// <summary>
/// 游戏中的管理类，需要被实例化，且要单例化
/// </summary>
namespace com.zonglv.minigame.manager
{
    /// <summary>
    /// 用于管理对象的生命周期的对象池
    /// </summary>
    public class PoolManager : MonoSingleton<PoolManager>
    {

        protected override void Init()
        {
            //throw new System.NotImplementedException();
        }

        protected override void DisInit()
        {
            //throw new System.NotImplementedException();
        }

        protected PoolManager()
        {
            
        }

        /// <summary>
        /// 用于对象栈的初始化
        /// </summary>
        public void SpriteInit()
        {
            Debug.Log("init the sprite pool");
        }
    }
}
