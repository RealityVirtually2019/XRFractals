using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StartScreenUI : MonoBehaviour {

    private Animator animator;

	void Start ()
    {
        animator = GetComponentInChildren<Animator>();
	}
    
    public void PlayStartAnim()
    {
        this.gameObject.SetActive(true);
        animator.Play("Anim_Opening");
    }

    public void DisableUI()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        //testing
        //if (Input.GetKey(KeyCode.A))
        //{
        //    PlayStartAnim();
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    DisableUI();
        //}
    }
}
