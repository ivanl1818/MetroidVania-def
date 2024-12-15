using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Image lifeBar;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private AudioClip LevelMusic;

    // Start is called before the first frame update
    void Start()
    {
        // Configurar la posición inicial del jugador
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerTransform.position = spawnPoints[GameManager.instance.nextSpawnPoint].position;
        playerTransform.rotation = spawnPoints[GameManager.instance.nextSpawnPoint].rotation;

        // Reproducir música del nivel
        if (LevelMusic != null)
        {
            AudioManager.instance.PlayMusic(LevelMusic, 1);
        }

        // Actualizar la barra de vida al inicio
        UpdateLife();
    }

    // Método para actualizar la barra de vida
    public void UpdateLife()
    {
        // Calcula el porcentaje de vida y actualiza la barra
        float lifePercentage = Mathf.Clamp01(GameManager.instance.life / GameManager.instance.maxLife);
        lifeBar.fillAmount = lifePercentage;
    }
}
