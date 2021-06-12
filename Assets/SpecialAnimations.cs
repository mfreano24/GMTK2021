using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAnimations : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    private Material matWhite;
    private Material matDefault;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = spriteRenderer.material;

    }

    public void CallDamageFlash(float length, float fadeIntensity, float fadeSpeed)
    {

        StopCoroutine(DamageFlash(1f,1f,1f));
        StartCoroutine(DamageFlash(length, fadeIntensity, fadeSpeed));


    }

    public IEnumerator DamageFlash(float length, float fadeIntensity, float fadeSpeed)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            float scaleLimit = fadeIntensity;
            float fadeValue = ((Mathf.Sin(Mathf.PI + fadeSpeed * i)) / scaleLimit) + (1 - (1 / scaleLimit));
            spriteRenderer.sharedMaterial.SetFloat("_Fade", fadeValue);
            yield return null;
        }

        float baseValue = spriteRenderer.sharedMaterial.GetFloat("_Fade");
        for (float i = 0; i < 0.2f; i += Time.deltaTime) 
        {
            spriteRenderer.sharedMaterial.SetFloat("_Fade", Mathf.Lerp(baseValue, 1f, i / 0.2f));
            yield return null;
        }
        yield return null;
    }

    public IEnumerator WhiteToFade(float whiteIntensity, float whiteDuration, float fadeDuration)
    {

        spriteRenderer.sharedMaterial.SetFloat("_White", whiteIntensity);
        yield return new WaitForSeconds(whiteDuration);
        spriteRenderer.sharedMaterial.SetFloat("_White", 0);


        for (float i = 0; i < fadeDuration; i += Time.deltaTime)
        {
            float scaleLimit = 2.2f;
            float fadeValue = ((Mathf.Sin(4 * i)) / scaleLimit) + (1 / scaleLimit);
            //float whiteValue = ((Mathf.Sin(i) / 8f) + 0.125f);
            spriteRenderer.sharedMaterial.SetFloat("_Fade", fadeValue);
            //spriteRenderer.sharedMaterial.SetFloat("_White", whiteValue);
            Debug.Log(fadeValue);
            yield return null;
        }
        spriteRenderer.sharedMaterial.SetFloat("_Fade", 1f);
        yield return null;

    }

    public IEnumerator FlashWhitetoFlashFade(float whiteIntensityDivider, float whiteDuration, float fadeDuration)
    {
        for (float i = 0; i < whiteDuration; i += Time.deltaTime)
        {
            float scaleLimit = whiteIntensityDivider;
            float fadeValue = ((Mathf.Sin((Mathf.PI / 2f) + 4 * i)) / scaleLimit) + (1 - (1 / scaleLimit));
            spriteRenderer.sharedMaterial.SetFloat("_White", fadeValue);
            Debug.Log(fadeValue);
            yield return null;
        }


        for (float i = 0; i < fadeDuration; i += Time.deltaTime)
        {
            float scaleLimit = 2.2f;
            float fadeValue = ((Mathf.Sin((Mathf.PI / 2f) + 4 * i)) / scaleLimit) + (1 - (1 / scaleLimit));
            //float whiteValue = ((Mathf.Sin(i) / 8f) + 0.125f);
            spriteRenderer.sharedMaterial.SetFloat("_Fade", fadeValue);
            //spriteRenderer.sharedMaterial.SetFloat("_White", whiteValue);
            Debug.Log(fadeValue);
            yield return null;
        }
        spriteRenderer.sharedMaterial.SetFloat("_Fade", 1f);
        yield return null;
    }
}
