using UnityEngine;
using System.Collections;
using System.Threading;

/// <summary>
/// base空间的所有对象都为抽象类，并不用于实例化，只是用于其它类来继承
/// </summary>
namespace com.zonglv.minigame.bases
{
    /// <summary>
    /// 单例基类，提供两个抽象函数Init 和 DisInit 初始化和逆初始化过程。
    /// 用于Gameobject需要实例化的情况，所以继承了MonoBehaviour
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {

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
                        m_Instance.Init();
                    }
                }
                return m_Instance;
            }
        }

        private static bool IsSingle()
        {
            bool createdNew;
            S_name = "Singleton of " + typeof(T).ToString();
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
                    m_Instance.Init();
                }
            }
            else
            {
                Destroy(this);
            }
        }

        protected abstract void Init();
        protected abstract void DisInit();
        private void OnDestory()
        {
            if (m_Instance != null)
            {
                mutex.ReleaseMutex();
                DisInit();
                m_Instance = null;
            }
        }
        private void OnApplicationQuit()
        {
            //报错先关掉
            //mutex.ReleaseMutex();
        }
    }

    /// <summary>
    /// 用于非实例化，只需要代码执行的单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CSharpSingletion<T> where T : new()
    {

        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }

    }
    
    /// <summary>
    /// 需要实例化，而不需要控制线程的情况，即单线程使用的。不需要担心对象池的时候用此抽象类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingletionN<T> : MonoBehaviour where T : MonoBehaviour
    {

            private static string rootName = "MonoSingletionRoot";
            private static GameObject monoSingletionRoot;

            private static T instance;
            public static T Instance
            {
                get
                {
                    if (monoSingletionRoot == null)
                    {
                        monoSingletionRoot = GameObject.Find(rootName);
                        if (monoSingletionRoot == null) Debug.Log("please create a gameobject named " + rootName);
                    }
                    if (instance == null)
                    {
                        instance = monoSingletionRoot.GetComponent<T>();
                        if (instance == null) instance = monoSingletionRoot.AddComponent<T>();
                    }
                    return instance;
                }
            }

    }
}