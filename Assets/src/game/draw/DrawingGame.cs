using UnityEngine;
using UnityEngine.UI;


using com.zonglv.minigame.framework.view;
using com.zonglv.minigame.manager;

namespace com.zonglv.minigame.views
{
    /// <summary>
    /// 关于画图的所有场景的判断各方面都在这里面进行,由于策划更改需求，重写了一些方法如截图等等
    /// 作者：田国松
    /// 创建时间：2018.9.28
    /// 修改时间：2018.10.12
    /// </summary>
    public class DrawingGame : BaseView<DrawingGame>
    {
        private GameObject hedgehog;

        /// <summary>
        /// 继承自父类，用于做扩展的方法
        /// </summary>
        public override void StartView() { }
        public override void ClosedView() { }

        /// <summary>
        /// 用于进行实时监测，看是否玩家有划线操作而进行游戏
        /// </summary>
        private void FixedUpdate()
        {
            BeginDraw();
        }

        private Button return_btn;
        private Button restart_btn;

        /// <summary>
        /// 用于控制整个绘画的流程
        /// </summary>
        public void InitDrawingFunction()
        {
            InitPerfab();

            return_btn = GameObject.Find("return_btn").GetComponent<Button>();
            return_btn.onClick.AddListener(ReturnChooseFunction);

            restart_btn = GameObject.Find("Restart_btn").GetComponent<Button>();
            restart_btn.onClick.AddListener(RestartGameFunction);
        }

        /// <summary>
        /// 开始绘画了要进行的控制
        /// </summary>
        private void BeginDraw()
        {
            if(GameObject.Find("Drawing0")!=null)
            {
                if (GameObject.Find("hedgehog") != null)
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        hedgehog = GameObject.Find("hedgehog");
                        hedgehog.AddComponent<Rigidbody2D>();
                    }
                }
            }
        }

        /// <summary>
        /// 返回选关界面
        /// </summary>
        private void ReturnChooseFunction()
        {
            RemovePerfab();
            DestroyEverying();
            GameSceneManager.instance.DisInitDrawFunction();
            EventManager.Instance.TriggerMiniGameEvent("ChooseChapter");
        }

        /// <summary>
        /// 重新开始当前关游戏
        /// </summary>
        private void RestartGameFunction()
        {
            DestroyEverying();
            RemovePerfab();
            GameSceneManager.instance.DisInitDrawFunction();
            EventManager.Instance.TriggerMiniGameEvent("EnterGameDraw");
        }

        public void NextPassFunction()
        {
            RemovePerfab();
            DestroyEverying();
            GameSceneManager.instance.DisInitDrawFunction();
            EventManager.Instance.TriggerMiniGameEvent("ChooseChapter");
        }

        /// <summary>
        /// 从场景中删除所有当前场景的物件
        /// </summary>
        private void DestroyEverying()
        {
            GameObject[]  line=GameObject.FindGameObjectsWithTag("Line");
            for(int i=0;i<line.Length;i++)
            {
                Destroy(line[i]);
            }
        }
    }
}
