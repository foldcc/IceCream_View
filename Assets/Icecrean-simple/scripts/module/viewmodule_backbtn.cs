using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IcecreamView;
using UnityEngine.UI;

public class viewmodule_backbtn : GameViewAbstractModule {

    public Button backBtn;

    public override void OnInitView()
    {
        //页面第一次被创建时
        if (backBtn != null) {
            backBtn.onClick.AddListener(()=> {
                viewConnector.CloseView();
            });
        }
    }

    public override void OnOpenView()
    {
        //页面被打开时
    }

    public override void OnCloseView()
    {
        //页面被关闭时
    }

    public override void OnDestoryView()
    {
        //页面被销毁时
    }
}
