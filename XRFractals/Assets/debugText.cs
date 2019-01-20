using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class debugText : MonoBehaviour {

    Text debugTxt;
	void Start () {
        debugTxt = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
        //debugTxt.text = "x : " + arpoint.x + "\n" + "y : " + arpoint.y;
        debugTxt.text = "x : " + "xxx" + "\n" + "y : " + "yyy";
    }
}
