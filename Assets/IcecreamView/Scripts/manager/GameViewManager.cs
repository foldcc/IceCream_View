using System;
using System.Collections.Generic;
using UnityEngine;

[assembly: System.Reflection.AssemblyVersion("1.0.*")]
namespace IcecreamView
{
    /// <summary>
    /// icecream页面管理器，是驱动所有页面的核心控制器，用于控制页面生成、展示、隐藏、跳转等操作
    /// </summary>
    public sealed class GameViewManager
    {

        public static GameViewManager InstantiateViewManager(GameViewConfig gameViewConfig, Transform parent)
        {
            return new GameViewManager(gameViewConfig, parent);
        }

        private GameViewConfig Config;

        private Dictionary<string, GameViewInfo> ConfigViewDictionary;

        private List<GameViewAbstract> ViewDictionary;

        public Transform UIparent;

        public GameViewManager(GameViewConfig gameViewConfig, Transform parent)
        {
            Init(gameViewConfig, parent);
        }
        
        /// <summary>
        /// 获取指定页面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public T GetView<T>(string table) where T : GameViewAbstract
        {
            if (ContainsKeyView(table))
            {
                foreach (var item in ViewDictionary)
                {
                    if (table.Equals(item.VIEWTABLE))
                    {
                        return (T)item;
                    }
                }
            }
            return default(T);
        }

        public T OpenView<T>(string table, bool isSinge = false) where T : GameViewAbstract
        {
            GameViewAbstract view = OpenView(table, isSinge);
            if (view != null)
            {
                return view as T;
            }
            return null;
        }

        public T OpenView<T>(bool isSinge = false) where T : GameViewAbstract
        {
            string cname = typeof(T).ToString();
            foreach (GameViewAbstract key in ViewDictionary)
            {
                if (key.GetType().Name.Equals(cname))
                {
                    return OpenView<T>(key.VIEWTABLE, isSinge);
                }
            }
            Debug.LogError("GameViewManager : 打开view失败，未找到指定T --- " + cname);
            return null;
        }

        /// <summary>
        /// 构建一个View对象
        /// </summary>
        /// <param name="Table">ViewTable</param>
        /// <returns></returns>
        private GameViewAbstract CreateView(string Table)
        {
            GameViewAbstract gameViewAbstract = GameObject.Instantiate<GameViewAbstract>(ConfigViewDictionary[Table].View, UIparent);
            gameViewAbstract.VIEWTABLE = Table;
            if (ConfigViewDictionary[Table].isOnce)
            {
                gameViewAbstract.isOnce = ConfigViewDictionary[Table].isOnce;
            }
            else
            {
                gameViewAbstract.isOnce = ContainsKeyView(Table);
            }
            gameViewAbstract.SetViewManager(this);
            gameViewAbstract.isOpen = false;
            gameViewAbstract.OnInitView();
            return gameViewAbstract;
        }
        
        /// <summary>
        /// 打开指定页面
        /// </summary>
        /// <param name="table"></param>
        /// <param name="isSinge"></param>
        /// <returns></returns>
        public GameViewAbstract OpenView(string table, bool isSinge = false)
        {
            int viewCount = getViewIndex(table, isSinge);
            if (viewCount != -1)
            {
                ViewDictionary[viewCount].isOpen = true;
                ViewDictionary[viewCount].gameObject.SetActive(true);
                ViewDictionary[viewCount].transform.SetAsLastSibling();
                ViewDictionary[viewCount].OnOpenView();
                return ViewDictionary[viewCount];
            }
            else if (ConfigViewDictionary.ContainsKey(table))
            {
                GameViewAbstract gameViewAbstract = CreateView(table);
                gameViewAbstract.isOpen = true;
                gameViewAbstract.gameObject.SetActive(true);
                gameViewAbstract.transform.SetAsLastSibling();
                gameViewAbstract.OnOpenView();
                ViewDictionary.Add(gameViewAbstract);
                return gameViewAbstract;
            }
            else
            {

                Debug.LogFormat("<color=yellow>{0}</color>" , "GameViewManager : 打开view失败，未找到指定table --- " + table);
                return null;
            }
        }

