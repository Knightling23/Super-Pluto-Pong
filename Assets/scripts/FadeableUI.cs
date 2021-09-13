using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FadeableUI : MonoBehaviour
{
    // Start is called before the first frame update
    private CanvasGroup canvasGroup;
    private GraphicRaycaster gr;
    private Coroutine fadeCoroutine;


    [SerializeField] private float fadeTime = 0.5f;
    
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        gr = GetComponent<GraphicRaycaster>();
    }


    private void Fade(float targetAlpha, bool instant)
    {
        if (instant)
        {
            canvasGroup.alpha = targetAlpha;
        }

        else
        {
            if(fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
                
            }
            fadeCoroutine = StartCoroutine(FadeRoutine(targetAlpha));// part theat actually fades it

        }



    }//end of fade method


    private IEnumerator FadeRoutine(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;

        for(float timer =0f; timer < fadeTime; timer += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeTime);
            yield return null;
        }
        canvasGroup.alpha = targetAlpha;
        fadeCoroutine = null;

    }


    public void FadeIn(bool instant)
    {

        gr.enabled = true;
        Fade(1f, instant);

    }

    public void FadeOut(bool instant)
    {
        gr.enabled = false;

        Fade(0f,instant);

    }


}
