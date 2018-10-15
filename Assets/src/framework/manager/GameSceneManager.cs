using UnityEngine.SceneManagement;
using UnityEngine;

using com.zonglv.minigame.bases;
using com.zonglv.minigame.framework.view;

namespace com.zonglv.minigame.manager
{
    /// <summary>
    /// 处理游戏中的场景的切换功能
    /// </summary>
    public class GameSceneManager : MonoSingleton<GameSceneManager>
    {
        /// <summary>
        /// 打开画画的游戏场景
        /// </summary>
        public  void InitDrawFunction()
        {

            SceneManager.LoadSceneAsync("DrawingScene",LoadSceneMode.Additive);
        }

        /// <summary>
        /// 进行发射游戏场景的调用
        /// </summary>
        public void InitBowFunction()
        {
            SceneManager.LoadSceneAsync("BowScene", LoadSceneMode.Additive);
        }

        public void DisInitDrawFunction()
        {
            SceneManager.UnloadSceneAsync("DrawingScene");
        }

        public void DisInitBowFunction()
        {
            SceneManager.UnloadSceneAsync("BowScene");
        }

        protected override void DisInit()
        {
            //throw new System.NotImplementedException();
        }

        protected override void Init()
        {
            //throw new System.NotImplementedException();
        }
    }
}
