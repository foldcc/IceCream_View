using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IcecreamView;

public class ViewController : MonoBehaviour {

    public GameViewConfig viewConfig;

    public Transform Canvasteansform;
	
    private GameViewManager viewManager;


    private void Start()
    {
        viewManager = new GameViewManager();
        viewManager.Init(viewConfig , Canvasteansform);
    }
}
