using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCopyScript : MonoBehaviour {

    public GameObject copyGameObject;
    public GameObject superGameObject;
    private GameObject childGameObject;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClick()
    {
        childGameObject = Instantiate(copyGameObject);
        childGameObject.transform.parent = superGameObject.transform;
        childGameObject.transform.localPosition = Vector3.zero;

        
    }

}
