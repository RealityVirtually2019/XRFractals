using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genmotionscript : MonoBehaviour {
	
	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;
	public KeyCode upKey = KeyCode.W;
	public KeyCode downKey = KeyCode.S;
	
	public KeyCode rotLeft = KeyCode.Q;
	public KeyCode rotRight = KeyCode.E;

	// very hacky copy paste but can be cleaned up later!
	
	void Update () {
		if (Input.GetKeyDown(leftKey))
		{
			Vector3 position = this.transform.position;
			position.x--;
			this.transform.position = position;
		}
		if (Input.GetKeyDown(rightKey))
		{
			Vector3 position = this.transform.position;
			position.x++;
			this.transform.position = position;
		}
		if (Input.GetKeyDown(upKey))
		{
			Vector3 position = this.transform.position;
			position.z++;
			this.transform.position = position;
		}
		if (Input.GetKeyDown(downKey))
		{
			Vector3 position = this.transform.position;
			position.z--;
			this.transform.position = position;
		}

		if (Input.GetKeyDown(rotLeft)){
			transform.Rotate(Vector3.up * 10);
		}
		if (Input.GetKeyDown(rotRight)){
			transform.Rotate(-Vector3.up * 10);
		}
	}
}
