using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffset : MonoBehaviour
{
    GameController _gameController;
    private MeshRenderer mRenderer;
    private Material currentMaterial;

    private float offset;
    public float offsetIncrement;
    public float speed;
    public string sortingLayer;
    public int orderInLayer;

    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

        mRenderer = GetComponent<MeshRenderer>();
        currentMaterial = mRenderer.material;

        mRenderer.sortingLayerName = sortingLayer;
        mRenderer.sortingOrder = orderInLayer;
    }

    void FixedUpdate()
    {
        if (_gameController.currentState == gameState.gameplay)
        {
            offset += offsetIncrement;
            currentMaterial.SetTextureOffset("_MainTex", new Vector2(offset * speed, 0));
        }
    }
}
