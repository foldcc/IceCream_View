using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView {
    public class GameViewNonView : GameViewAbstract
    {
        public bool isDebugLog = false;
        public override void OnInitView()
        {
            base.OnInitView();
            if (!isDebugLog) return;
            Debug.Log("GameViewNonView OnInit");
        }

        public override void OnCloseView()
        {
            base.OnCloseView();
            if (!isDebugLog) return;
            Debug.Log("GameViewNonView OnClose");
        }

        public override void OnOpenView()
        {
            base.OnOpenView();
            if (!isDebugLog) return;
            Debug.Log("GameViewNonView OnOpen");
        }
    }
}


