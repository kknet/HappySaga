using UnityEngine;
using System.Threading;

using com.zonglv.minigame.bases;

namespace com.zonglv.minigame.framework.view
{
    /// <summary>
    /// 所有视图操作的基类
    /// </summary>
    public abstract class BaseView<T> : MonoBehaviour where T : BaseView<T>
    {
        /// <summary>
        /// 基本视图所必需的所有变量
        /// </summary>
        private static T m_Instance = null;
        private static string S_name;
        private static Mutex mutex;
        public static T instance
        {
            get
            {
                if (m_Instance == null)
                {
                    if (IsSingle())
                    {
                        m_Instance = new GameObject(S_name, typeof(T)).GetComponent<T>();
                        m_Instance.StartView();
                    }
                }
                return m_Instance;
            }
        }

        private static bool IsSingle()
        {
            bool createdNew;
            string str=typeof(T).ToString();
            S_name = str.Substring(26);
            mutex = new Mutex(false, S_name, out createdNew);
            return createdNew;
        }

        private void Awake()
        {
            if (m_Instance == null)
            {
                if (IsSingle())
                {
                    m_Instance = this as T;
                    m_Instance.StartView();
                }
            }
            else
            {
                Destroy(this);
            }
        }

        public abstract void StartView();
        public abstract void ClosedView();

        /// <summary>
        /// 打开视图,此方法为虚方法，可以被子视图重写
        /// </summary>
        GameObject view_ui;
        ResourceRequest obj2;
        public virtual  void InitPerfab()
        {
            string viewspath = "perfabs/views/"+S_name;
            ///采用同步加载方法
            //Object obj= Resources.Load(viewspath ,typeof(GameObject));

            ///采用异步加载的方法加载
            obj2 = Resources.LoadAsync(viewspath, typeof(GameObject));
            view_ui = Instantiate(obj2.asset) as GameObject;

            view_ui.name = (this.name) + "_ui";
            view_ui.gameObject.transform.SetParent( GameObject.Find(S_name).transform);
        }
        /// <summary>
        /// 关闭视图
        /// </summary>
        public virtual void RemovePerfab()
        {
            Destroy(view_ui);
            obj2 = null;
            Resources.UnloadUnusedAssets();
        }

        /// <summary>
        /// 销毁View视图的方法
        /// </summary>
        private void OnDestory()
        {
            if (m_Instance != null)
            {
                mutex.ReleaseMutex();
                ClosedView();
                m_Instance = null;
            }
        }
        private void OnApplicationQuit()
        {
            //报错先关掉
            //mutex.ReleaseMutex();
        }
    }
}
