using System;
using Player;
using UnityEngine;

public class Drone : ReceiverObject
{
    [SerializeField] private bool isHolding;
    public bool isFlying;
    public bool IsHolding
    {
        get => isHolding;
        set => isHolding = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pocket = FindObjectOfType<PocketSignal>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")
            || other.gameObject.CompareTag("PocketSignal")
            || other.gameObject.CompareTag("Receiver"))
        {
            onGround = true;
            isFlying = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")
            || other.gameObject.CompareTag("PocketSignal")
            || other.gameObject.CompareTag("Receiver"))
        {
            onGround = false;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SignalField"))
        {
            isSelected = false;
            isHolding = false;
            // pocket.pocketControl = true;
                
            if (pocket.pocketControl) // left receiver in field is exit from fields
            {
                // GameController.Instance.isPocket = true;
                GameController.Instance.isPocketDelay = true;
                StartCoroutine(GameController.Instance.PlayerControllerDelay(1.5f));
                if (!GameController.Instance.isReceiver) pocket.GetComponent<SpriteRenderer>().color = controlColor;
            }

            if (!isSelected)
            {
                gameObject.GetComponent<SpriteRenderer>().color = normalColor;
                gameObject.GetComponent<SpriteRenderer>().sprite = whiteSprite;
            }

            if(!isSelected && !pocket.pocketControl)
            {
                // gameObject.GetComponent<SpriteRenderer>().color = inFieldColor;
                // gameObject.GetComponent<SpriteRenderer>().sprite = whiteSprite;
                pocket.pocketControl = true;
                // GameController.Instance.isPocket = true;
                GameController.Instance.isPocketDelay = true;
                StartCoroutine(GameController.Instance.PlayerControllerDelay(1.5f));
                pocket.GetComponent<SpriteRenderer>().color = controlColor;
            }
                
        }
    }

    public override void Move()
    {
        if (GameController.Instance.isReceiver && isSelected)
        {
            rb.gravityScale = 0;
            
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            Vector3 movement = new Vector3(horizontalInput, 0f, 0f);

            if (horizontalInput < 0) gameObject.GetComponent<SpriteRenderer>().flipX = true;

            if (horizontalInput > 0) gameObject.GetComponent<SpriteRenderer>().flipX = false;

            transform.Translate(movement * speed * Time.deltaTime);
            
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, 1 * speed * Time.deltaTime, 0f);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(0, -1 * speed * Time.deltaTime, 0f);
            }
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
}
