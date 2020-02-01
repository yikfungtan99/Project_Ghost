using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] MirrorGhost mirror;

    private bool deathDebug = false;

    private Rigidbody2D rBody;
    private bool facingRight = true;
    public bool trappedByMirrorGhost = false;
    public bool collisionTrigger = false;
    public bool isPlayerDead = false;
    public bool pauseOnTriggerStay = false;

    public Transform dragPlayerToTarget;
    private Vector2 targetPosition;
    public float dragSpeed = 1f;
    private float dragStep = 0f;

    public float moveSpeed = 5f;
    float xMove = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        targetPosition = new Vector2(dragPlayerToTarget.position.x, transform.position.y);
    }

    private void FixedUpdate()
    {
        if(trappedByMirrorGhost == true)
        {
            MovementManager(true);
        }
        else if(isPlayerDead == true)
        {
            MovementManager(true);
        }
        else
        {
            MovementManager(false);
        }
    }

    private void Update()
    {
        //checking player death (debug)
        if(isPlayerDead == true)
        {
            if(deathDebug == false)
            {
                deathDebug = true;
                Debug.Log("Player is Dead");
            }
        }

        //simulating the mirror dragging the player in LIVE (disabled movement when trapped, but can be negated by red cloth)
        if(trappedByMirrorGhost == true && mirror.isCoveredByCloth == false)
        {
            dragStep = dragSpeed * Time.deltaTime;
            if(transform.position.x != targetPosition.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, dragStep);
            }
            else
            {
                
            }
        }

        
    }

    private void MovementManager(bool isMovementStopped)
    {
        if(isMovementStopped == false)
        {
            xMove = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            xMove = 0;
        }

        rBody.velocity = new Vector2(xMove * moveSpeed, rBody.velocity.y);

        if (xMove > 0 && !facingRight)
        {
            FlipPlayerSprite();
        }
        else if (xMove < 0 && facingRight)
        {
            FlipPlayerSprite();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(mirror.isCoveredByCloth == false && collision.gameObject.name == "MirrorGhost")
        {
            Debug.Log(collision.gameObject.name + " sequence initiated.");
            trappedByMirrorGhost = true;
        }
        else if(mirror.isCoveredByCloth == true && collision.gameObject.name == "MirrorGhost")
        {
            Debug.Log("Due to the cloth, " + collision.gameObject.name + "'s sequence was not initiated.");
        }
        pauseOnTriggerStay = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (mirror.isCoveredByCloth == false && collision.gameObject.name == "MirrorGhost")
        {
            if(pauseOnTriggerStay == false)
            {
                Debug.Log(collision.gameObject.name + " sequence initiated.");
                trappedByMirrorGhost = true;
                pauseOnTriggerStay = true;
            }
        }
    }
    
    void FlipPlayerSprite()
    {
        //turn left into right, or right into left
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 newLocalScale = transform.localScale;
        newLocalScale.x = newLocalScale.x * -1;
        transform.localScale = newLocalScale;
    }

}
