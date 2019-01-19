using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class KochLine : KochGenerator {
    LineRenderer _lineRenderer;
    [Range(0,1)]
    public float _lerpAmount;
    Vector3[] _lerpPostion;
    public float _generateMultiplier;

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
}
