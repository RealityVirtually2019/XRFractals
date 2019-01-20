using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour {

    public Transform ARParent;
	void Start () {
		
	}
	

	void Update () {
		//tap to create portal


	}

    public void reset()
    {
        ARParent.position = new Vector3(ARParent.position.x, 1000f, ARParent.position.z);
    }
}
