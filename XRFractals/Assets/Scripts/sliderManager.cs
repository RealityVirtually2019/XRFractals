using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderManager : MonoBehaviour {

    public int whichProperty = 0;

    public string[] propertyNames;
    public Text propertyUIText;

	void Start () {
		
	}
	
	void Update () {
       
    }

    public void prev()
    {
      
        if (whichProperty > 0)
        {
            whichProperty--;
        }
        else whichProperty = propertyNames.Length - 1;

        propertyUIText.text = propertyNames[whichProperty];
    }


    public void next()  
    {
        if (whichProperty < propertyNames.Length - 1)
        {
            whichProperty++;
        } 
        else whichProperty = 0; 

        propertyUIText.text = propertyNames[whichProperty];
    }
}
