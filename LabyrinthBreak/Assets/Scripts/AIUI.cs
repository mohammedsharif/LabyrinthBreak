using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private AIController aIController;

    private void Start()  
    {
        Player.Instance.OnAttackTouched += Player_OnAttackTouched;    
    }

    private void Player_OnAttackTouched(object sender, EventArgs e)
    {
        float res = (float)aIController.GetHealth() / (float) aIController.GetMaxHealth();

        image.fillAmount = (float)System.Math.Round(res,2);
    }
}
