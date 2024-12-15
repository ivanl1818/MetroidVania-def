using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    private int nextSpawnPoint;
    [SerializeField]
    private int nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.nextSpawnPoint = nextSpawnPoint;
            SceneManager.LoadScene(nextScene);
        }
    }
}

