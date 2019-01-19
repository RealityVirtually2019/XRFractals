using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KochGenerator : MonoBehaviour {
    protected enum _axis
    {
        XAxis,
        YAxis,
        ZAxis
    }

    [SerializeField]
    protected _axis axis = new _axis();

    protected enum _initititor
    {
        Triangle,
        Square,
        Pentagon,
        Hexagon,
        Heptagon,
        Octogon
    }

    //Struct for holding info about the edges of the polygons
    public struct LineSegment
    {
        public Vector3 StartPostion { get; set; }
        public Vector3 EndPosition { get; set; }
        public Vector3 Direction { get; set; }
        public float Length { get; set; }
    }

    [SerializeField]
    protected _initititor intititor = new _initititor();

    [SerializeField]
    protected AnimationCurve _generator;
    protected Keyframe[] _keys;

    protected int _generationCount;

    protected int _initiatorPointAmount;    //How many sides per shape
    private Vector3[] _initiatorPoint;      //Sides of the shape
    private Vector3 _rotateVector;
    private Vector3 _rotateAxis;
    private float _initalRotation;

    [SerializeField]
    protected float _initiatorSize;         //Size of each side

    protected Vector3[] _position;
    protected Vector3[] _targetPosition;
    private List<LineSegment> _lineSegment;

    private void Awake()
    {
        GetIntiatorPoints();
        //Assign Lists & Arrays
        _position = new Vector3[_initiatorPointAmount+1];
        _targetPosition = new Vector3[_initiatorPointAmount + 1];
        _lineSegment = new List<LineSegment>();
        _keys = _generator.keys;

        _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;
        //Fills the array with the points of the object, updating rotation per point
        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            _position[i] = _rotateVector * _initiatorSize;
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;
        }
        _position[_initiatorPointAmount] = _position[0];
        _targetPosition = _position;
    }

    protected void KochGenerate(Vector3[] positions, bool outwards, float generatorMultiplier)
    {
        //Creating the Line Segments
        _lineSegment.Clear();
        for (int i = 0; i < positions.Length - 1; i++)
        {
            LineSegment line = new LineSegment();
            line.StartPostion = positions[i];
            if(i==positions.Length-1)
            {
                line.EndPosition = positions[0];
            }
            else
            {
                line.EndPosition = positions[i + 1];
            }
            line.Direction = (line.EndPosition - line.StartPostion).normalized;
            line.Length = Vector3.Distance(line.EndPosition, line.StartPostion);
            _lineSegment.Add(line);
        }
        // add line segment points to the a point array
        List<Vector3> newPos = new List<Vector3>();
        List<Vector3> targetPos = new List<Vector3>();

        //Add positions of all LineSegments to the lists
        for(int i= 0; i < _lineSegment.Count; i++)
        {
            newPos.Add(_lineSegment[i].StartPostion);
            targetPos.Add(_lineSegment[i].StartPostion);

            //iterates through all keys on the line
            for(int j = 1; j<_keys.Length -1; j++)
            {
                
                float moveAmount = _lineSegment[i].Length * _keys[j].time;
                float heightAmount = (_lineSegment[i].Length * _keys[j].value * generatorMultiplier);
                Vector3 movePos = _lineSegment[i].StartPostion + (_lineSegment[i].Direction * moveAmount);
                Vector3 dir;
                if(outwards)
                {
                    dir = Quaternion.AngleAxis(-90, _rotateAxis) * _lineSegment[i].Direction;
                }
                else
                {
                    dir = Quaternion.AngleAxis(90, _rotateAxis) * _lineSegment[i].Direction;
                }
                newPos.Add(movePos);
                targetPos.Add(movePos + (dir * heightAmount));
            }
        }
        //Add the end positions
        newPos.Add(_lineSegment[0].StartPostion);
        targetPos.Add(_lineSegment[0].StartPostion);

        _position = new Vector3[newPos.Count];
        _targetPosition = new Vector3[targetPos.Count];
        _position = newPos.ToArray();
        _targetPosition = targetPos.ToArray();

        _generationCount++;
    }

    private void OnDrawGizmos()
    {
        GetIntiatorPoints();
        _initiatorPoint = new Vector3[_initiatorPointAmount];

        _rotateVector = Quaternion.AngleAxis(_initalRotation, _rotateAxis) * _rotateVector;
        
        //Fills the array with the points of the object, updating rotation per point
        for (int i =0; i<_initiatorPointAmount; i++)
        {
            _initiatorPoint[i] = _rotateVector * _initiatorSize;
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector; 
        }
        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            Gizmos.color = Color.white;

            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.matrix = rotationMatrix;
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
                _initalRotation = 0;
                break;

            case _initititor.Square:
                _initiatorPointAmount = 4;
                _initalRotation = 45;
                break;

            case _initititor.Pentagon:
                _initiatorPointAmount = 5;
                _initalRotation = 36;
                break;

            case _initititor.Hexagon:
                _initiatorPointAmount = 6;
                _initalRotation = 30;
                break;

            case _initititor.Heptagon:
                _initiatorPointAmount = 7;
                _initalRotation = 7.1428f;
                break;

            case _initititor.Octogon:
                _initiatorPointAmount = 8;
                _initalRotation = 22.5f;
                break;
            default:
                break;

        };
        
        switch(axis)
        {
            case _axis.XAxis:
                _rotateVector = new Vector3(1,0,0);
                _rotateAxis = new Vector3(0,0,1);
                break;

            case _axis.YAxis:
                _rotateVector = new Vector3(0,1,0);
                _rotateAxis = new Vector3(1,0,0);
                break;

            case _axis.ZAxis:
                _rotateVector = new Vector3(0,0,1);
                _rotateAxis = new Vector3(0,1,0);
                break;

            default:
                _rotateVector = new Vector3(0, 0, 1);
                _rotateAxis = new Vector3(0, 1, 0);
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
