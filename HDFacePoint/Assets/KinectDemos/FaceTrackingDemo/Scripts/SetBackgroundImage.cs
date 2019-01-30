/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;

public class SetBackgroundImage : MonoBehaviour 
{
	[Tooltip("GUI-texture used to display the color camera feed on the scene background.")]
	public GUITexture backgroundImage;

	[Tooltip("Camera that will be set-up to display 3D-models in the Kinect FOV.")]
	public Camera foregroundCamera;

	[Tooltip("Use this setting to minimize the offset between the image and the model overlay.")]
	[Range(-0.1f, 0.1f)]
	public float adjustedCameraOffset = 0f;

    [Range(-0.1f, 0.1f)]
    public float adjustedCameraOffset_Y = 0f;

    [Range(-0.1f, 0.1f)]
    public float adjustedCameraOffset_Z = 0f;

    public CombineButtonController bt_control;

    // variable to track the current camera offset
    private float currentCameraOffset = 0f;

    private float currentCameraOffset_Y = 0f;

    private float currentCameraOffset_Z = 0f;

    // initial camera position
    private Vector3 initialCameraPos = Vector3.zero;

    private byte[] color_map_buffer = new byte[1920*1080*4];

	void Start()
	{
        KinectManager manager = KinectManager.Instance;
		
		if(manager && manager.IsInitialized())
		{
			KinectInterop.SensorData sensorData = manager.GetSensorData();
			if(foregroundCamera != null && sensorData != null && sensorData.sensorInterface != null)
			{
				foregroundCamera.fieldOfView = sensorData.colorCameraFOV; 

				initialCameraPos = foregroundCamera.transform.position;
				Vector3 fgCameraPos = initialCameraPos;

                // adjust x
				fgCameraPos.x += sensorData.faceOverlayOffset + adjustedCameraOffset;

                fgCameraPos.y += sensorData.faceOverlayOffset + adjustedCameraOffset_Y;

                fgCameraPos.z += sensorData.faceOverlayOffset + adjustedCameraOffset_Z;

                foregroundCamera.transform.position = fgCameraPos;

				currentCameraOffset = adjustedCameraOffset;

                currentCameraOffset_Y = adjustedCameraOffset_Y;

                currentCameraOffset_Z = adjustedCameraOffset_Z;

            }
		}
	}

	void Update()
	{
		KinectManager manager = KinectManager.Instance;
		if(manager && manager.IsInitialized())
		{
            if (backgroundImage && (backgroundImage.texture == null) )
            {
                backgroundImage.texture = manager.GetUsersClrTex();
			}

            // adjust x y z
            if (currentCameraOffset != adjustedCameraOffset || currentCameraOffset_Y != adjustedCameraOffset_Y || currentCameraOffset_Z != adjustedCameraOffset_Z)
            {
                // update the camera automatically, according to the current sensor height and angle
                KinectInterop.SensorData sensorData = manager.GetSensorData();

                if (foregroundCamera != null && sensorData != null)
                {
                    Vector3 fgCameraPos = initialCameraPos;

                    fgCameraPos.x += sensorData.faceOverlayOffset + adjustedCameraOffset;
                    fgCameraPos.y += sensorData.faceOverlayOffset + adjustedCameraOffset_Y;
                    fgCameraPos.z += sensorData.faceOverlayOffset + adjustedCameraOffset_Z;

                    foregroundCamera.transform.position = fgCameraPos;

                    currentCameraOffset = adjustedCameraOffset;
                    currentCameraOffset_Y = adjustedCameraOffset_Y;
                    currentCameraOffset_Z = adjustedCameraOffset_Z;
                }
            }

        }
	}

    public void FreezeScreen()
    {
        KinectManager manager = KinectManager.Instance;
        if (manager && manager.IsInitialized())
        {
            if (backgroundImage && (backgroundImage.texture == null))
            {
                backgroundImage.texture = manager.GetUsersClrTex();
            }

            Texture2D test_tex = new Texture2D(1920, 1080);

            KinectInterop.SensorData sensorData = manager.GetSensorData();
            if (sensorData != null)
            {
                // get color map data (1920*1080*4)
                color_map_buffer = sensorData.colorImage;

                // convert color to texture
                if (color_map_buffer != null)
                {
                    // apply temp texture for freeze screen
                    for (int i = 0; i < 1920; i++)
                    {
                        for (int j = 0; j < 1080; j++)
                        {
                            test_tex.SetPixel(i, j, new Color(color_map_buffer[(i + 1920 * j) * 4] / 256.0f,
                                color_map_buffer[(i + 1920 * j) * 4 + 1] / 256.0f, color_map_buffer[(i + 1920 * j) * 4 + 2] / 256.0f));
                        }
                    }
                    test_tex.Apply();

                    //print((float)color_map_buffer[860 + 1920 * 540]/256 + ", " + (float)color_map_buffer[860 + 1920 * 540 + 1]/256 + ", " + (float)color_map_buffer[860 + 1920 * 540 + 2]/256);
                    backgroundImage.texture = test_tex;
                }
            }
        }
    }

    public void ContinueScreen()
    {
        backgroundImage.texture = null;
    }

}
