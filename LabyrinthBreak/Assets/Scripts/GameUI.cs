using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image image;

    private void Start() 
    {
        Player.Instance.OnHealthChanged += Player_OnHealthChanged;
    }

    private void Player_OnHealthChanged(object sender, Player.EventArgsOnHealthChanged e)
    {
        float res = (float)Player.Instance.GetHealth() / (float)e.maxHealth;

        image.fillAmount = (float)System.Math.Round(res,2);

    }
}
