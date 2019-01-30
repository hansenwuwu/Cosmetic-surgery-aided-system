using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;
using Microsoft.Kinect.Face;

public class PointRecord : MonoBehaviour {

    [Tooltip("GUI-texture used to display the color camera feed on the scene background.")]
    public GUITexture backgroundImage;

    [Tooltip("Camera that will be used to overlay the 3D-objects over the background.")]
    public Camera foregroundCamera;

    [Tooltip("GUI-Text to display the FT-manager debug messages.")]
    public GUIText debugText;
    public GUIText debugText2;

    public FacetrackingManager facetrackingmanager;
    public GetHDFacePoint getHDFacePoints;
    public CombineButtonController bt_control;

    public int mouse_x_calib;
    public int mouse_y_calib;

    // see if draw line done
    public bool isLineDraw = false;

    private KinectSensor sensor;

    // store face and color freeze data
    private Vector3[] FacePointBuffer = new Vector3[1347];
    private Vector2[] MapColorPt = new Vector2[1347];

    // check if record mode start
    private bool is_getallpoint = false;

    // check which edit mode
    private int edit_status = 0;

    // check is in continue mode
    private bool is_conti = false;

    private int count_point_num = -1;
    private bool is_GetTwoPt = false;

    // calculate face plane point
    private float min_dis_1 = 999999;
    private float min_dis_2 = 999999;
    private int[] min_index_1 = new int[22] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                                                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
    private int[] min_index_2 = new int[22] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                                                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};

    // store mouse pick point
    private Vector2[] pick_pt = new Vector2[2];

    // store line point & face plane point
    private int[,] pt_index = new int[22, 2] { { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 },
                                                { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}};
    private int[,] face_index = new int[22 ,2] {  { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 },
                                                { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0} };

    // parameter for search closest point
    private float min_dis = 99999;
    private int min_index = -1;

    // check if line is done edit
    private bool[] isLineDone = new bool[22] { false, false, false, false , false , false , false , false , false , false , false,
                                               false, false, false, false, false, false, false, false, false, false, false };

    // object
    public Transform line1;
    public Transform[] line_pt;

    public Transform[] linegroup;

    // material
    public Material red_ball;
    public Material green_ball;

    
    // Use this for initialization
    void Start () {
        sensor = KinectSensor.GetDefault();
    }
	
	// Update is called once per frame
	void Update () {

        KinectManager manager = KinectManager.Instance;

        if (manager && manager.IsInitialized() && foregroundCamera)
        {
            if (facetrackingmanager.IsTrackingFace())   // check if get face
            {
                // ------------------------- start take point -----------------------------
                if (is_getallpoint && edit_status != 0)     // check if point collect && check 
                {

                    // ------------- get two point --------------
                    if (Input.GetMouseButtonUp(0) && count_point_num < 2)
                    {
                        if (count_point_num >= 0)
                        {
                            pick_pt[count_point_num] = new Vector2(Input.mousePosition.x + mouse_x_calib, (Input.mousePosition.y + mouse_y_calib));

                            // search the closest face point
                            for (int i = 0; i < 1347; i++)
                            {
                                if (Vector2.Distance(pick_pt[count_point_num], MapColorPt[i]) < min_dis)
                                {
                                    min_dis = Vector2.Distance(pick_pt[count_point_num], MapColorPt[i]);
                                    min_index = i;
                                }
                            }

                            // record point index
                            pt_index[edit_status-1, count_point_num] = min_index;

                            // position line point
                            Vector3 temp = FacePointBuffer[min_index];
                            line_pt[count_point_num].transform.position = temp;

                            debugText2.GetComponent<GUIText>().text = "pick point: " + new Vector2(Input.mousePosition.x + mouse_x_calib, Input.mousePosition.y + mouse_y_calib);
                        }
                        count_point_num++;

                        min_dis = 99999;
                        min_index = -1;
                    }

                    // ------------------- find two point center and the closest to it --------------------
                    if (count_point_num == 2 && is_GetTwoPt == false)
                    {
                        Vector2 centerpt = (MapColorPt[pt_index[edit_status-1, 0]] + MapColorPt[pt_index[edit_status-1, 1]]) / 2;

                        // find the smallest
                        for (int i = 0; i < 1347; i++)
                        {
                            if (Vector2.Distance(MapColorPt[i], centerpt) < min_dis_1)
                            {
                                min_dis_1 = Vector2.Distance(MapColorPt[i], centerpt);
                                min_index_1[edit_status-1] = i;
                            }
                        }

                        // find the second smallest
                        for (int i = 0; i < 1347; i++)
                        {
                            if (Vector2.Distance(MapColorPt[i], centerpt) < min_dis_2 && i != min_index_1[edit_status-1])
                            {
                                min_dis_2 = Vector2.Distance(MapColorPt[i], centerpt);
                                min_index_2[edit_status-1] = i;
                            }
                        }

                        is_GetTwoPt = true;
                    }

                    // record two point
                    if (count_point_num == 2)
                    {
                        Vector3 start_pt = FacePointBuffer[pt_index[edit_status-1, 0]];
                        Vector3 end_pt = FacePointBuffer[pt_index[edit_status-1, 1]];
                        Vector3 ct1_pt = FacePointBuffer[min_index_1[edit_status-1]];
                        Vector3 ct2_pt = FacePointBuffer[min_index_2[edit_status-1]];

                        linegroup[edit_status-1].transform.position = start_pt;

                        // rotation
                        Vector3 targetDir = end_pt - start_pt;
                        Vector3 oriDir = new Vector3(0.0f, 0.0f, 1.0f);
                        Vector3 newDir = Vector3.RotateTowards(oriDir, targetDir, 360.0f, 360.0f);

                        Vector3 v2 = ct1_pt - ct2_pt;

                        // set right rotation
                        Vector3 cro_out = Vector3.Cross(v2, newDir);

                        // calculate left or right face   (1297,1293) two side
                        Vector3 left_pt = getHDFacePoints.GetAllPoint(1297);
                        Vector3 right_pt = getHDFacePoints.GetAllPoint(1293);
                        Vector3 temp_center_pt = (left_pt + right_pt) / 2;
                        Vector3 temp_dir_vec = ct1_pt - temp_center_pt;
                        //Debug.Log(temp_dir_vec.x + ", " + temp_dir_vec.y + ", " + temp_dir_vec.z);

                        if (temp_dir_vec.x < 0) // left face point
                        {
                            if (cro_out.x < 0)
                            {
                                cro_out = Vector3.Cross(newDir, v2);
                            }
                        }
                        else  // right face point
                        {
                            if (cro_out.x > 0)
                            {
                                cro_out = Vector3.Cross(newDir, v2);
                            }
                        }

                        linegroup[edit_status-1].transform.rotation = Quaternion.LookRotation(newDir, cro_out);      // second parameter indicate upward direction 

                        // point position
                        line_pt[0].transform.position = Vector3.zero;
                        line_pt[1].transform.position = Vector3.zero;

                        isLineDone[edit_status - 1] = true;

                        // reset 
                        min_dis_1 = 999999;
                        min_dis_2 = 999999;

                        edit_status = 0;
                        count_point_num = -1;
                        is_GetTwoPt = false;

                    }   
                }

                // ------------------  position line ------------------------
                if (is_conti == true)
                {
                    for (int i=0; i<22; i++)
                    {
                        if (isLineDone[i] == true)
                        {
                            Vector3 start_pt = getHDFacePoints.GetAllPoint(pt_index[i, 0]);
                            Vector3 end_pt = getHDFacePoints.GetAllPoint(pt_index[i, 1]);
                            Vector3 ct1_pt = getHDFacePoints.GetAllPoint(min_index_1[i]);
                            Vector3 ct2_pt = getHDFacePoints.GetAllPoint(min_index_2[i]);

                            float pt_dis = Vector3.Distance(start_pt, end_pt);

                            // position
                            linegroup[i].transform.position = start_pt;

                            // rotation
                            Vector3 targetDir = end_pt - start_pt;
                            Vector3 oriDir = new Vector3(0.0f, 0.0f, 1.0f);
                            Vector3 newDir = Vector3.RotateTowards(oriDir, targetDir, 360.0f, 360.0f);

                            Vector3 v2 = ct1_pt - ct2_pt;

                            // set right rotation
                            Vector3 cro_out = Vector3.Cross(v2, newDir);

                            // calculate left or right face   (1297,1293) two side
                            Vector3 left_pt = getHDFacePoints.GetAllPoint(1297);
                            Vector3 right_pt = getHDFacePoints.GetAllPoint(1293);
                            Vector3 temp_center_pt = (left_pt + right_pt) / 2;
                            Vector3 temp_dir_vec = ct1_pt - temp_center_pt;
                            //Debug.Log(temp_dir_vec.x + ", " + temp_dir_vec.y + ", " + temp_dir_vec.z);

                            if (temp_dir_vec.x < 0) // left face point
                            {
                                if (cro_out.x < 0)
                                {
                                    cro_out = Vector3.Cross(newDir, v2);
                                }
                            }
                            else  // right face point
                            {
                                if (cro_out.x > 0)
                                {
                                    cro_out = Vector3.Cross(newDir, v2);
                                }
                            }

                            linegroup[i].transform.rotation = Quaternion.LookRotation(newDir, cro_out);      // second parameter indicate upward direction   
                        }
                    }
                }

            }
        }
   
	}

    // for record button, record 1347 face point color and space data
    public void RecordFacePoint()
    {
        KinectManager manager = KinectManager.Instance;

        if (manager && manager.IsInitialized() && foregroundCamera)
        {
            // get the background rectangle (use the portrait background, if available)
            Rect backgroundRect = foregroundCamera.pixelRect;

            if (facetrackingmanager.IsTrackingFace())
            {
                // get all face point to color cord
                for (int i = 0; i < 1347; i++)
                {
                    FacePointBuffer[i] = getHDFacePoints.GetAllPoint(i);
                    MapColorPt[i] = manager.GetHDFaceColorPoint(FacePointBuffer[i], foregroundCamera, backgroundRect);

                }
                is_getallpoint = true;
                is_conti = false;
            }
            
        }
    }

    public void EditStart(int i)
    {
        edit_status = i;
    }

    public void EditEnd()
    {
        edit_status = 0;
    }

    public void NotRecord()
    {
        FacePointBuffer.Initialize();
        MapColorPt.Initialize();
        is_getallpoint = false;
        is_conti = true;
    }

    public void DeleteFunc(int i)
    {
        min_index_1[i - 1] = -1;
        min_index_2[i - 1] = -1;

        pt_index[i - 1, 0] = 0;
        pt_index[i - 1, 1] = 0;

        face_index[i - 1, 0] = 0;
        face_index[i - 1, 1] = 0;

        isLineDone[i - 1] = false;

        linegroup[i - 1].transform.position = new Vector3(0.075f, 0, 0);
    }

    public void EnterColor(int i)
    {
        if (i<=11)
        {
            GameObject.Find("/Line/LiftingLine0" + i + "/stick01/Cylinder001").GetComponent<Renderer>().material = red_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/stick02/Cylinder001").GetComponent<Renderer>().material = red_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/stick03/Cylinder001").GetComponent<Renderer>().material = red_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/stick04/Cylinder001").GetComponent<Renderer>().material = red_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/stick05/Cylinder001").GetComponent<Renderer>().material = red_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/arrow01/default").GetComponent<Renderer>().material = red_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/arrow02/default").GetComponent<Renderer>().material = red_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/arrow03/default").GetComponent<Renderer>().material = red_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/arrow04/default").GetComponent<Renderer>().material = red_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/arrow05/default").GetComponent<Renderer>().material = red_ball;
        }else if (i>11)
        {
            i = i - 11;
            GameObject.Find("/Line/SingleLine0" + i + "/arrow01/default").GetComponent<Renderer>().material = red_ball;
            GameObject.Find("/Line/SingleLine0" + i + "/stick03/Cylinder001").GetComponent<Renderer>().material = red_ball;
        }

        
    }

    public void ExitColor(int i)
    {
        if (i<=11)
        {
            GameObject.Find("/Line/LiftingLine0" + i + "/stick01/Cylinder001").GetComponent<Renderer>().material = green_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/stick02/Cylinder001").GetComponent<Renderer>().material = green_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/stick03/Cylinder001").GetComponent<Renderer>().material = green_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/stick04/Cylinder001").GetComponent<Renderer>().material = green_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/stick05/Cylinder001").GetComponent<Renderer>().material = green_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/arrow01/default").GetComponent<Renderer>().material = green_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/arrow02/default").GetComponent<Renderer>().material = green_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/arrow03/default").GetComponent<Renderer>().material = green_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/arrow04/default").GetComponent<Renderer>().material = green_ball;
            GameObject.Find("/Line/LiftingLine0" + i + "/arrow05/default").GetComponent<Renderer>().material = green_ball;
        }else if (i>11)
        {
            i = i - 11;
            GameObject.Find("/Line/SingleLine0" + i + "/arrow01/default").GetComponent<Renderer>().material = green_ball;
            GameObject.Find("/Line/SingleLine0" + i + "/stick03/Cylinder001").GetComponent<Renderer>().material = green_ball;
        }

        
    }

}
