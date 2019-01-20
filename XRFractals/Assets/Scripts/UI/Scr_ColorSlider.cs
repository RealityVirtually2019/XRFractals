using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Scr_ColorSlider : MonoBehaviour {

    private Slider slider;
    public LineRenderer fractalLineRend;

    [SerializeField] private float speed;

    float alpha = 1.0f;


    public void OnSliderValueChange()
    {
        Gradient gradient = new Gradient();

        Color randomColor1 = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        Color randomColor2 = Random.ColorHSV(0f, 0.6f, 0.8f, 1f, 0.5f, 1f);

        //set the gradient keys 
        gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(randomColor1, 0.0f), new GradientColorKey(randomColor2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) });

        //fractalLineRend.colorGradient = gradient;

        StartCoroutine(FadeColorGradient(gradient));
    }

    //fading the color gradient
    IEnumerator FadeColorGradient(Gradient gradient)
    {
        Color color = Color.Lerp(fractalLineRend.colorGradient.Evaluate(Time.deltaTime * speed), gradient.Evaluate(Time.deltaTime * speed), Time.deltaTime * speed);

        Gradient gradientNew = new Gradient();

        gradientNew.SetKeys(new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) },
           new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) });

        fractalLineRend.colorGradient = gradient;

        yield return null;
    }
}
