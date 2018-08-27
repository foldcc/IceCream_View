using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView {

    public abstract class GameViewAbstract : MonoBehaviour , GameViewInterface
    {
        /// <summary>
        /// 对应View管理器
        /// </summary>
        private GameViewManager viewManager;

        public void SetViewManager(GameViewManager viewManager) {
            if (this.viewManager == null) {
                this.viewManager = viewManager;
            }
        }

        public virtual void OnInitView() { }

        public virtual void OnOpenView() { }

        public virtual void OnCloseView() { }

        public void CloseView()
        {
            OnCloseView();
            gameObject.SetActive(false);
        }

        public void OpenView(string ViewTable , bool isCloseThis = false) {
            if (viewManager != null) {
                if (isCloseThis) {
                    CloseView();
                }
                viewManager.OpenView(ViewTable);
            }
        }
    }
}


