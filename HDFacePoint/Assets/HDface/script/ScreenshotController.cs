using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotController : MonoBehaviour {

    public Button Screenshotbt;
    public Button Screenrecordbt;

    

	// Use this for initialization
	void Start () {

        Screenshotbt.onClick.AddListener(ScreenshotOnClick);
        Screenrecordbt.onClick.AddListener(ScreenrecordOnClick);

        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ScreenshotOnClick()
    {
        
    }

    void ScreenrecordOnClick()
    {

    }

}
