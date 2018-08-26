using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IcecreamView
{
    public abstract class GamePoolAbstract : MonoBehaviour , GamePoolInterface
    {
        public string pool_hash;

        public virtual void Pool_Disable()
        {

        }

        public virtual void Pool_Enable()
        {

        }

        public void Pool_Destory()
        {
            GamePoolManager.Instance.Destory(this);
        }

    }
}


