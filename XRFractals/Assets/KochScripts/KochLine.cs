using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

[RequireComponent(typeof(LineRenderer))]
public class KochLine : KochGenerator {
    LineRenderer _lineRenderer;
    [Range(0,1)]
    public float _lerpAmount;
    Vector3[] _lerpPostion;
    public float _generateMultiplier;
 public Slider slider1;
    [Header("Audio")]
    public AudioPeer _audioPeer;
    public int _audioBand;
    private AudioPeer masterAudioPeer;

    public AudioMixerSnapshot currentFractalSnapshot;
    public AudioMixerGroup colliderGroup;

	// Use this for initialization
	void Start () {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        //masterAudioPeer = 

    }

    private void updateLine()
    {
        UpdateFractal();
        _lineRenderer.positionCount = _position.Length;
        _lineRenderer.SetPositions(_position);
        _lerpPostion = new Vector3[_position.Length];
        

        DrawFractal();
    }

    private void DrawFractal()
    {
        if (_generationCount != 0)
        {
            for (int i = 0; i < _position.Length; i++)
            {
                //OLD Call adding audio now
                //_lerpPostion[i] = Vector3.Lerp(_position[i], _targetPosition[i], _lerpAmount);
                //Debug.Log(_audioPeer._audioBandBuffer[0]);
                //_lerpPostion[i] = Vector3.Lerp(_position[i], _targetPosition[i], _audioPeer._audioBandBuffer[_audioBand]);
                _lerpPostion[i] = Vector3.Lerp(_position[i], _targetPosition[i], 1.0f);
            }
            if (_useBezierCurves)
            {
                _bezierPosition = BezierCurve(_lerpPostion, _bezierVertexCount);
                _lineRenderer.positionCount = _bezierPosition.Length;
                _lineRenderer.SetPositions(_bezierPosition);
            }
            else
            {
                _lineRenderer.positionCount = _lerpPostion.Length;
                _lineRenderer.SetPositions(_lerpPostion);
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
       
        if(Input.GetKeyDown(KeyCode.U))
        {
            updateLine();
        }
    }
 public void SliderStartGenChange(float myValue)
    {

        Debug.Log("HELLO!!!!!");

        _startGen = new StartGen[UnityEngine.Random.Range(1,5)];

         for (int i = 0; i < _startGen.Length; i++)
        {
            // TODO : Is there a more efficient way to do this? 
            _startGen[i] = new StartGen();
            // UnityEngine.Random boolean value via 
            //https://gamedev.stackexchange.com/questions/110332/is-there-a-random-command-for-boolean-variables-in-unity-c
            Debug.Log(UnityEngine.Random.value > 0.5f);
            _startGen[i].outwards = (UnityEngine.Random.value > 0.5f);
;
            _startGen[i].scale = UnityEngine.Random.Range(1,8);
        }
        updateLine();

         if (myValue == 0.0) {
            colliderGroup.audioMixer.SetFloat("GrainVolume", 0.0f);


        } else {
            colliderGroup.audioMixer.SetFloat("GrainVolume", 1.0f);

        }
        float newPitch = Convert.ToSingle(UnityEngine.Random.Range(3, 20) * 0.1);


        colliderGroup.audioMixer.SetFloat("GrainPitch", newPitch);


    }

    public void SliderSizeChange(float newSizeValue)
    {        
        _initiatorSize = (10* newSizeValue);
        Debug.Log("Size Changing to : " + _initiatorSize);
        updateLine();
        if (newSizeValue == 0.0) {
            colliderGroup.audioMixer.SetFloat("BassVolume", 0.0f);


        }
        else
        {
            colliderGroup.audioMixer.SetFloat("BassVolume", 1.0f);

        }

        if (newSizeValue > .7) {


        colliderGroup.audioMixer.SetFloat("BassPitch", 1.24f);

        } else if (newSizeValue > .4) {
        colliderGroup.audioMixer.SetFloat("BassPitch", 1.12f);



        }else {


                    colliderGroup.audioMixer.SetFloat("BassPitch", 1.0f);

        }

    }

     public void SliderLerpChange(float newLerpValue)
    {        
        _lerpAmount = newLerpValue;
        Debug.Log("Lerp Changing to : " + _lerpAmount);
        updateLine();

         if (newLerpValue == 0.0) {
            colliderGroup.audioMixer.SetFloat("SynthVolume", 0.0f);


        }
        else
        {
            colliderGroup.audioMixer.SetFloat("SynthVolume", 1.0f);

        }
    
               if (newLerpValue > .7) {


                    colliderGroup.audioMixer.SetFloat("SynthPitch", 1.0f);

        } else if (newLerpValue > .4) {
        colliderGroup.audioMixer.SetFloat("SynthPitch", 1.12f);



        }else {

        colliderGroup.audioMixer.SetFloat("SynthPitch", 1.24f);


        }

    }

      public void SliderGenerateMultiplierChange(float newMultiplierValue)
      {     
           //UnityEngine.Random boolean value
           _useBezierCurves = UnityEngine.Random.value > 0.5f;
           Debug.Log("SliderGenerateMultiplierChange Changing to : " + _useBezierCurves);
           updateLine();

if (newMultiplierValue == 0.0) {
            colliderGroup.audioMixer.SetFloat("ArpVolume", 0.0f);


        }

                 if (newMultiplierValue >.5) {
        colliderGroup.audioMixer.SetFloat("ArpVolume", 1.0f);

    }
      }
}
