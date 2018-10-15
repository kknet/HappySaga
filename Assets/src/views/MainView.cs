using UnityEngine.UI;
using UnityEngine;

using com.zonglv.minigame.framework.view;
using com.zonglv.minigame.manager;

namespace com.zonglv.minigame.views
{
    /// <summary>
    /// 主界面的功能控制类
    /// </summary>
    public class MainView : BaseView<MainView>
    {
        private Button Enter_Button;

        /// <summary>
        /// 继承自父类，用于做扩展的方法
        /// </summary>
        public override void StartView(){       }
        public override void ClosedView(){        }

        /// <summary>
        /// 启动主场景的方法
        /// </summary>
        public void InitMainFunction()
        {
            Debug.Log("main view 's test function");
            InitPerfab();

            Enter_Button = GameObject.Find("Button").GetComponent<Button>();
            Enter_Button.onClick.AddListener(EnterFunction);
            
        }

        private void EnterFunction()
        {
            Debug.Log("enter choose view");
            RemovePerfab();
            EventManager.Instance.TriggerMiniGameEvent("ChoosePageView");
        }


    }
}
