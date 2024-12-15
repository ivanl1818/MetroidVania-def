using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleLight : MonoBehaviour
{
  
    [SerializeField]
    private float minScale, maxScale;
    [SerializeField]
    private bool flameOn;
    [SerializeField]
    private float t;


    private void Start()
    {
        StartCoroutine(FlameScale());
    }

    IEnumerator FlameScale()
    {
        while (flameOn == true)
        {
            float scale = Random.Range(minScale, maxScale);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return new WaitForSeconds(t);
        }
    }
}

