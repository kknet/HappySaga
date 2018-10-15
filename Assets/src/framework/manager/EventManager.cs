using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.zonglv.minigame.bases;
//using com.zonglv.minigame.game.draw;
using com.zonglv.minigame.views;

namespace com.zonglv.minigame.manager
{
    /// <summary>
    /// 用于做游戏中的事件注册，由于是小游戏所以通过事件观察者来解藕
    /// </summary>
    public class EventManager : CSharpSingletion<EventManager>
    {
        //public delegate void OnPlayerInfoChangedEvent(string infoType);
        //public event OnPlayerInfoChangedEvent OnPlayerInfoChanged;

        /// <summary>
        /// 画线游戏进入事件
        /// </summary>
        public delegate void DrawGameSceneInitEvent();
        public static event DrawGameSceneInitEvent DrawGameSceneInit;

        /// <summary>
        /// 进入主界面的事件
        /// </summary>
        public delegate void MainViewInitEvent();
        public static event MainViewInitEvent MainViewInit;

        /// <summary>
        /// 调用弓箭射击游戏事件
        /// </summary>
        public delegate void BowViewInitEvent();
        public static event BowViewInitEvent BowViewInit;

        /// <summary>
        /// 进入选关界面
        /// </summary>
        public delegate void ChooseChapterInitEvent();
        public static event ChooseChapterInitEvent ChooseChapterInit;

        /// <summary>
        /// 战斗胜利的界面
        /// </summary>
        public delegate void VictoryInitEvent();
        public static event VictoryInitEvent VictoryInit;

        public delegate void PageInitEvent();
        public static event PageInitEvent PageInit;

        public void InitEventCenter()
        {
            Debug.Log("EventCenter is init");
            //Subject.ObserverTestEvent += ChangeButton;

            ///Game的绑定
            DrawGameSceneInit += DrawingGame.instance.InitDrawingFunction;
            DrawGameSceneInit += GameSceneManager.instance.InitDrawFunction;
            BowViewInit += GameSceneManager.instance.InitBowFunction;

            ///View的绑定
            MainViewInit += MainView.instance.InitMainFunction;
            ChooseChapterInit += ChooseChapter.instance.ChooseFunction;
            VictoryInit += VictoryView.instance.InitVictoryFunction;
            PageInit += ChoosePageView.instance.InitPageFunction;
        }

        public void StartMiniGameEvent()
        {

        }

        public void StopMiniGameEvent()
        {
            DrawGameSceneInit -= DrawingGame.instance.InitDrawingFunction;
            DrawGameSceneInit -= GameSceneManager.instance.InitDrawFunction;
            BowViewInit -= GameSceneManager.instance.InitBowFunction;
            MainViewInit -= MainView.instance.InitMainFunction;
            ChooseChapterInit -= ChooseChapter.instance.ChooseFunction;
            VictoryInit -= VictoryView.instance.InitVictoryFunction;
            PageInit -= ChoosePageView.instance.InitPageFunction;

        }

        public void TriggerMiniGameEvent(string _Eventname)
        {
            switch(_Eventname)
            {
                case "MainView":
                    {
                        if (MainViewInit != null)
                        {
                            MainViewInit();
                        }
                        break;
                    }
                case "ChooseChapter":
                    {
                        if (ChooseChapterInit != null)
                        {
                            ChooseChapterInit();
                        }
                        break;
                    }
                case "EnterGameDraw":
                    {
                        if (DrawGameSceneInit != null)
                        {
                            DrawGameSceneInit();
                        }
                        break;
                    }
                case "Victory":
                    {
                        if(VictoryInit!=null)
                        {
                            VictoryInit();
                        }
                        break;
                    }
                case "ChoosePageView":
                    {
                        if(PageInit!=null)
                        {
                            PageInit();
                        }
                        break;
                    }
                default:
                    break;
            }
        }


    }    
}
