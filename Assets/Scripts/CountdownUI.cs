using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
   [SerializeField] private Player player;
   [SerializeField] private Transform countdownTransform;
   private string countdownInitialText = "3";

   private void Awake() {
    countdownTransform.gameObject.SetActive(true);
    countdownTransform.gameObject.GetComponent<TextMeshProUGUI>().text = countdownInitialText;
    player.OnCountdownChange += Player_OnCountdownChange;
   }

   private void Player_OnCountdownChange(object sender, Player.OnCountdownChangeEventArgs e) {
    float actualCountdown = e.countdown - 1;
    if (e.countdown > 1) {
    countdownTransform.gameObject.GetComponent<TextMeshProUGUI>().text = actualCountdown.ToString("0");
    }
    else if (e.countdown > 0) {
        countdownTransform.gameObject.GetComponent<TextMeshProUGUI>().text = "GO!";
    }
    else {
        countdownTransform.gameObject.SetActive(false);
    }

   }
}
