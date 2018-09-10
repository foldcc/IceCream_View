using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IcecreamView {
    /// <summary>
    /// 空白的实例模块
    /// </summary>
    public class GameViewNonModule : GameViewAbstractModule
    {

        public bool isDebugLog = false;

        public override void OnInitView()
        {
            base.OnInitView();
            if (!isDebugLog) return;
            Debug.Log("GameViewNonModule OnInit");
        }

        public override void OnCloseView()
        {
            base.OnCloseView();
            if (!isDebugLog) return;
            Debug.Log("GameViewNonModule OnClose");
        }

        public override void OnOpenView()
        {
            base.OnOpenView();
            if (!isDebugLog) return;
            Debug.Log("GameViewNonModule OnOpen");
        }
    }

}
