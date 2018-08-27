using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView {
    /// <summary>
    /// gameView模板类
    /// </summary>
    [RequireComponent(typeof(GameViewModuleConnector))]
    public abstract class GameViewAbstractModule : MonoBehaviour
    {
        [Header("执行优先级")]
        public int prioritylevel = 1;

        [HideInInspector]
        public GameViewModuleConnector viewConnector;

        public virtual void OnOpenView() { }

        public virtual void OnCloseView() { }

        public virtual void OnInitView() { }

    }
}

