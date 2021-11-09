using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioFX;

    public float forwardSpeed = 8f;
    public float moveSpeed = 8f;
    public float jumpForce = 300f;

    private Vector3 moveDirection = Vector3.zero;
    private float movementZ;

    private bool canMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioFX = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Invoke(nameof(ActivateMoveMent), 2f);
    }

    private void FixedUpdate()
    {
        CharacterMove();
        CharacterJump();
    }

    void CharacterMove()
    {
        if (!canMove)
        {
            return;
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            movementZ = -moveSpeed;//zэто будет влево и вправо перса повернули из за сцен

        }else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            movementZ = moveSpeed;
        }
        else
        {
            movementZ = 0;
        }

        moveDirection = new Vector3(forwardSpeed, rb.velocity.y, movementZ);//прямо пойдет сам!лево право мы ,z-потому что мы его повернули из за сцен
        rb.velocity = moveDirection;
    }

    void CharacterJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rb.velocity.y == 0f)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    void ActivateMoveMent()
    {
        canMove = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "KillZone") {
            if (!canMove)
            {
                return;
            }

            rb.velocity = Vector3.zero;

            canMove = false;
            audioFX.Play();

            Invoke(nameof(RestartGame), 2f);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
