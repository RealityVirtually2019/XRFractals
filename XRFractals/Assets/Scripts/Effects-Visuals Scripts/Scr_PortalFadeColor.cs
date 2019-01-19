using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PortalFadeColor : MonoBehaviour {
    [Header("Particle References")]
    public ParticleSystem edgeParticle;
    public ParticleSystem centerParticle;

    [SerializeField] private bool loopFade;
    [SerializeField] private float loopTime;

    [Header("Original Colors")]
    [SerializeField] private Color originalParticleEdgeColor;
    [SerializeField] private Color originalParticleCenterColor;

    Color edgeParticleClear;
    Color centerParticleClear;

    void Start ()
    {
        edgeParticleClear = new Color(edgeParticle.main.startColor.color.a, edgeParticle.main.startColor.color.g, edgeParticle.main.startColor.color.b, 0);
        centerParticleClear = new Color(centerParticle.main.startColor.color.a, centerParticle.main.startColor.color.g, centerParticle.main.startColor.color.b, 0);

        originalParticleEdgeColor = edgeParticle.startColor;
        originalParticleCenterColor = centerParticle.startColor;
	}
	

	void Update ()
    {
        if (loopFade)
        {
            StartCoroutine(StartFade());
        }

        if (Input.GetKeyDown(KeyCode.A)) StartRunningPortalParticle();
        if (Input.GetKeyDown(KeyCode.S)) StopRunningPortalParticle();
    }


    public void StartRunningPortalParticle()
    {
        loopFade = true;
        edgeParticle.Play();
        centerParticle.Play();
    }

    //call this function this when you want the particle to stop
    public void StopRunningPortalParticle()
    {
        loopFade = false;
        edgeParticle.Stop();
        centerParticle.Stop();
    }

    IEnumerator StartFade()
    {
        float lerp = Mathf.PingPong(Time.time, loopTime) / loopTime;
        edgeParticle.startColor = Color.Lerp(edgeParticle.main.startColor.color, edgeParticleClear, lerp);
        centerParticle.startColor = Color.Lerp(centerParticle.main.startColor.color, centerParticleClear, lerp);

        yield return new WaitForSeconds(loopTime);

        edgeParticle.startColor = Color.Lerp(edgeParticle.main.startColor.color, originalParticleEdgeColor, lerp);
        centerParticle.startColor = Color.Lerp(centerParticle.main.startColor.color,originalParticleCenterColor, lerp);
    }
}
