using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView {
    [DisallowMultipleComponent,System.Serializable]
    public abstract class GameViewAbstract : MonoBehaviour , GameViewInterface
    {
        /// <summary>
        /// 对应View管理器
        /// </summary>
        [System.NonSerialized]
        private GameViewManager viewManager;

        public void SetViewManager(GameViewManager viewManager) {
            if (this.viewManager == null) {
                this.viewManager = viewManager;
            }
        }

        /// <summary>
        /// 页面被创建初始化时触发该方法
        /// </summary>
        public virtual void OnInitView() { }
        
        /// <summary>
        /// 页面打开时触发该方法
        /// </summary>
        public virtual void OnOpenView() { }
        
        /// <summary>
        /// 页面被关闭前触发该方法
        /// </summary>
        public virtual void OnCloseView() { }

        /// <summary>
        /// 关闭当前页面
        /// </summary>
        public void CloseView()
        {
            OnCloseView();
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 打开指定页面
        /// </summary>
        /// <param name="ViewTable">页面table</param>
        /// <param name="isCloseThis">是否同时关闭自己</param>
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


