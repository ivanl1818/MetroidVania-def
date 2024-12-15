using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemieController;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    private Rigidbody2D rb;
    private float horizontal;
    [SerializeField]
    private bool isHit;
    private bool isAttacking;
    private Animator animator;
    private int jumpCount;
    [SerializeField]
    public float damage;
    private LevelManager levelManager;
    [Header("Sfx")]
    [SerializeField]
    private AudioClip espadazosSFX, hitSFX, deathSFX, fireballSFX, jumpSFX, stepsSFX;

    private bool jumping; // Variable declarada

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    void Update()
    {
        if (!isHit && !isAttacking)
        {
            // Movimiento horizontal
            horizontal = Input.GetAxis("Horizontal");
            if (horizontal != 0)
            {
                transform.eulerAngles = (horizontal > 0) ? Vector3.zero : new Vector3(0, 180, 0);
                animator.SetBool("run", true);
            }
            else
            {
                animator.SetBool("run", false);
            }

            // Salto
            if (Input.GetButtonDown("Jump") && rb.velocity.y == 0) // Salta si está en el suelo
            {
                jumping = true;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetTrigger("jump");
                AudioManager.instance.PlaySFX(jumpSFX, 1);
            }

            // Ataque
            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("attack");
                isAttacking = true;
                AudioManager.instance.PlaySFX(espadazosSFX, 1);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isHit)
        {
            // Movimiento horizontal
            rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);

            // Actualización del estado de salto
            if (jumping && rb.velocity.y == 0)
            {
                jumping = false;
            }

            animator.SetBool("jump", jumping);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Daño al enemigo
            try
            {
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            }
            catch
            {
                collision.gameObject.GetComponent<FinalBossController>().TakeDamage(damage);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        GameManager.instance.life -= damage;
        levelManager.UpdateLife();

        if (GameManager.instance.life <= 0)
        {
            // Muerte del jugador
            animator.SetTrigger("death");
            AudioManager.instance.PlaySFX(deathSFX, 1);
            this.enabled = false;
        }
        else
        {
            // Recibe daño
            isHit = true;
            AudioManager.instance.PlaySFX(hitSFX, 1);
            Invoke(nameof(ResetHit), 0.5f); // Reinicia el estado de golpe tras 0.5 segundos
        }
    }

    private void ResetHit()
    {
        isHit = false;
    }

    private void WallJump(Vector2 wallNormal)
    {
        // Impulso al saltar desde una pared
        rb.AddForce((wallNormal + Vector2.up) * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("jump");
    }

    public void PlayStepsSound()
    {
        AudioManager.instance.PlaySFX(stepsSFX, 1);
    }
}
