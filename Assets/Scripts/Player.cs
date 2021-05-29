using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameController _gameController;

    private Rigidbody2D playerRb;
    private Animator    playerAnim;
    private SpriteRenderer    playerSr;

    public Transform    groundCheck;
    public LayerMask    whatIsGround;

    public float startPos = -6.22f;

    public float        speed = 5f;
    public int          extrasJumps;
    public int          extraJump;
    public float        jumpForce = 500f;

    private bool        isGrounded;
    private int         speedX;
    private float       speedY;

    private bool        isLookingLeft;

    [Header("Projectile Config.")]
    public Transform    weapon;
    public GameObject   projectilePrefab;
    private bool        isFiring;
    public float        shotDelay;
    public float        shotSpeed;

    [Header("Projectile 2 Config.")]
    public GameObject projectile2Prefab;
    public float projectile2HorizontalForce;
    public float projectile2VerticalForce;

    [Header("FX Config.")]
    public AudioSource fxSource;
    public AudioClip fxJump;
    public AudioClip fxHit;
    public AudioClip fxCarrot;

    public bool moveFreely;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerSr= GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (isLookingLeft && horizontal > 0 && moveFreely)
        {
            flipWithScale();
            //flipWithSpriteRenderer();
        }

        if (!isLookingLeft && horizontal < 0 && moveFreely)
        {
            flipWithScale();
            //flipWithSpriteRenderer();
        }

        speedX = horizontal != 0 ? 1 : 0;
        speedY = playerRb.velocity.y;
        
        //playerRb.velocity = new Vector2(horizontal * speed, speedY);

        if (isGrounded)
        {
            extraJump = extrasJumps;
        }

        if (Input.GetButtonDown("Jump") && extraJump> 0)
        {
            jump();
            extraJump--;
        } 
        else if (Input.GetButtonDown("Jump") && isGrounded && extraJump == 0)
        {
            jump();
        }

        if (Input.GetButtonDown("Fire1") && !isFiring && moveFreely)
        {
            shot();
        }

        if (Input.GetButtonDown("Fire2") && !isFiring && moveFreely)
        {
            shot2();
        }
    }

    private void LateUpdate()
    {
        playerAnim.SetBool("isGrounded", isGrounded);
        playerAnim.SetInteger("speedX", speedX);
        playerAnim.SetFloat("speedY", speedY);

        //if (transform.position.x < startPos)
        //{
        //    transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        //}
    }

    void jump()
    {
        playerRb.AddForce(new Vector2(0, jumpForce));
        fxSource.PlayOneShot(fxJump);
    }

    void flipWithScale()
    {
        isLookingLeft = !isLookingLeft;

        float scaleX = transform.localScale.x;
        scaleX *= -1; // invert the signal 1 : -1 or -1 : 1
        shotSpeed *= -1;
        projectile2HorizontalForce *= -1;

        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

    void flipWithSpriteRenderer()
    {
        isLookingLeft = !isLookingLeft;

        playerSr.flipX = isLookingLeft;
    }

    void shot()
    {
        isFiring = true;

        StartCoroutine("shotControl");

        GameObject temp = Instantiate(projectilePrefab);
        temp.transform.position = weapon.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(shotSpeed, 0);

        Destroy(temp, 2f);
    }

    void shot2()
    {
        isFiring = true;

        StartCoroutine("shotControl");

        GameObject temp = Instantiate(projectile2Prefab);
        temp.transform.position = weapon.position;
        temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(projectile2HorizontalForce, projectile2VerticalForce));

        Destroy(temp, 2f);
    }

    IEnumerator shotControl()
    {
        yield return new WaitForSeconds(shotDelay);
        isFiring = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "collectable":
                CollectableId collectable = collision.gameObject.GetComponent<CollectableId>();

                switch (collectable.name)
                {
                    case "carrot":
                        _gameController.setCarrot(collectable.quantity);
                        fxSource.PlayOneShot(fxCarrot);
                        break;

                    case "egg":
                        print("Egg taken!");
                        break;
                }

                Destroy(collision.gameObject);
                break;
            case "obstacle":
                fxSource.PlayOneShot(fxHit);
                playerAnim.SetTrigger("hit");
                _gameController.gameOver();
                break;
        }
    }
}
