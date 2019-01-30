using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class Zhi : MonoBehaviour {

    [Tooltip("Camera that will be used to overlay the 3D-objects over the background.")]
    public Camera foregroundCamera;

    public FacetrackingManager facetrackingmanager;

    public GameObject cube;
    public GetHDFacePoint getHDFacePoints;
    private KinectSensor sensor;

    // Use this for initialization
    void Start () {
        sensor = KinectSensor.GetDefault();
    }
	
	// Update is called once per frame
	void Update () {

        KinectManager manager = KinectManager.Instance;

        if (manager && manager.IsInitialized() && foregroundCamera)
        {
            if (facetrackingmanager.IsTrackingFace())
            {
                cube.transform.position = getHDFacePoints.GetAllPoint(11);

                // rotation
                Vector3 targetDir = getHDFacePoints.GetAllPoint(11) - (getHDFacePoints.GetAllPoint(469) + getHDFacePoints.GetAllPoint(1117))/2 ;
                Vector3 oriDir = new Vector3(0.0f, 0.0f, 1.0f);
                Vector3 newDir = Vector3.RotateTowards(oriDir, targetDir, 360.0f, 360.0f);

                // set right rotation
                

                // calculate left or right face   (1297,1293) two side
                Vector3 left_pt = getHDFacePoints.GetAllPoint(1297);
                Vector3 right_pt = getHDFacePoints.GetAllPoint(1293);
                Vector3 temp_center_pt = left_pt - right_pt;

                Vector3 cro_out = Vector3.Cross(newDir, temp_center_pt);

                cube.transform.rotation = Quaternion.LookRotation(newDir, cro_out);


            }
        }  
    }
}
