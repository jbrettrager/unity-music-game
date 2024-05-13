using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private Road road;

    private void Start()
    {
        Debug.Log(road);
        road.OnHPChanged += Road_OnHPChanged;
    }

    private void Road_OnHPChanged(object sender, Road.OnHPChangedEventArgs e)
    {
        barImage.fillAmount += e.hpChange;
    }
}
