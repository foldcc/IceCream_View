using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyGameTemplate {

    public abstract class GameViewAbstract : MonoBehaviour , GameViewInterface
    {

        public virtual void View_Init() {

        }

        public virtual void View_Disable()
        {

        }

        public virtual void View_Enable()
        {

        }

        public virtual void View_Destory()
        {
            View_Disable();
            gameObject.SetActive(false);
        }
    }

}


