using UnityEngine;

namespace IcecreamView
{
    /// <summary>
    /// 单例模板类
    /// </summary>
    /// <typeparam name="T">必须为继承MonoBehaviour对象</typeparam>
    public class SingletonTemplate<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static volatile T instance;
        private static object syncRoot = new Object();
        public static T Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                       instance = FindObjectOfType<T>();
                        if (instance == null)
                        {
                            GameObject go = new GameObject();
                            go.name = typeof(T).Name;
                            instance = go.AddComponent<T>();
                        }
                    }
                }
                return instance;
            }
        }
    }
}