using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView {
    /// <summary>
    /// gameView模板类
    /// </summary>
    [RequireComponent(typeof(GameViewModuleConnector))]
    public abstract class GameViewAbstractModule : MonoBehaviour, GameViewInterface
    {
        [Header("执行优先级")]
        public int prioritylevel = 1;

        /// <summary>
        /// 关闭当前view
        /// </summary>
        public void CloseView() {
            GetComponent<GameViewModuleConnector>().CloseView();
        }

        public virtual void OnOpenView() { }

        public virtual void OnCloseView() { }

        public virtual void OnInitView() { }

    }
}

