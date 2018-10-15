using UnityEngine.UI;
using UnityEngine;
using System.Collections;

using com.zonglv.minigame.framework.view;
using com.zonglv.minigame.manager;

/// <summary>
/// 选择关卡的视图功能控制类
/// </summary>
namespace com.zonglv.minigame.views
{
    /// <summary>
    /// 选择关卡的界面
    /// </summary>
    public class ChooseChapter : BaseView<ChooseChapter>
    {
        private Button DrawGame_btn;



        private string photopath;
        private GameObject userspic_obj;
        private Image userspic;

        public override void StartView()        {        }

        public override void ClosedView()       {        }



        public void ChooseFunction()
        {
            InitPerfab();

            DrawGame_btn = GameObject.Find("chapter_btn0").GetComponent<Button>();
            DrawGame_btn.onClick.AddListener(DrawFunction);

            photopath = Application.persistentDataPath + "/onMobileSavedScreen1.png";


            userspic_obj = GameObject.Find("chapter_btn0").transform.Find("userpic0").gameObject;
            userspic = userspic_obj.GetComponent<Image>();
            userspic_obj.SetActive(true);

            StartCoroutine("LoadPicFromFile");

        }

        /// <summary>
        /// 进入画图场景
        /// </summary>
        private void DrawFunction()
        {
            RemovePerfab();
            //GameSceneManager.instance.InitDrawFunction();
            EventManager.Instance.TriggerMiniGameEvent("EnterGameDraw");
        }

        /// <summary>
        /// 进入射击游戏场景
        /// </summary>
        private void BowFunction()
        {
            RemovePerfab();
            GameSceneManager.instance.InitBowFunction();            
        }

        /// <summary>
        /// 裁入截图所在文件夹的图片
        /// </summary>
        /// <returns></returns>
        IEnumerator LoadPicFromFile()
        {
            WWW picpath = new WWW("file://" + photopath);
            yield return picpath;

            if (string.IsNullOrEmpty(picpath.error))
            {
                Texture2D tex = picpath.texture;
                Sprite temp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
                userspic.sprite = temp;
            }

        }
    }
}
