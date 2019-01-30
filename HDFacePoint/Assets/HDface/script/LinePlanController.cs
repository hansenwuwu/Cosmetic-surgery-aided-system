using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinePlanController : MonoBehaviour {

    // define plan positoin
    private Vector3[] plan_pos = new Vector3[11];

    // define current plan number ( -1 means no plan)
    private int current_plan = -1;

    // define current lifting / single number
    private int current_lifting_num = -1;
    private int current_single_num = -1;

    // define each lifting/single pos num ( -1 means not on table)
    private int[] lifting_pos_num = new int[11] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
    private int[] single_pos_num = new int[11] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

    // define each lifting/single is use
    private bool[] lifting_use = new bool[11] {false, false, false, false, false, false, false, false, false, false, false };
    private bool[] single_use = new bool[11] { false, false, false, false, false, false, false, false, false, false, false };

    // lifting/single plan
    public GameObject[] lifting_plan;
    public GameObject[] single_plan;

    // add button
    public Button Add_Lifting_bt;
    public Button Add_Single_bt;

    public Button Add_Long_bt;
    public Text Longbt_text;
    private bool is_LongClick;

    // define delete button
    public Button[] lifting_Delete_bt;
    public Button[] single_Delete_bt;

    // define edit button
    public Button[] lifting_Edit_bt;
    public Button[] single_Edit_bt;

    // button
    public Button Line01_Edit_bt;
    public Button Line01_Cancel_bt;
    public Button Line02_Edit_bt;
    public Button Line02_Cancel_bt;
    public Button Line03_Edit_bt;
    public Button Line03_Cancel_bt;

    public PointRecord pointrecord;

    // Use this for initialization
    void Start () {

        is_LongClick = false;

        // initial plan position
        plan_pos[0] = new Vector2(-475, 75);
        plan_pos[1] = new Vector2(-380, 75);
        plan_pos[2] = new Vector2(-285, 75);
        plan_pos[3] = new Vector2(-190, 75);
        plan_pos[4] = new Vector2(-95, 75);
        plan_pos[5] = new Vector2(-0, 75);
        plan_pos[6] = new Vector2(95, 75);
        plan_pos[7] = new Vector2(190, 75);
        plan_pos[8] = new Vector2(285, 75);
        plan_pos[9] = new Vector2(380, 75);
        plan_pos[10] = new Vector2(475, 75);

        // call Add Button
        Add_Lifting_bt.onClick.AddListener(AddLiftingOnClick);
        Add_Single_bt.onClick.AddListener(AddSingleOnClick);

        Add_Long_bt.onClick.AddListener(AddLongOnClick);

        // call lifting delete button
        lifting_Delete_bt[0].onClick.AddListener(LiftingDelete01OnClick);
        lifting_Delete_bt[1].onClick.AddListener(LiftingDelete02OnClick);
        lifting_Delete_bt[2].onClick.AddListener(LiftingDelete03OnClick);
        lifting_Delete_bt[3].onClick.AddListener(LiftingDelete04OnClick);
        lifting_Delete_bt[4].onClick.AddListener(LiftingDelete05OnClick);
        lifting_Delete_bt[5].onClick.AddListener(LiftingDelete06OnClick);
        lifting_Delete_bt[6].onClick.AddListener(LiftingDelete07OnClick);
        lifting_Delete_bt[7].onClick.AddListener(LiftingDelete08OnClick);
        lifting_Delete_bt[8].onClick.AddListener(LiftingDelete09OnClick);
        lifting_Delete_bt[9].onClick.AddListener(LiftingDelete10OnClick);
        lifting_Delete_bt[10].onClick.AddListener(LiftingDelete11OnClick);

        // call single delete button
        single_Delete_bt[0].onClick.AddListener(SingleDelete01OnClick);
        single_Delete_bt[1].onClick.AddListener(SingleDelete02OnClick);
        single_Delete_bt[2].onClick.AddListener(SingleDelete03OnClick);
        single_Delete_bt[3].onClick.AddListener(SingleDelete04OnClick);
        single_Delete_bt[4].onClick.AddListener(SingleDelete05OnClick);
        single_Delete_bt[5].onClick.AddListener(SingleDelete06OnClick);
        single_Delete_bt[6].onClick.AddListener(SingleDelete07OnClick);
        single_Delete_bt[7].onClick.AddListener(SingleDelete08OnClick);
        single_Delete_bt[8].onClick.AddListener(SingleDelete09OnClick);
        single_Delete_bt[9].onClick.AddListener(SingleDelete10OnClick);
        single_Delete_bt[10].onClick.AddListener(SingleDelete11OnClick);

        // call lifting edit button
        lifting_Edit_bt[0].onClick.AddListener(LiftingEdit01OnClick);
        lifting_Edit_bt[1].onClick.AddListener(LiftingEdit02OnClick);
        lifting_Edit_bt[2].onClick.AddListener(LiftingEdit03OnClick);
        lifting_Edit_bt[3].onClick.AddListener(LiftingEdit04OnClick);
        lifting_Edit_bt[4].onClick.AddListener(LiftingEdit05OnClick);
        lifting_Edit_bt[5].onClick.AddListener(LiftingEdit06OnClick);
        lifting_Edit_bt[6].onClick.AddListener(LiftingEdit07OnClick);
        lifting_Edit_bt[7].onClick.AddListener(LiftingEdit08OnClick);
        lifting_Edit_bt[8].onClick.AddListener(LiftingEdit09OnClick);
        lifting_Edit_bt[9].onClick.AddListener(LiftingEdit10OnClick);
        lifting_Edit_bt[10].onClick.AddListener(LiftingEdit11OnClick);

        // call single edit button
        single_Edit_bt[0].onClick.AddListener(SingleEdit01OnClick);
        single_Edit_bt[1].onClick.AddListener(SingleEdit02OnClick);
        single_Edit_bt[2].onClick.AddListener(SingleEdit03OnClick);
        single_Edit_bt[3].onClick.AddListener(SingleEdit04OnClick);
        single_Edit_bt[4].onClick.AddListener(SingleEdit05OnClick);
        single_Edit_bt[5].onClick.AddListener(SingleEdit06OnClick);
        single_Edit_bt[6].onClick.AddListener(SingleEdit07OnClick);
        single_Edit_bt[7].onClick.AddListener(SingleEdit08OnClick);
        single_Edit_bt[8].onClick.AddListener(SingleEdit09OnClick);
        single_Edit_bt[9].onClick.AddListener(SingleEdit10OnClick);
        single_Edit_bt[10].onClick.AddListener(SingleEdit11OnClick);

        // test hovering
        

        Line01_Edit_bt.interactable = false;
        Line01_Cancel_bt.interactable = false;
        Line02_Edit_bt.interactable = false;
        Line02_Cancel_bt.interactable = false;
        Line03_Edit_bt.interactable = false;
        Line03_Cancel_bt.interactable = false;

        Line01_Edit_bt.onClick.AddListener(Line01EditOnClick);
        Line02_Edit_bt.onClick.AddListener(Line02EditOnClick);
        Line03_Edit_bt.onClick.AddListener(Line03EditOnClick);

        Line01_Cancel_bt.onClick.AddListener(Line01CancelOnClick);
        Line02_Cancel_bt.onClick.AddListener(Line02CancelOnClick);
        Line03_Cancel_bt.onClick.AddListener(Line03CancelOnClick);

        Line01_Cancel_bt.interactable = false;
        Line02_Cancel_bt.interactable = false;
        Line03_Cancel_bt.interactable = false;

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void AddLongOnClick()
    {
        is_LongClick = !is_LongClick;

        if (is_LongClick == true)
        {
            // change normal color
            ColorBlock cb = Add_Long_bt.colors;
            cb.normalColor = new Color(68 / 255f, 152 / 255f, 112 / 255f);
            Add_Long_bt.colors = cb;
            Longbt_text.text = "Cancel Lifting";
        }
        else
        {
            // return normal color
            ColorBlock cb = Add_Long_bt.colors;
            cb.normalColor = new Color(48/255f, 50/255f, 59/255f);
            Add_Long_bt.colors = cb;
            Longbt_text.text = "Lifting";
        }

        
    }

    void Line01EditOnClick()
    {
        //Debug.Log("edit 01 click");

        Line01_Cancel_bt.interactable = true;

        Line01_Edit_bt.interactable = false;
        Line02_Edit_bt.interactable = false;
        Line03_Edit_bt.interactable = false;

        pointrecord.EditStart(1);

    }

    void Line02EditOnClick()
    {
        //Debug.Log("edit 02 click");

        Line02_Cancel_bt.interactable = true;

        Line01_Edit_bt.interactable = false;
        Line02_Edit_bt.interactable = false;
        Line03_Edit_bt.interactable = false;

        pointrecord.EditStart(2);

    }

    void Line03EditOnClick()
    {
        //Debug.Log("edit 03 click");

        Line03_Cancel_bt.interactable = true;

        Line01_Edit_bt.interactable = false;
        Line02_Edit_bt.interactable = false;
        Line03_Edit_bt.interactable = false;

        pointrecord.EditStart(3);

    }

    void Line01CancelOnClick()
    {
        //Debug.Log("cancel 01 click");

        Line01_Cancel_bt.interactable = false;

        Line01_Edit_bt.interactable = true;
        Line02_Edit_bt.interactable = true;
        Line03_Edit_bt.interactable = true;

        pointrecord.EditEnd();

    }

    void Line02CancelOnClick()
    {
        //Debug.Log("cancel 02 click");

        Line02_Cancel_bt.interactable = false;

        Line01_Edit_bt.interactable = true;
        Line02_Edit_bt.interactable = true;
        Line03_Edit_bt.interactable = true;

        pointrecord.EditEnd();

    }

    void Line03CancelOnClick()
    {
        //Debug.Log("cancel 03 click");

        Line03_Cancel_bt.interactable = false;

        Line01_Edit_bt.interactable = true;
        Line02_Edit_bt.interactable = true;
        Line03_Edit_bt.interactable = true;

        pointrecord.EditEnd();

    }

    // Add lifting button
    void AddLiftingOnClick()
    {
        if (current_plan < 10)
        {
            current_plan += 1;
            // search avalible plan
            for (int i=0; i<11; i++)
            {
                if (lifting_use[i] == false)
                {
                    lifting_pos_num[i] = current_plan;

                    RectTransform temp = lifting_plan[i].GetComponent<RectTransform>();
                    temp.anchoredPosition = plan_pos[current_plan];

                    lifting_use[i] = true;

                    break;
                }
            }
        }
    }

    // Add single button
    void AddSingleOnClick()
    {
        if (current_plan < 10)
        {
            current_plan += 1;
            // search avalible plan
            for (int i = 0; i < 11; i++)
            {
                if (single_use[i] == false)
                {
                    single_pos_num[i] = current_plan;

                    RectTransform temp = single_plan[i].GetComponent<RectTransform>();
                    temp.anchoredPosition = plan_pos[current_plan];

                    single_use[i] = true;

                    break;
                }
            }
        }
    }

    // define lifting delete button
    void LiftingDelete01OnClick()
    {
        pointrecord.DeleteFunc(1);

        RectTransform tempRR = lifting_plan[0].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;
        current_lifting_num -= 1;

        int temp = lifting_pos_num[0];

        lifting_pos_num[0] = -1;

        // search lift
        for (int i=0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }

        lifting_use[0] = false;

    }

    void LiftingDelete02OnClick()
    {
        pointrecord.DeleteFunc(2);

        RectTransform tempRR = lifting_plan[1].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;
        current_lifting_num -= 1;

        int temp = lifting_pos_num[1];

        lifting_pos_num[1] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }
        lifting_use[1] = false;
    }

    void LiftingDelete03OnClick()
    {
        pointrecord.DeleteFunc(3);

        RectTransform tempRR = lifting_plan[2].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;
        current_lifting_num -= 1;

        int temp = lifting_pos_num[2];

        lifting_pos_num[2] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }
        lifting_use[2] = false;
    }

    void LiftingDelete04OnClick()
    {
        pointrecord.DeleteFunc(4);

        RectTransform tempRR = lifting_plan[3].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;
        current_lifting_num -= 1;

        int temp = lifting_pos_num[3];

        lifting_pos_num[3] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }
        lifting_use[3] = false;
    }

    void LiftingDelete05OnClick()
    {
        pointrecord.DeleteFunc(5);

        RectTransform tempRR = lifting_plan[4].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;
        current_lifting_num -= 1;

        int temp = lifting_pos_num[4];

        lifting_pos_num[4] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }
        lifting_use[4] = false;
    }

    void LiftingDelete06OnClick()
    {
        pointrecord.DeleteFunc(6);

        RectTransform tempRR = lifting_plan[5].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;
        current_lifting_num -= 1;

        int temp = lifting_pos_num[5];

        lifting_pos_num[5] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }
        lifting_use[5] = false;
    }

    void LiftingDelete07OnClick()
    {
        pointrecord.DeleteFunc(7);

        RectTransform tempRR = lifting_plan[6].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;
        current_lifting_num -= 1;

        int temp = lifting_pos_num[6];

        lifting_pos_num[6] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }
        lifting_use[6] = false;
    }

    void LiftingDelete08OnClick()
    {
        pointrecord.DeleteFunc(8);

        RectTransform tempRR = lifting_plan[7].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;
        current_lifting_num -= 1;

        int temp = lifting_pos_num[7];

        lifting_pos_num[7] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }
        lifting_use[7] = false;
    }

    void LiftingDelete09OnClick()
    {
        pointrecord.DeleteFunc(9);

        RectTransform tempRR = lifting_plan[8].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;
        current_lifting_num -= 1;

        int temp = lifting_pos_num[8];

        lifting_pos_num[8] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }
        lifting_use[8] = false;
    }

    void LiftingDelete10OnClick()
    {
        pointrecord.DeleteFunc(10);

        RectTransform tempRR = lifting_plan[9].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;
        current_lifting_num -= 1;

        int temp = lifting_pos_num[9];

        lifting_pos_num[9] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }
        lifting_use[9] = false;
    }

    void LiftingDelete11OnClick()
    {
        pointrecord.DeleteFunc(11);

        RectTransform tempRR = lifting_plan[10].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;
        current_lifting_num -= 1;

        int temp = lifting_pos_num[10];

        lifting_pos_num[10] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }
        lifting_use[10] = false;
    }


    // define single delete button
    void SingleDelete01OnClick()
    {
        pointrecord.DeleteFunc(12);

        RectTransform tempRR = single_plan[0].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;

        int temp = single_pos_num[0];

        single_pos_num[0] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }

        single_use[0] = false;

    }

    void SingleDelete02OnClick()
    {
        pointrecord.DeleteFunc(13);

        RectTransform tempRR = single_plan[1].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;

        int temp = single_pos_num[1];

        single_pos_num[1] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }

        single_use[1] = false;

    }

    void SingleDelete03OnClick()
    {
        pointrecord.DeleteFunc(14);

        RectTransform tempRR = single_plan[2].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;

        int temp = single_pos_num[2];

        single_pos_num[2] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }

        single_use[2] = false;

    }

    void SingleDelete04OnClick()
    {
        pointrecord.DeleteFunc(15);

        RectTransform tempRR = single_plan[3].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;

        int temp = single_pos_num[3];

        single_pos_num[3] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }

        single_use[3] = false;

    }

    void SingleDelete05OnClick()
    {
        pointrecord.DeleteFunc(16);

        RectTransform tempRR = single_plan[4].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;

        int temp = single_pos_num[4];

        single_pos_num[4] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }

        single_use[4] = false;

    }

    void SingleDelete06OnClick()
    {
        pointrecord.DeleteFunc(17);

        RectTransform tempRR = single_plan[5].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;

        int temp = single_pos_num[5];

        single_pos_num[5] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }

        single_use[5] = false;

    }

    void SingleDelete07OnClick()
    {
        pointrecord.DeleteFunc(18);

        RectTransform tempRR = single_plan[6].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;

        int temp = single_pos_num[6];

        single_pos_num[6] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }

        single_use[6] = false;

    }

    void SingleDelete08OnClick()
    {
        pointrecord.DeleteFunc(19);

        RectTransform tempRR = single_plan[7].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;

        int temp = single_pos_num[7];

        single_pos_num[7] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }

        single_use[7] = false;

    }

    void SingleDelete09OnClick()
    {
        pointrecord.DeleteFunc(20);

        RectTransform tempRR = single_plan[8].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;

        int temp = single_pos_num[8];

        single_pos_num[8] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }

        single_use[8] = false;

    }

    void SingleDelete10OnClick()
    {
        pointrecord.DeleteFunc(21);

        RectTransform tempRR = single_plan[9].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;

        int temp = single_pos_num[9];

        single_pos_num[9] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }

        single_use[9] = false;

    }

    void SingleDelete11OnClick()
    {
        pointrecord.DeleteFunc(22);

        RectTransform tempRR = single_plan[10].GetComponent<RectTransform>();
        tempRR.anchoredPosition = new Vector3(-475, -300, 0);

        current_plan -= 1;

        int temp = single_pos_num[10];

        single_pos_num[10] = -1;

        // search lift
        for (int i = 0; i < 11; i++)
        {
            if (lifting_pos_num[i] > temp)
            {
                RectTransform tempR = lifting_plan[i].GetComponent<RectTransform>();
                lifting_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[lifting_pos_num[i]];
            }
        }

        // search single
        for (int i = 0; i < 11; i++)
        {
            if (single_pos_num[i] > temp)
            {
                RectTransform tempR = single_plan[i].GetComponent<RectTransform>();
                single_pos_num[i] -= 1;
                tempR.anchoredPosition = plan_pos[single_pos_num[i]];
            }
        }

        single_use[10] = false;

    }


    // define edit button
    void LiftingEdit01OnClick()
    {
        pointrecord.EditStart(1);
    }

    void LiftingEdit02OnClick()
    {
        pointrecord.EditStart(2);
    }

    void LiftingEdit03OnClick()
    {
        pointrecord.EditStart(3);
    }

    void LiftingEdit04OnClick()
    {
        pointrecord.EditStart(4);
    }

    void LiftingEdit05OnClick()
    {
        pointrecord.EditStart(5);
    }

    void LiftingEdit06OnClick()
    {
        pointrecord.EditStart(6);
    }

    void LiftingEdit07OnClick()
    {
        pointrecord.EditStart(7);
    }

    void LiftingEdit08OnClick()
    {
        pointrecord.EditStart(8);
    }

    void LiftingEdit09OnClick()
    {
        pointrecord.EditStart(9);
    }

    void LiftingEdit10OnClick()
    {
        pointrecord.EditStart(10);
    }

    void LiftingEdit11OnClick()
    {
        pointrecord.EditStart(11);
    }

    void SingleEdit01OnClick()
    {
        pointrecord.EditStart(12);
    }

    void SingleEdit02OnClick()
    {
        pointrecord.EditStart(13);
    }

    void SingleEdit03OnClick()
    {
        pointrecord.EditStart(14);
    }

    void SingleEdit04OnClick()
    {
        pointrecord.EditStart(15);
    }

    void SingleEdit05OnClick()
    {
        pointrecord.EditStart(16);
    }

    void SingleEdit06OnClick()
    {
        pointrecord.EditStart(17);
    }

    void SingleEdit07OnClick()
    {
        pointrecord.EditStart(18);
    }

    void SingleEdit08OnClick()
    {
        pointrecord.EditStart(19);
    }

    void SingleEdit09OnClick()
    {
        pointrecord.EditStart(20);
    }

    void SingleEdit10OnClick()
    {
        pointrecord.EditStart(21);
    }

    void SingleEdit11OnClick()
    {
        pointrecord.EditStart(22);
    }
}
