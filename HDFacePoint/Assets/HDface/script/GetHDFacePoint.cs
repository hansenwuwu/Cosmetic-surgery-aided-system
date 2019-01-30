using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Kinect.Face;

public class GetHDFacePoint : MonoBehaviour {

    [Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
    public int playerIndex = 0;

    private KinectManager manager = null;
    private FacetrackingManager faceManager = null;

    private Vector3[] faceVertices;
    private Dictionary<HighDetailFacePoints, Vector3> dictFacePoints = new Dictionary<HighDetailFacePoints, Vector3>();

    // check faceVertices ok
    private bool check_f = false;

    // returns the face point coordinates or Vector3.zero if not found
    public Vector3 GetPoint(HighDetailFacePoints pointType)
    {
        if (dictFacePoints != null && dictFacePoints.ContainsKey(pointType))
        {
            return dictFacePoints[pointType];
        }

        return Vector3.zero;
    }

    public Vector3 GetAllPoint(int i)
    {
        if (check_f && faceVertices[i] != null)
        {
            return faceVertices[i];
        }
        return Vector3.zero;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Matrix4x4 kinectToWorld = Matrix4x4.zero;

        //Quaternion quatTiltAngle = Quaternion.Euler(-manager.sensorAngle, 0.0f, 0.0f);
        //kinectToWorld.SetTRS(new Vector3(0.0f, manager.sensorHeight, 0.0f), quatTiltAngle, Vector3.one);

        if (!manager)
        {
            manager = KinectManager.Instance;
        }

        if (!faceManager)
        {
            faceManager = FacetrackingManager.Instance;
        }

        // get the face points
        if (manager != null && manager.IsInitialized() && faceManager && faceManager.IsFaceTrackingInitialized())
        {
            long userId = manager.GetUserIdByIndex(playerIndex);

            if (faceVertices == null)
            {
                //int iVertCount = faceManager.GetUserFaceVertexCount(userId);
                int iVertCount = 1347;

                if (iVertCount > 0)
                {
                    faceVertices = new Vector3[iVertCount];
                }

                check_f = false;
            }

            if (faceVertices != null)
            {
                if (faceManager.GetUserFaceVertices(userId, ref faceVertices))
                {

                    //-------------- get 35 special point-------------------
                    //Matrix4x4 kinectToWorld = manager.GetKinectToWorldMatrix();
                    HighDetailFacePoints[] facePoints = (HighDetailFacePoints[])System.Enum.GetValues(typeof(HighDetailFacePoints));

                    for (int i = 0; i < facePoints.Length; i++)
                    {
                        HighDetailFacePoints point = facePoints[i];
                        //dictFacePoints[point] = kinectToWorld.MultiplyPoint3x4(faceVertices[(int)point]);

                        dictFacePoints[point] = faceVertices[(int)point];

                    }

                    check_f = true;

                }
            }

        }
    }
}
