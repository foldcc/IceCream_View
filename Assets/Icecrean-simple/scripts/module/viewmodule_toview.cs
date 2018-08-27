using UnityEngine;
using UnityEngine.UI;
using IcecreamView;

public class viewmodule_toview : GameViewAbstractModule
{
    public Button toview;

    public string viewTable;

    public override void OnInitView()
    {
        if (toview != null) {
            toview.onClick.AddListener(()=> {
                viewConnector.OpenView(viewTable);
            });
        }
    }
}
