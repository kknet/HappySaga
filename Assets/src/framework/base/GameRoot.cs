using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.zonglv.minigame.framework
{
    public class GameRoot : MonoBehaviour
    {
        private static GameObject _rootObj;

        private static List<Action> _singletonReleaseList = new List<Action>();

        public void Awake()
        {
            _rootObj = gameObject;
            GameObject.DontDestroyOnLoad(_rootObj);

            StartCoroutine(InitSingletons());
        }

        /// <summary>
        /// 在这里进行所有单例的销毁
        /// </summary>
        public void OnApplicationQuit()
        {
            for (int i = _singletonReleaseList.Count - 1; i >= 0; i--)
            {
                _singletonReleaseList[i]();
            }
        }

        /// <summary>
        /// 在这里进行所有单例的初始化
        /// </summary>
        /// <returns></returns>
        private IEnumerator InitSingletons()
        {
            yield return null;
            // Init Singletons
        }

        private static void AddSingleton<T>() where T : Singleton<T>
        {
            if (_rootObj.GetComponent<T>() == null)
            {
                T t = _rootObj.AddComponent<T>();
                t.SetInstance(t);
                t.Init();

                _singletonReleaseList.Add(delegate ()
                {
                    t.Release();
                });
            }
        }

        public static T GetSingleton<T>() where T : Singleton<T>
        {
            T t = _rootObj.GetComponent<T>();

            if (t == null)
            {
                AddSingleton<T>();
            }

            return t;
        }
    }
}
