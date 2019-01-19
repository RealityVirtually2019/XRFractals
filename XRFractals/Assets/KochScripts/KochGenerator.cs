using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KochGenerator : MonoBehaviour {
    protected enum _initititor
    {
        Triangle,
        Square,
        Pentagon,
        Hexagon,
        Heptagon,
        Octogon
    }
    [SerializeField]
    protected _initititor intititor = new _initititor();

    //How many sides per shape
    protected int _initiatorPointAmount;
    private Vector3[] _initiatorPoint;
    private Vector3 _rotateVector;
    private Vector3 _rotateAxis;
    [SerializeField]
    protected float _initiatorSize;

    private void OnDrawGizmos()
    {
        GetIntiatorPoints();
        _initiatorPoint = new Vector3[_initiatorPointAmount];

        _rotateVector = new Vector3(0, 0, 1);
        _rotateAxis = new Vector3(0, 1, 0);

        //Fills the array with the points of the object, updating rotation per point
        for(int i =0; i<_initiatorPointAmount; i++)
        {
            _initiatorPoint[i] = _rotateVector * _initiatorSize;
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector; 
        }
        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            Gizmos.color = Color.white;

            if(i<_initiatorPointAmount - 1)
            {
                //Draw lines from point to point
                Gizmos.DrawLine(_initiatorPoint[i], _initiatorPoint[i + 1]);
            }
            else
            {
                //Unless it is the last line, then go to the begining
                Gizmos.DrawLine(_initiatorPoint[i], _initiatorPoint[0]);
            }
        }
    }

    private void GetIntiatorPoints()
    {
        switch(intititor)
        {
            case _initititor.Triangle:
                _initiatorPointAmount = 3;
                break;

            case _initititor.Square:
                _initiatorPointAmount = 4;
                break;

            case _initititor.Pentagon:
                _initiatorPointAmount = 5;
                break;

            case _initititor.Hexagon:
                _initiatorPointAmount = 6;
                break;

            case _initititor.Heptagon:
                _initiatorPointAmount = 7;
                break;

            case _initititor.Octogon:
                _initiatorPointAmount = 8;
                break;
            default:
                break;

        }

    }
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
