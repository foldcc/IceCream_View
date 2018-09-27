using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView {

    [System.Serializable]
    public struct GameViewInfo {
        public string Table;
        public string TableInfo;
        // 是否提前缓存(用于大型View)
        public bool isCache;
        public GameViewAbstract View;
    }

    /// <summary>
    /// 游戏View配置表
    /// </summary>
    public class GameViewConfig : ScriptableObject
    {

        public string ConfigName;

        public string DefaultViewTable;

        public List<GameViewInfo> GameViewList;

        /// <summary>
        /// 获取View Map
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, GameViewInfo> GetViewWithDic()
        {

            if (GameViewList != null && GameViewList.Count > 0)
            {

                Dictionary<string, GameViewInfo> viewDic = new Dictionary<string, GameViewInfo>();

                GameViewList.ForEach(view =>
                {
                    if(view.Table == null || view.Table == ""){
                        view.Table = view.View.name;
                    }
                    if (!viewDic.ContainsKey(view.Table))
                    {
                        viewDic.Add(view.Table, view);
                    }

                });

                return viewDic;
            }

            return null;
        }
    }
}