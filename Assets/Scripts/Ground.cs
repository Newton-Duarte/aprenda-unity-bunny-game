using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private GameController _gameController;

    [SerializeField]
    private Rigidbody2D rb;

    private bool isInstantiated;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        rb.velocity = new Vector2(_gameController.groundSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < _gameController.destroyDistance)
        {
            Destroy(gameObject);
        }

        if (!isInstantiated && transform.position.x <= 0)
        {
            int rand = Random.Range(0, 100);
            int idPrefab;

            if (rand <= 25) { idPrefab = 0; }
            else if (rand > 25 && rand <= 50){ idPrefab = 1; }
            else if (rand > 50 && rand <= 75) { idPrefab = 2; }
            else { idPrefab = 3; }

            isInstantiated = true;
            GameObject temp = Instantiate(_gameController.groundPrefab[idPrefab]);
            temp.transform.position = new Vector3(transform.position.x + _gameController.groundSize, transform.position.y, 0);
        }
    }
}
