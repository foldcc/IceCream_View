using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView {

    public abstract class GameViewAbstract : MonoBehaviour , GameViewInterface
    {

        public virtual void OnInitView() { }

        public virtual void OnOpenView() { }

        public virtual void OnCloseView() { }

        public void CloseView()
        {
            OnOpenView();
            gameObject.SetActive(false);
        }
    }

}


