using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using Microsoft.Kinect.Face;

public class GetHDFacePointTest : MonoBehaviour {

    [Tooltip("GUI-texture used to display the color camera feed on the scene background.")]
    public GUITexture backgroundImage;

    [Tooltip("Camera that will be used to overlay the 3D-objects over the background.")]
    public Camera foregroundCamera;

    public KinectManager manager;
    public FacetrackingManager facetrackingmanager;
    public GetHDFacePoint getHDFacePoints;

    // enable function
    public bool DrawSpecialPointOn;

    // Face point
    public Transform[] sphere;

    // line
    public Transform line1;
    public Transform start_p;
    public Transform end_p;

    [Tooltip("gameobject to be copy")]
    public GameObject copyGameObject;
    public GameObject superGameObject;
    private GameObject[] childGameObject = new GameObject[1347];

    private KinectSensor sensor;

    // Use this for initialization
    void Start () {
        sensor = KinectSensor.GetDefault();

        for (int i = 0; i<1347; i++)
        {
            childGameObject[i] = Instantiate(copyGameObject);
            childGameObject[i].transform.parent = superGameObject.transform;
            childGameObject[i].transform.localPosition = Vector3.zero;
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (manager && manager.IsInitialized() && foregroundCamera)
        {
            // get the background rectangle (use the portrait background, if available)
            Rect backgroundRect = foregroundCamera.pixelRect;

            // check if face is tracking
            if (facetrackingmanager.IsTrackingFace())
            {
                if (DrawSpecialPointOn)
                {
                    //------------ draw 35 special face point------------
                    //Vector3[] newFacePos = new Vector3[35];

                    //HighDetailFacePoints[] facePoints = (HighDetailFacePoints[])System.Enum.GetValues(typeof(HighDetailFacePoints));

                    //for (int i = 0; i < facePoints.Length; i++)
                    //{
                    //    HighDetailFacePoints point = facePoints[i];
                    //    newFacePos[i] = getHDFacePoints.GetPoint(point);

                    //    //Vector3 temp = manager.GetHDFaceOverlay(newFacePos[i], foregroundCamera, backgroundRect);
                    //    Vector3 temp = newFacePos[i];

                    //    //sphere[i].transform.position = newFacePos[i];
                    //    sphere[i].transform.position = temp;
                    //}

                    //------------ draw all face points------------
                    for (int i = 0; i < 1347; i++)
                    {
                        // draw specific point to white
                        //if (i == 420)
                        //{
                        //    Material m_Material = childGameObject[i].GetComponent<Renderer>().material;
                        //    m_Material.color = UnityEngine.Color.white;
                        //    childGameObject[i].GetComponent<Renderer>().material = m_Material;
                        //}

                        childGameObject[i].transform.position = getHDFacePoints.GetAllPoint(i);
                    }
                }
                else
                {
                    for (int i = 0; i < 1347; i++)
                    {
                        childGameObject[i].transform.position = new Vector3(0.075f, 0, 0);
                    }
                }



            }

        }

        

    }
    
}