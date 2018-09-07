using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView
{
    /// <summary>
    /// 使用模板组合View模式的必要组件
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class GameViewModuleConnector : GameViewAbstract
    {
        private List<GameViewAbstractModule> gameViewAbstractModules = null;

        private RunType awaitType;

        private int awaitCount = 0;

        private bool isAwait = false;

        public override void OnOpenView()
        {
            awaitType = RunType.OnOpen;
            while (awaitCount < gameViewAbstractModules.Count)
            {
                gameViewAbstractModules[awaitCount].OnOpenView();
                awaitCount++;
                if (isAwait) {
                    return;
                }
            }
            awaitCount = 0;
            base.OnOpenView();
        }

        public override void OnCloseView()
        {
            awaitType = RunType.OnClose;
            while (awaitCount < gameViewAbstractModules.Count)
            {
                gameViewAbstractModules[awaitCount].OnCloseView();
                awaitCount++;
                if (isAwait)
                {
                    return;
                }
            }
            awaitCount = 0;
        }

        public override void OnInitView()
        {
            awaitType = RunType.OnInit;
            if (gameViewAbstractModules == null)
            {
                gameViewAbstractModules = new List<GameViewAbstractModule>();
                //获取当前对象上的所有View模块
                GetComponents(gameViewAbstractModules);
                //重新根据优先级排序
                gameViewAbstractModules.Sort((GameViewAbstractModule a, GameViewAbstractModule b) => {
                    if (a.prioritylevel < b.prioritylevel)
                    {
                        return -1;
                    }
                    else if (a.prioritylevel > b.prioritylevel)
                    {
                        return 1;
                    }
                    return 0;
                });
                //初始化所有模块
                gameViewAbstractModules.ForEach(m => {
                    m.viewConnector = this;
                });
            }
            while (awaitCount < gameViewAbstractModules.Count)
            {
                gameViewAbstractModules[awaitCount].OnInitView();
                awaitCount++;
                if (isAwait)
                {
                    return;
                }
            }
            awaitCount = 0;
        }

        /// <summary>
        /// 停止该页面正在执行的生命周期 ,停止的范围仅限(OnInit、OnOpen、OnClose)
        /// 不支持多线程操作
        /// </summary>
        /// <returns>用于重新恢复中断的生命周期执行器</returns>
        public Action Await() {
            isAwait = true;
            return Continue;
        }

        private void Continue() {
            if (!isAwait) return;
            isAwait = false;
            switch (awaitType) {
                case RunType.OnClose:
                    OnCloseView();
                    break;
                case RunType.OnInit:
                    OnCloseView();
                    break;
                case RunType.OnOpen:
                    OnOpenView();
                    break;
            }
        }

        private enum RunType{
            OnInit,
            OnOpen,
            OnClose
        }
    }
}

