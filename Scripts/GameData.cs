using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class GameData 
{
        [Header("PlayerStats")]
        [SerializeField] private float life;
        [SerializeField] private float maxLife;
        [SerializeField] private float mana;
        [SerializeField] private float maxMana;
        [Space]

        [SerializeField] private int currentScene;
        [SerializeField] private Vector3 playerPos;
        [SerializeField] private int ranura;

        [Header("Powers")]
        [SerializeField] private bool hasFireRune;
        [SerializeField] private int hasAirRune;


        public float Life
        {
            get { return life; }
            set { life = value; }
        }
        public float MaxLife
        {
            get { return maxLife; }
            set { maxLife = value; }
        }

        public float Mana
        {
            get { return mana; }
            set { mana = value; }
        }

        public float MaxMana
        {
            get { return maxMana; }
            set { maxMana = value; }
        }

        public int CurrentScene
        {
            get { return currentScene; }
            set { currentScene = value; }
        }

        public Vector3 PlayerPos
        {
            get { return playerPos; }
            set { playerPos = value; }
        }

        public int Ranura
        {
            get { return ranura; }
            set { ranura = value; }
        }

        public bool HasFireRune
        {
            get { return hasFireRune; }
            set { hasFireRune = value; }
        }
        public int HasAirRune
        {
            get { return hasAirRune; }
            set { hasAirRune = value; }
        }
    }