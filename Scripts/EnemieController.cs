using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieController : MonoBehaviour
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float life;
        [SerializeField]
        private float speed;
        private Transform playerDetected; // Cambiado a Transform
        private Animator animator;
        [SerializeField]
        private float attackRate;
        [SerializeField]
        private float jumpForce;
        [SerializeField]
        private float knockBackForce;
        [SerializeField]
        private float stopDistance;
        private Rigidbody2D rb;
        private float timePass;
        private bool isHit;
        private bool isAttacking;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            if (playerDetected != null && GameManager.instance.life > 0)
            {
                Vector3 distanciaEnVector = playerDetected.position - transform.position;
                Vector3 direccion = distanciaEnVector.normalized;
                float moduloDistancia = distanciaEnVector.magnitude;

                // Orientar el enemigo hacia el jugador
                if (distanciaEnVector.x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    transform.eulerAngles = Vector3.zero;
                }

                // Movimiento hacia el jugador o detenerse para atacar
                if (moduloDistancia > stopDistance)
                {
                    rb.velocity = new Vector2(speed * direccion.x, rb.velocity.y);
                    animator.SetBool("walk", true);
                }
                else
                {
                    if (!isHit)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        animator.SetBool("walk", false);
                        Attack();
                    }
                }
            }

            timePass += Time.fixedDeltaTime; // Arreglado
        }

        void Attack()
        {
            if (timePass >= attackRate)
            {
                timePass = 0;
                animator.SetTrigger("attack");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) // Mejor método para comparar tags
            {
                playerDetected = collision.transform;
                isAttacking = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerDetected = null; // El jugador ya no está detectado
                isAttacking = false;
            }
        }

        public void TakeDamage(float _damage)
        {
            Debug.Log("El enemigo ha recibido " + _damage + " de daño.");
            life -= _damage; // Resta la vida
            if (life <= 0)
            {
                Death();
            }
        }

        void Death()
        {
            animator.SetTrigger("death");
        }

        public void DestroyEnemy()
        {
            Destroy(this.gameObject);
        }

        public void SetIsHitFalse()
        {
            isHit = false;
        }

        public void IsNotAttacking()
        {
            isAttacking = false;
        }
    }
}

