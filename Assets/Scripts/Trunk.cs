using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    [Header("Object Config.")]
    public Transform objectToMove;
    public Transform startPos;
    public Transform endPos;
    public float moveSpeed;
    public bool isMove;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            objectToMove.position = Vector3.Lerp(startPos.position, endPos.position, moveSpeed * Time.deltaTime);

            if (objectToMove.position.y >= endPos.position.y)
            {
                isMove = !isMove;
                StartCoroutine("moveObject");
            }
        }
    }

    IEnumerator moveObject()
    {
        yield return new WaitForSeconds(3f);
        isMove = !isMove;
        //objectToMove.position = startPos.position;
        //yield return new WaitForSeconds(0.5f);
        //isMove = !isMove;
    }
}
