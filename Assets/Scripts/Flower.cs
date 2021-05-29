using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine("animate");
    }

    IEnumerator animate()
    {
        yield return new WaitForSeconds(0.3f);
        sr.flipX = !sr.flipX;
        StartCoroutine("animate");

    }
}
