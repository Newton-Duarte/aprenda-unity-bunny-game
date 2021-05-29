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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("moveObject");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //if (objectToMove.transform.position.y >= endPos.position.y)
        //{
        //    objectToMove.position = startPos.position;
        //}
    }

    IEnumerator moveObject()
    {
        yield return new WaitForSeconds(1f);
        objectToMove.position = Vector3.Lerp(startPos.position, endPos.position, moveSpeed * Time.deltaTime);
        yield return new WaitForSeconds(2f);
        objectToMove.position = startPos.position;
        StartCoroutine("moveObject");
    }
}
