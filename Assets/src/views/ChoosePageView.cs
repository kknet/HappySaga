using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using com.zonglv.minigame.framework.view;
using com.zonglv.minigame.manager;

namespace com.zonglv.minigame.views
{
    /// <summary>
    /// 章节选择视图
    /// </summary>
    public class ChoosePageView : BaseView<ChoosePageView>
    {
        private GameObject Into_chaper0;

        public override void ClosedView(){  }

        public override void StartView(){        }

        /// <summary>
        /// 初始化章节选择界面
        /// </summary>
        public void InitPageFunction()
        {
            InitPerfab();
            Into_chaper0 = GameObject.Find("Button_Page1");
            Into_chaper0.GetComponent<Button>().onClick.AddListener(GotoChapterFunction);
        }

        public void GotoChapterFunction()
        {
            RemovePerfab();
            EventManager.Instance.TriggerMiniGameEvent("ChooseChapter");
        }
    }
}
