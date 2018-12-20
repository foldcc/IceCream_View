using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView {

    [System.Serializable]
    public struct GameViewInfo {
        public GameViewAbstract View;

        public string Table;
        public string TableInfo;
        // 是否提前缓存(用于大型View)
        public bool isCache;
        // 是否作为一次性使用，当页面被关闭时直接销毁
        public bool isOnce;
    }

    /// <summary>
    /// 游戏View配置表
    /// </summary>
    [CreateAssetMenu(fileName = "GameViewConfig" , menuName = "Config/IceViewConfig")]
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
            Dictionary<string, GameViewInfo> viewDic = new Dictionary<string, GameViewInfo>();
            if (GameViewList != null && GameViewList.Count > 0)
            {
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
            }
            return viewDic;
        }
    }
}