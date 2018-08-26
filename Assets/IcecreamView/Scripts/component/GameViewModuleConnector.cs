using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView
{
    /// <summary>
    /// 使用模板组合View模式的必要组件
    /// </summary>
    public sealed class GameViewModuleConnector : GameViewAbstract
    {
        private List<GameViewAbstractModule> gameViewAbstractModules = new List<GameViewAbstractModule>();

        public override void View_Destory()
        {
            foreach (var item in gameViewAbstractModules)
            {
                item.View_Destory();
            }
            base.View_Destory();
        }

        public override void View_Disable()
        {
            foreach (var item in gameViewAbstractModules)
            {
                item.View_Disable();
            }
            base.View_Disable();
        }

        public override void View_Enable()
        {
            foreach (var item in gameViewAbstractModules)
            {
                item.View_Enable();
            }
            base.View_Enable();
        }

        public override void View_Init()
        {
            GetComponents(gameViewAbstractModules);
            if (gameViewAbstractModules == null)
            {
                gameViewAbstractModules = new List<GameViewAbstractModule>();
            }

            foreach (var item in gameViewAbstractModules)
            {
                item.View_Init();
            }

            base.View_Init();
        }
    }
}

