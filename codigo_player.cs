using System.Collections; // funcao de importacao
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour // orientado a objetos
{
    public float move_speed = 5f; // variavel publica, ou seja, aparece no inspector
    public float jump_force = 5f;
    private Rigidbody2D rigd;
    public Animator anim;
    public bool isGround;

    void Start()
    {
        rigd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        // Attack();
    }

    void Move()
    {
        float tecla = Input.GetAxis("Horizontal");
        rigd.velocity = new Vector2(tecla * move_speed, 0f);

        if (tecla < 0 && isGround)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
            anim.SetInteger("transition", 1);
        }
        else if (tecla > 0 && isGround)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
            anim.SetInteger("transition", 1);
        }
        else if(tecla == 0 && isGround)
        {
            anim.SetInteger("transition", 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigd.AddForce(Vector2.up * jump_force, ForceMode2D.Impulse);
            isGround = false;
            anim.SetInteger("Transition", 2);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            Debug.Log("Estou no chão");
            isGround = true;
        }

        if (collision.gameObject.tag == "cubo")
        {
            Debug.Log("Toquei no chão");
            isGround = true;
        }
    }
}
