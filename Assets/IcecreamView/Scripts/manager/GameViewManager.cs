using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView{

    public class GameViewManager : MonoBehaviour
    {
        private GameViewConfig Config;

        private Dictionary<string, GameViewInfo> ConfigViewDictionary;

        private Dictionary<string, GameViewAbstract> ViewDictionary;

        public Transform UIparent;

        public void SetConfig(GameViewConfig config) {
            Config = config;
        }

        public void Init(GameViewConfig gameViewConfig, Transform parent)
        {
            Config = gameViewConfig;

            UIparent = parent;

            ConfigViewDictionary = new Dictionary<string, GameViewInfo>();

            ViewDictionary = new Dictionary<string, GameViewAbstract>();

            if (Config == null) {
                return;
            }

            ConfigViewDictionary = Config.GetViewWithDic();

            foreach (string item in ConfigViewDictionary.Keys)
            {
                ViewDictionary.Add(item, Instantiate<GameViewAbstract>(ConfigViewDictionary[item].View , UIparent));
                ViewDictionary[item].SetViewManager(this);
                ViewDictionary[item].OnInitView();
                ViewDictionary[item].gameObject.SetActive(false);
            }
            if (Config.DefaultViewTable != null) {
                OpenView(Config.DefaultViewTable);
            }
        }

        public bool isTable(string table) {
            return ViewDictionary.ContainsKey(table);
        }

        public T OpenView<T>(string table) where T : GameViewAbstract {
            GameViewAbstract view = OpenView(table);
            if (view != null) {
                return view as T;
            }
            return null;
        }

        public T OpenView<T>() where T : GameViewAbstract
        {
            string cname = typeof(T).ToString();
            foreach (string key in ViewDictionary.Keys) {
                if (ViewDictionary[key].GetType().Name.Equals(cname)) {
                    return OpenView<T>(key);
                }
            }
            Debug.LogError("GameViewManager : 打开view失败，未找到指定T --- " + cname);
            return null;
        }
       
        public GameViewAbstract OpenView(string table)
        {
            if (ViewDictionary.ContainsKey(table))
            {
                ViewDictionary[table].gameObject.SetActive(true);
                ViewDictionary[table].OnCloseView();
                return ViewDictionary[table];
            }
            else {
                Debug.LogError("GameViewManager : 打开view失败，未找到指定table --- " + table);
                return null;
            }
        }

        public GameViewAbstract OpenViewAndCloseOther(string table)
        {
            if (table == null)
            {
                return null;
            }
            GameViewAbstract view = null;
            foreach (var item in ViewDictionary.Keys)
            {
                if (item.Equals(table))
                {
                    if (!ViewDictionary[table].gameObject.activeSelf) {
                        ViewDictionary[table].OnCloseView();
                    }
                    ViewDictionary[table].gameObject.SetActive(true);
                    view = ViewDictionary[table];
                }
                else
                {
                    if (ViewDictionary[item].gameObject.activeSelf) {
                        ViewDictionary[item].CloseView();
                    }
                }
            }
            return view;
        }

        /// <summary>
        /// 关闭指定Table的页面
        /// </summary>
        /// <param name="table"></param>
        public void CloseView(string table) {
            if (ViewDictionary.ContainsKey(table))
            {
                ViewDictionary[table].CloseView();
            }
        }
        /// <summary>
        /// 关闭所有页面
        /// </summary>
        public void CloseAllView() {
            foreach (var item in ViewDictionary.Keys)
            {
                ViewDictionary[item].CloseView();
            }
        }

        /// <summary>
        /// 获取指定页面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public T GetView<T>(string table)  where T : GameViewAbstract
        {
            if (ViewDictionary.ContainsKey(table))
            {
                return (T)ViewDictionary[table];
            }
            else {
                return default(T);
            }
        }
    }
}
