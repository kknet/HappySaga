using UnityEngine.UI;
using UnityEngine;

using com.zonglv.minigame.framework.view;

namespace com.zonglv.minigame.views
{
    /// <summary>
    /// 胜利界面的代码控制
    /// </summary>
    public class VictoryView : BaseView<VictoryView>
    {
        /// <summary>
        /// 继承自父类，用于做扩展的方法
        /// </summary>
        public override void StartView() { }
        public override void ClosedView() { }

        private Button next_btn;

        /// <summary>
        /// 初始化胜利界面
        /// </summary>
        public void InitVictoryFunction()
        {
            InitPerfab();
            next_btn = GameObject.Find("Next_btn").GetComponent<Button>();
            next_btn.onClick.AddListener(NextPassFunction);
        }

        private void NextPassFunction()
        {
            RemovePerfab();
            Time.timeScale = 1;
            DrawingGame.instance.NextPassFunction();
        }
    }
}
