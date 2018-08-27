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

        public override void OnOpenView()
        {
            foreach (var item in gameViewAbstractModules)
            {
                item.OnOpenView();
            }
            base.OnOpenView();
        }

        public override void OnCloseView()
        {
            foreach (var item in gameViewAbstractModules)
            {
                item.OnCloseView();
            }
        }

        public override void OnInitView()
        {
            GetComponents(gameViewAbstractModules);
            gameViewAbstractModules.Sort((GameViewAbstractModule a , GameViewAbstractModule b) => {
                if (a.prioritylevel < b.prioritylevel) {
                    return -1;
                } else if(a.prioritylevel > b.prioritylevel)
                {
                    return 1;
                }
                return 0;
            });

            gameViewAbstractModules.ForEach(m => {
                m.viewConnector = this;
            });

            if (gameViewAbstractModules == null)
            {
                gameViewAbstractModules = new List<GameViewAbstractModule>(); 
            }

            foreach (var item in gameViewAbstractModules)
            {
                item.OnInitView();
            }
        }
    }
}

