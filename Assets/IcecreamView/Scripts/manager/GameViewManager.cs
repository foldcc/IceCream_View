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
                //Debug.Log(ViewDictionary[item].name + " class: " + ViewDictionary[item].GetType().Name);
                ViewDictionary[item].View_Init();
                ViewDictionary[item].gameObject.SetActive(false);
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
                ViewDictionary[table].View_Enable();
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
                        ViewDictionary[table].View_Enable();
                    }
                    ViewDictionary[table].gameObject.SetActive(true);
                    view = ViewDictionary[table];
                }
                else
                {
                    if (ViewDictionary[item].gameObject.activeSelf) {
                        ViewDictionary[item].View_Destory();
                    }
                    
                }
            }
            return view;
        }

        public void CloseView(string table) {
            if (ViewDictionary.ContainsKey(table))
            {
                ViewDictionary[table].View_Destory();
            }
        }

        public void CloseAllView() {
            foreach (var item in ViewDictionary.Keys)
            {
                ViewDictionary[item].View_Destory();
            }
        }

        

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
