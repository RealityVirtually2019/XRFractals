using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour {

	private Animator animation;

	// Use this for initialization
	void Start () {
		animation = GetComponent<Animator>();
		animation.Play ("cruscarp_skel|swim",0,0.0f);
		// animation.Play ("movement",0,0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
