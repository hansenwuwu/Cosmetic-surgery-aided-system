using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineButtonController : MonoBehaviour {

    public Button RecordBt;
    public Button ConBt;
    public Button ExitBt;

    public Button Line01_Edit_bt;
    public Button Line01_Cancel_bt;
    public Button Line02_Edit_bt;
    public Button Line02_Cancel_bt;
    public Button Line03_Edit_bt;
    public Button Line03_Cancel_bt;

    public SetBackgroundImage setbackgroundimage;
    public PointRecord pointrecord;

    // for screen freeze
    public bool isRecord_Click;
    public bool isRecord_On;

    public bool isContinue_Click;

    // for record hdface point
    public bool isFaceGet_Check;

    public int record_status;

    // Use this for initialization
    void Start () {

        RecordBt.onClick.AddListener(RecordBtOnclick);
        ConBt.onClick.AddListener(ConBtOnclick);
        ExitBt.onClick.AddListener(ExitBtOnclick);

        RecordBt.interactable = true;
        ConBt.interactable = false;
        ExitBt.interactable = true;

        isRecord_On = false;
        isRecord_Click = false;
        isContinue_Click = false;
        isFaceGet_Check = false;
        record_status = 0;
    }
	
	// Update is called once per frame
	void Update () {

	}

    void RecordBtOnclick()
    {
        // freeze screen
        setbackgroundimage.FreezeScreen();

        // menu
        RecordBt.interactable = false;
        ConBt.interactable = true;

        // line group
        Line01_Edit_bt.interactable = true;
        Line01_Cancel_bt.interactable = false;
        Line02_Edit_bt.interactable = true;
        Line02_Cancel_bt.interactable = false;
        Line03_Edit_bt.interactable = true;
        Line03_Cancel_bt.interactable = false;

        // start record mode
        pointrecord.RecordFacePoint();

        isRecord_Click = !isRecord_Click;
        record_status = 1;
        isRecord_On = true;

        isFaceGet_Check = !isFaceGet_Check;
    }

    void ConBtOnclick()
    {
        // un freeze screen
        setbackgroundimage.ContinueScreen();

        // menu
        RecordBt.interactable = true;
        ConBt.interactable = false;

        // line group
        Line01_Edit_bt.interactable = false;
        Line01_Cancel_bt.interactable = false;
        Line02_Edit_bt.interactable = false;
        Line02_Cancel_bt.interactable = false;
        Line03_Edit_bt.interactable = false;
        Line03_Cancel_bt.interactable = false;

        // not in record mode
        pointrecord.NotRecord();

        record_status = 2;
        isRecord_On = false;

        isContinue_Click = !isContinue_Click;
        
    }

    void ExitBtOnclick()
    {
        Application.Quit();
    }


}
