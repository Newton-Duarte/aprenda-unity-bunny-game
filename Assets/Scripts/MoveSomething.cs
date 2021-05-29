using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSomething : MonoBehaviour
{
    public float moveSpeed;
    public float speedY;
    public float startPosX;
    public float startPosY;
    public float endPosY;
    public bool isMove;
    public float waitTime = 2f;

    private void OnBecameVisible()
    {
        StartCoroutine("startAnimation");
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            StopCoroutine("moveControl");
            transform.Translate(new Vector3(0, speedY * moveSpeed * Time.deltaTime, 0));
        }

        if (transform.localPosition.y > endPosY)
        {
            isMove = false;
            StartCoroutine("moveControl");
        }
    }

    void resetPosition()
    {
        transform.localPosition = new Vector3(startPosX, startPosY, 0);
    }

    IEnumerator startAnimation()
    {
        yield return new WaitForSeconds(1f);
        isMove = true;
    }

    IEnumerator moveControl()
    {
        yield return new WaitForSeconds(waitTime);
        resetPosition();
        yield return new WaitForSeconds(waitTime);
        isMove = !isMove;
    }
}
