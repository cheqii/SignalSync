using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class Signal : MonoBehaviour
    {
        #region -Declared Variables-

        protected Rigidbody2D rb;
        
        [SerializeField] protected float speed;
        [SerializeField] protected float jumpForce;

        [SerializeField] protected bool onGround;

        public bool OnGround
        {
            get => onGround;
            set => onGround = value;
        }

            [SerializeField] protected Sprite colorSprite;

        public Sprite ColorSprite
        {
            get => colorSprite;
            set => colorSprite = value;
        }
        
        [SerializeField] protected Sprite whiteSprite;
        
        public Sprite WhiteSprite
        {
            get => whiteSprite;
            set => whiteSprite = value;
        }
        
        [Header("Color Pick")]
        [SerializeField] protected Color32 inFieldColor;

        public Color32 InfieldColor
        {
            get => inFieldColor;
            set => inFieldColor = value;
        }
        [SerializeField] protected Color32 controlColor;

        public Color32 ControlColor
        {
            get => controlColor;
            set => controlColor = value;
        }
        [SerializeField] protected Color32 normalColor;

        public Color32 NormalColor
        {
            get => normalColor;
            set => normalColor = value;
        }
        
        #endregion

        public virtual void Move()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            Vector3 movement = new Vector3(horizontalInput, 0f, 0f);

            if (horizontalInput < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            
            if (horizontalInput > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }

            transform.Translate(movement * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (onGround)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    onGround = false;
                }
            }

            transform.rotation = Quaternion.identity;
        }
        
        
    }
}
