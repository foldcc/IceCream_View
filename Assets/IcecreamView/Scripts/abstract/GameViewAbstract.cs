using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView
{
    [DisallowMultipleComponent, System.Serializable]
    public abstract class GameViewAbstract : MonoBehaviour, GameViewInterface
    {
        /// <summary>
        /// ViewTable标识 不能修改
        /// </summary>
        [HideInInspector]
        public string VIEWTABLE;

        [HideInInspector]
        public bool isOnce = false;

        /// <summary>
        /// 对应View管理器
        /// </summary>
        [System.NonSerialized]
        private GameViewManager viewManager;

        public void SetViewManager(GameViewManager viewManager)
        {
            if (this.viewManager == null)
            {
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
            if (_closeHook())
            {
                _directClose();
            }
        }

        /// <summary>
        /// 用于截断页面关闭的钩子方法,默认返回true，如果返回false转为手动模式，需要自行close页面，适用于各种骚操作，请谨慎使用，可能会引起未知错误
        /// </summary>
        /// <returns></returns>
        protected virtual bool _closeHook()
        {
            return true;
        }

        /// <summary>
        /// 直接关闭页面，不会触发OnClose事件，通常情况下请使用CloseView方法，直接使用该方法可能导致不稳定出现位置问题
        /// </summary>
        protected void _directClose()
        {
            if (isOnce)
            {
                viewManager.clearViewAtHash(gameObject.GetHashCode());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }


        /// <summary>
        /// 打开指定页面
        /// </summary>
        /// <param name="ViewTable">页面table</param>
        /// <param name="isCloseThis">是否同时关闭自己</param>
        public GameViewAbstract OpenView(string ViewTable, bool isCloseThis = false, bool isSinge = false)
        {
            if (viewManager != null)
            {
                if (isCloseThis)
                {
                    CloseView();
                }
                return viewManager.OpenView(ViewTable, isSinge);
            }
            return null;
        }
    }
}


