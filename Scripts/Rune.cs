using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    [SerializeField] private string runaName;
    [SerializeField] private AudioClip pickUpRuneSFX;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        switch (runaName)
        {
            case "FireRune":
                animator.SetTrigger("FireRune");
                if (GameManager.instance.gameData.HasFireRune == true)
                {
                    Destroy(gameObject);
                }
                break;

            case "AirRune":
                animator.SetTrigger("AirRune");
                if (GameManager.instance.gameData.HasAirRune > 1)
                {
                    Destroy(gameObject);
                }
                break;

            case "EarthRune":

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioManager.instance.PlaySFX(pickUpRuneSFX, 1);
            switch (runaName)
            {
                case "FireRune":
                    GameManager.instance.gameData.HasFireRune = true;
                    Destroy(gameObject);
                    break;

                case "AirRune":
                    GameManager.instance.gameData.HasAirRune += 1;
                    Destroy(gameObject);
                    break;

                case "EarthRune":

                    break;
            }
        }
    }
}