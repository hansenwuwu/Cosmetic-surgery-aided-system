using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPoint : MonoBehaviour {

    public GUIText debugText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("press");
            Debug.Log(Input.mousePosition);
            debugText.GetComponent<GUIText>().text = "Click Mouse Cord: " + Input.mousePosition;
        }

	}
}
