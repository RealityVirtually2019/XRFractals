using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class KochLine : KochGenerator {
    LineRenderer _lineRenderer;
    [Range(0,1)]
    public float _lerpAmount;
    Vector3[] _lerpPostion;
    public float _generateMultiplier;
 public Slider slider1;
    //[Header("Audio")]
    //public AudioPeer _audioPeer;
    //public int _audioBand;

	// Use this for initialization
	void Start () {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;

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
                _lerpPostion[i] = Vector3.Lerp(_position[i], _targetPosition[i], _lerpAmount);
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
 public void ValueChangeCheck(float myValue)
    {

        _startGen = new StartGen[Random.Range(1,5)];

         for (int i = 0; i < _startGen.Length; i++)
        {
            // Is there a more efficient way to do this? 
            _startGen[i] = new StartGen();
            // Random boolean value via 
            //https://gamedev.stackexchange.com/questions/110332/is-there-a-random-command-for-boolean-variables-in-unity-c
            Debug.Log(Random.value > 0.5f);
            _startGen[i].outwards = (Random.value > 0.5f);
;
            _startGen[i].scale = Random.Range(1,8);
        }

        Debug.Log(_startGen.Length +1);

        // int newValue = 10* myValue;
        
        // _initiatorSize = (2* myValue);
        updateLine();

    }

    public void SliderSizeChange(float newSizeValue)
    {        
        _initiatorSize = (10* newSizeValue);
        Debug.Log("Size Changing to : " + _initiatorSize);
        updateLine();

    }

     public void SliderLerpChange(float newLerpValue)
    {        
        _lerpAmount = newLerpValue;
        Debug.Log("Lerp Changing to : " + _lerpAmount);
        updateLine();

    }

      public void SliderGenerateMultiplierChange(float newMultiplierValue)
    {        
        //Random boolean value
        _useBezierCurves = Random.value > 0.5f;
        Debug.Log("SliderGenerateMultiplierChange Changing to : " + _useBezierCurves);
        updateLine();

    }






}
