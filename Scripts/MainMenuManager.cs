using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenuManager : MonoBehaviour
{
        [SerializeField]
        private GameObject partidasPanel;
        private LevelManager levelManager;
        private GameManager gameManager;
        public Button button;

        public void PlayButton()
        {
            partidasPanel.SetActive(true);
            partidasPanel.transform.GetChild(1).GetComponent<Button>().Select();
            RevisarPartidas();
        }

        public void ExitButton()
        {
            Application.Quit();
        }

        public void BackButton()
        {
            partidasPanel.SetActive(false);
            GameObject.Find("StartButton").GetComponent<Button>().Select();
        }

        void RevisarPartidas()
        {
            for (int i = 0; i <= 3; i++)
            {
                if (PlayerPrefs.HasKey("gameData" + i.ToString()))
                {
                    GameManager.instance.LoadData();

                    partidasPanel.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Partida" + i.ToString() + "\nVida:" + GameManager.instance.gameData.Life;
                }

                else
                {
                    partidasPanel.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Vacio";
                }

            }

        }

        public void StartGame(int ranura)
        {
            if (PlayerPrefs.HasKey("gameData" + ranura.ToString()))
            {
                GameManager.instance.LoadData();
                SceneManager.LoadScene(GameManager.instance.gameData.CurrentScene);
            }

            else
            {
                GameManager.instance.gameData = new GameData();
                GameManager.instance.gameData.Life = 100;
                GameManager.instance.gameData.MaxLife = 100;
            }
        }
    }




    /*int numero = 0;
            while(numero<10)
            {
                numero += 1;
            }

            do
            {
                numero += 1;
            } while (numero < 10);

            for(int i =0;i<10;i++)
            {
                numero += 1;
            }
            int[] numeros = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, };
            foreach(int nombreTemporal in numeros)
            {
                Debug.Log(nombreTemporal)
            }*/


