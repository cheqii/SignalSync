using System;
using Player;
using UnityEngine;

public class Drone : ReceiverObject
{
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
        if (other.gameObject.CompareTag("Ground"))
        {
            if (rb.bodyType == RigidbodyType2D.Static)
            {
                Debug.Log("Ground kub");
                transform.position += new Vector3(0f, 1f, 0f);
            }
        }
    }

    public override void Move()
    {
        if (GameController.Instance.isReceiver && isSelected)
        {
            rb.bodyType = RigidbodyType2D.Static;
            
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
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}