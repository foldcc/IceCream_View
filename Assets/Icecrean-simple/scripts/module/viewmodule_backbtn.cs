using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IcecreamView;
using UnityEngine.UI;

public class viewmodule_backbtn : GameViewAbstractModule {

    public Button backBtn;

    public override void OnInitView()
    {
        if (backBtn != null) {
            backBtn.onClick.AddListener(()=> {
                viewConnector.CloseView();
            });
        }
    }

}
