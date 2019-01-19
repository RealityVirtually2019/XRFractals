using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KochController : KochLine {

    KochGenerator _KochGenerator;
    KochLine _KochLine;

    public int generations;
    

    // Use this for initialization
    void Start()
    {
        _KochGenerator = new KochGenerator();
        _KochLine = new KochLine();

        FractalParams fparams = new FractalParams();
        fparams.initShape = _initititor.Triangle;
        //fparams.gens[0];

        
        //FractalParams fractalParam()
        //_KochGenerator._startGen

    }

    // Update is called once per frame
    void Update() {

    }

    public void GenerateFractal()
    {

    }
}
