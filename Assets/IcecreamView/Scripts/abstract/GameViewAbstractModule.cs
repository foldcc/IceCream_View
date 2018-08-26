using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView {
    /// <summary>
    /// gameView模板类
    /// </summary>
    [RequireComponent(typeof(GameViewModuleConnector)) , AddComponentMenu("IcecreamModule/")]
    public class GameViewAbstractModule : MonoBehaviour, GameViewInterface
    {
        [Header("执行优先级")]
        public int prioritylevel = 1;

        public virtual void View_Destory()
        {

        }

        public virtual void View_Disable()
        {

        }

        public virtual void View_Enable()
        {

        }

        public virtual void View_Init()
        {

        }
    }
}

