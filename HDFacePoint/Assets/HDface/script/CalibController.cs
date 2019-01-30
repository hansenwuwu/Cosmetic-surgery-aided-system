using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibController : MonoBehaviour {

    public SetBackgroundImage setbackgroundimage;
    public GetHDFacePointTest gethdfacepointtest;

    public Button CalibOn_bt;

    public GameObject CalibMenu;
    public Button Xplus;
    public Button Yplus;
    public Button Zplus;
    public Button Xminus;
    public Button Yminus;
    public Button Zminus;

    private bool is_CalibOn;

	// Use this for initialization
	void Start () {
        is_CalibOn = false;

        CalibOn_bt.onClick.AddListener(CalibOnClick);

        Xplus.onClick.AddListener(XplusOnClick);
        Yplus.onClick.AddListener(YplusOnClick);
        Zplus.onClick.AddListener(ZplusOnClick);

        Xminus.onClick.AddListener(XminusOnClick);
        Yminus.onClick.AddListener(YminusOnClick);
        Zminus.onClick.AddListener(ZminusOnClick);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CalibOnClick()
    {
        is_CalibOn = !is_CalibOn;
        if (is_CalibOn == true)
        {
            CalibMenu.SetActive(true);
            gethdfacepointtest.DrawSpecialPointOn = true;
        }
        else
        {
            CalibMenu.SetActive(false);
            gethdfacepointtest.DrawSpecialPointOn = false;
        }

    }

    void XplusOnClick()
    {
        setbackgroundimage.adjustedCameraOffset += 0.0025f;
    }

    void YplusOnClick()
    {
        setbackgroundimage.adjustedCameraOffset_Y += 0.0025f;
    }

    void ZplusOnClick()
    {
        setbackgroundimage.adjustedCameraOffset_Z += 0.0025f;
    }

    void XminusOnClick()
    {
        setbackgroundimage.adjustedCameraOffset -= 0.0025f;
    }

    void YminusOnClick()
    {
        setbackgroundimage.adjustedCameraOffset_Y -= 0.0025f;
    }

    void ZminusOnClick()
    {
        setbackgroundimage.adjustedCameraOffset_Z -= 0.0025f;
    }

}
