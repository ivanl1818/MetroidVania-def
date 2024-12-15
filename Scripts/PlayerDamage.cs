using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private bool proyectil;
    [SerializeField]
    private bool doingFall;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        if (doingFall == true)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doingFall == true)
        {
            Vector3 dirToLook = rb.velocity;

            Quaternion rot = Quaternion.LookRotation(dirToLook);
            transform.rotation = rot;
            transform.Rotate(new Vector3(0, 90, 0), Space.Self);
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
        if (proyectil == true)
        {
            Destroy(gameObject);
        }
    }
}
