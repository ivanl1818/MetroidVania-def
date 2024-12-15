using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : MonoBehaviour
{
    public enum bossStates { Idle, Rugido, Roll, Spines, Jump, Walk, Hit, Death }

    [SerializeField]
    private bossStates state;
    private Animator animator;
    private Rigidbody2D rb;
    public bool waiting;
    private float speed;
    private Transform player;
    private bool isHit;
    [Header("Stats Various")]
    [SerializeField]
    private float knockBackForce;
    [SerializeField]
    public float life;
    [SerializeField]
    public float maxLife;
    private float damage;
    [SerializeField]
    private float stopingDistance;
    [SerializeField]
    private Transform roarSpawnPoint;
    [SerializeField]
    private GameObject roarProyectilPrefab;
    [SerializeField]
    private float roarProyectilSpeed;
    [SerializeField]
    private float roarAttackTime;
    public float rollSpeed;
    private bool haChocado;
    private bool isRolling;
    [Header("Spike Attack")]
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private float spikeSpeed;
    public GameObject spikePrefab;
    [Header("Jump")]
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpSpeed = 1;
    private bool isJumping;


    // Start is called before the first frame update
    void Start()
    {

        state = bossStates.Idle;
        animator = GetComponent<Animator>();
        rb.GetComponent<Rigidbody2D>();
        waiting = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ChangeState(bossStates.Idle);
    }
    void CheckDirection()
    {
        Vector2 distanciaVector = player.position - transform.position;
        Vector2 direction = distanciaVector.normalized;
        if (direction.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (direction.x < 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    public void ChangeState(bossStates _state)
    {
        state = _state;
        CheckDirection();
        switch (state)
        {
            case bossStates.Idle:
                // no hara nada
                StartCoroutine(Idle());
                break;
            case bossStates.Rugido:
                Debug.Log("Rugido");
                //animacion de rugido
                //Lance algo
                //tendremos que saber cuando termina para cambiar el estado
                break;
            case bossStates.Roll:
                Debug.Log("Roll");
                StartCoroutine(Roll());
                //animacion de rodar
                //Moverlo mientras rueda
                //Cuando impacte contra la pared termina el estado
                break;
            case bossStates.Spines:
                StartCoroutine(Spines());
                //Animacion de spines
                // Instanciamos las espinas
                //Que se quede unos segundos o hasta que le peguen
                //Despues de ese tiempo termina el estado 
                break;
            case bossStates.Jump:
                StartCoroutine(Jump());
                //Animacion de jump
                //Desplazar al enemigo a otro sitio
                //Termina el estado cuando toque el suelo
                break;
            case bossStates.Death:
                //animacion de muerte
                //desactivamos  que no pueda hacer nada mas y se quede muerto
                break;
            case bossStates.Walk:
                //empezara a moverse
                StartCoroutine(Walk());
                //Animacion andar
                //mover hacia al jugador 
                //A cierta distancia del player

                break;

            default:

                break;
        }
    }

    IEnumerator Idle()
    {
        while (waiting == true)
        {
            yield return null;
        }

        ChangeState(bossStates.Jump);
    }

    IEnumerator Walk()
    {
        animator.SetBool("Walk", true);
        Vector2 distanciaVector = player.position - transform.position;
        Vector2 direction = distanciaVector.normalized;
        float distancia = distanciaVector.magnitude;
        while (distancia > stopingDistance)
        {
            transform.Translate(new Vector2(speed * direction.x, 0) * Time.deltaTime, Space.World);
            distanciaVector = player.position - transform.position;
            CheckDirection();
            direction = distanciaVector.normalized;
            distancia = distanciaVector.magnitude;
            yield return null;
        }
        rb.velocity = Vector2.zero;
        animator.SetBool("Walk", false);
        int azar = Random.Range(1, 4);

        //ChangeState(bossStates(azar));
        ChangeState(bossStates.Rugido);
    }


    IEnumerator Roar()
    {
        //animacion de rugido
        animator.SetTrigger("Roar");
        //Lance algo
        yield return new WaitForSeconds(roarAttackTime);
        //tiene que hacer ondas de sondo

        //tendremos que saber
    }

    public void ShootRoarProyectil()
    {
        GameObject clone = Instantiate(roarProyectilPrefab, roarSpawnPoint.position, roarSpawnPoint.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * -1 * roarProyectilSpeed);
    }
    IEnumerator Roll()
    {
        //Hacer al enemigo rodar
        animator.SetTrigger("Roll");
        haChocado = false;
        yield return new WaitForSeconds(1.2f);
        //Moverlo mientras rueda
        isRolling = true;
        while (haChocado == false)
        {
            transform.Translate(transform.right * -1 * rollSpeed * Time.deltaTime, Space.World);
            rb.velocity = transform.right * -1 * rollSpeed;
            yield return null;
        }
        isRolling = false;
        //Cuando impacte contra la pared termina el estado
        yield return new WaitForSeconds(1);
        int azar = Random.Range(1, 6);
        ChangeState((bossStates)azar);
    }
    IEnumerator Spines()
    {
        animator.SetTrigger("Spike");
        gameObject.layer = 7;
        //Que se queden unos segundos o pau hasta que le peguen
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(1, 23f);
        float timePass = 0;
        while (timePass < 4 && isHit)
        {
            yield return null;
            timePass += Time.deltaTime;
        }
        //Despues de ese tiempo termina el estado
        animator.SetTrigger("NotTired");
        yield return new WaitForSeconds(1);
    }

    public void LaunchSpines()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {


            GameObject cloneSpike = Instantiate(spikePrefab, spawnPoints[i].position, spawnPoints[i].rotation);
            cloneSpike.GetComponent<Rigidbody2D>().AddForce(cloneSpike.transform.right * -1 * rollSpeed);
        }
    }
    IEnumerator Jump()
    {
        animator.SetTrigger("TakeOff");
        animator.SetBool("Jump", true);
        //Desplazar al enemigo a otro sitio 
        isJumping = true;
        Vector3 playerPos = player.position;
        Vector3 bossPos = transform.position;
        rb.AddForce(Vector2.up * jumpForce);
        float t = 0;
        while (t < 1 || isJumping == false)
        {
            t += Time.deltaTime * jumpSpeed;
            float x = Mathf.Lerp(bossPos.x, playerPos.x, t);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
            yield return null;
        }


        //Termina el estado cuando toque el suelo
        yield return new WaitForSeconds(1);
        int azar = Random.Range(1, 6);
        //ChangeState((bossStates)azar);
    }



    public void StartRoll()
    {
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0, 55f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isRolling == true)
            if (collision.gameObject.tag == "Name")
            {
                rb.velocity = Vector2.zero;
                gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(1, 21f);
                collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
                animator.SetTrigger("Chocado");
                haChocado = true;
                gameObject.layer = 0;
            }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            //que nos haga daño
            gameObject.layer = 8;
            AddKnockBackForceToPlayer();
        }
        else if (isJumping == true)
            if (collision.gameObject.tag == "Player")
            {
                gameObject.layer = 8;
            }
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("Jump", false);
            isJumping = false;
            gameObject.layer = 0;
        }
    }

    void AddKnockBackForceToPlayer()
    {
        player.GetComponent<Rigidbody2D>().AddForce(transform.right * -1 * knockBackForce);
    }
    public void TakeDamage(float _damage)
    {
        life -= _damage;
        if (life <= 0)
        {
            animator.SetTrigger("Death");
            StopAllCoroutines();
            GetComponent<CapsuleCollider2D>().enabled = false;
            rb.isKinematic = true;
        }
        else
        {
            StartCoroutine(HitAnim());
        }
    }

    IEnumerator HitAnim()
    {
        //inicial
        Color colorInicial = Color.white;
        //final
        Color colorFinal = Color.red;
        //t
        float t = 0;
        SpriteRenderer bossSprite = GetComponent<SpriteRenderer>();
        while (t < 1)
        {
            bossSprite.color = Color.Lerp(colorInicial, colorFinal, t);
            t += Time.deltaTime * 2;
            yield return null;
        }
        yield return null;
    }
    //t=>1

}




