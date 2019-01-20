using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fractalAnimController : MonoBehaviour {


    public Animator m_animator; 

    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_animator.Play("frac_rotate_anim", 0, 0.0f);

    }
	
	// Update is called once per frame
	void Update () {


    }
}