        /// <summary>
        /// 关闭所有页面 并打开指定页面
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public GameViewAbstract OpenViewAndCloseOther(string table , bool isSinge)
        {
            if (table == null)
            {
                return null;
            }

            foreach (var item in ViewDictionary)
            {
                item.CloseView();
            }

            GameViewAbstract view = OpenView(table , isSinge);
            return view;
        }

        /// <summary>
        /// 打开指定的ViewModuleConnector页面
        /// </summary>
        /// <param name="table"></param>
        /// <param name="isSinge"></param>
        /// <returns></returns>
        public GameViewModuleConnector OpenConnectorView(string table, bool isSinge = false)
        {
            return OpenView<GameViewModuleConnector>(table, isSinge);
        }

        /// <summary>
        /// 判断指定table是否存在
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool ContainsKeyView(string table)
        {
            foreach (GameViewAbstract G in ViewDictionary)
            {
                if (table.Equals(G.VIEWTABLE))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 返回一个指定table的View对应下标
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public int getViewIndex(string table, bool isSinge = false, bool objectType = false)
        {
            for (int i = 0; i < ViewDictionary.Count; i++)
            {

                if (table.Equals(ViewDictionary[i].VIEWTABLE))
                {
                    if (isSinge)
                    {
                        return i;
                    }
                    else if (ViewDictionary[i].gameObject.activeSelf == objectType)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private void Init(GameViewConfig gameViewConfig, Transform parent)
        {

            Config = gameViewConfig;

            UIparent = parent;

            ConfigViewDictionary = new Dictionary<string, GameViewInfo>();

            ViewDictionary = new List<GameViewAbstract>();

            if (Config == null)
            {
                return;
            }

            ConfigViewDictionary = Config.GetViewWithDic();

            foreach (string item in ConfigViewDictionary.Keys)
            {
                if (ConfigViewDictionary[item].isCache)
                {
                    GameViewAbstract game = CreateView(item);
                    game.gameObject.SetActive(false);
                    ViewDictionary.Add(game);
                }
            }
            if (Config.DefaultViewTable != null && Config.DefaultViewTable != "")
            {
                OpenView(Config.DefaultViewTable);
            }
        }

        /// <summary>
        /// 关闭指定Table的页面
        /// </summary>
        /// <param name="table"></param>
        public void CloseView(string table)
        {
            if (ContainsKeyView(table))
            {
                var count = getViewIndex(table, false, true);
                if (count == -1) return;
                ViewDictionary[count].CloseView();
            }
        }

        /// <summary>
        /// 关闭指定Hash页面
        /// </summary>
        /// <param name="gameHash"></param>
        public void CloseViewForHash(int gameHash)
        {
            foreach (var item in ViewDictionary)
            {
                if (item.gameObject.GetHashCode() == gameHash)
                {
                    item.CloseView();
                }
            }
        }

        /// <summary>
        /// 关闭指定类型的所有页面
        /// </summary>
        /// <param name="table"></param>
        public void CloseTableViews(string table)
        {
            if (table == null) return;
            foreach (var item in ViewDictionary)
            {
                if (table.Equals(item.VIEWTABLE))
                {
                    item.CloseView();
                }
            }
        }

        /// <summary>
        /// 关闭所有页面
        /// </summary>
        public void CloseAllView()
        {
            foreach (var item in ViewDictionary)
            {
                item.CloseView();
            }
        }

        /// <summary>
        /// 销毁指定的view
        /// </summary>
        /// <param name="hash"></param>
        public void clearViewAtHash(int hash)
        {
            foreach (var item in ViewDictionary)
            {
                if (item.gameObject.GetHashCode() == hash)
                {
                    GameObject.Destroy(item.gameObject);
                    ViewDictionary.Remove(item);
                    return;
                }
            }
        }

        [Obsolete("不在提供运行时修改View配置表", true)]
        public void SetConfig(GameViewConfig config)
        {
            Config = config;
        }
    }
}
