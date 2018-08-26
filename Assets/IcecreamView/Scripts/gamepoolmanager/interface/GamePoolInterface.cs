using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyGameTemplate {
    public interface GamePoolInterface
    {
        /// <summary>
        /// 对象禁用
        /// </summary>
        void Pool_Disable();

        /// <summary>
        /// 对象激活
        /// </summary>
        void Pool_Enable();

        /// <summary>
        /// 对象销毁调用
        /// </summary>
        void Pool_Destory();

    }
}

