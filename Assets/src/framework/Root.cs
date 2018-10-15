using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.zonglv.minigame.manager;

namespace com.zonglv.minigame
{
    /// <summary>
    /// 整个程序的入口
    /// </summary>
    public class Root :MonoBehaviour
    {


        // Use this for initialization
        void Start()
        {

        }

        void Awake()
        {
            InitGame();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private void InitGame()
        {
            PoolManager.instance.SpriteInit();
            EventManager.Instance.InitEventCenter();
            EventManager.Instance.TriggerMiniGameEvent("MainView");
            
        }
    }
}
