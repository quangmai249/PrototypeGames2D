using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGoldMinerStats : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] float speedReach;

    public int Score
    {
        get => this.score;
    }

    public float SpeedReach
    {
        get => this.speedReach;
    }
}
