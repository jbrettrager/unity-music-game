using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JudgeUI : MonoBehaviour
{
   [SerializeField] private Transform judgeTransform;
   [SerializeField] private Transform comboTransform;
   [SerializeField] private Transform errorTransform;
   [SerializeField] private Road road;

   private int combo;

   private void Awake()
   {
      judgeTransform.gameObject.SetActive(false);
      comboTransform.gameObject.SetActive(false);
      errorTransform.gameObject.SetActive(false);
      road.OnNoteHit += Road_OnNoteHit;
      road.OnChartEnd += Road_OnChartEnd;
      road.OnFailSong += Road_OnFailSong;
      combo = 0;
   }
   private void Road_OnFailSong(object sender, Road.OnFailSongEventArgs e)
   {
      judgeTransform.gameObject.SetActive(false);
      comboTransform.gameObject.SetActive(false);
      errorTransform.gameObject.SetActive(false);
   }
   private void Road_OnChartEnd(object sender, Road.OnChartEndEventArgs e)
   {
      judgeTransform.gameObject.SetActive(false);
      comboTransform.gameObject.SetActive(false);
      errorTransform.gameObject.SetActive(false);
   }

   private void Road_OnNoteHit(object sender, Road.OnNoteHitEventArgs e)
   {
      string comboUIText = comboTransform.gameObject.GetComponent<TextMeshProUGUI>().text;
      float errorText = e.error * 1000;
      if (e.judge == "perfect")
      {
         judgeTransform.gameObject.SetActive(true);
         errorTransform.gameObject.SetActive(true);
         judgeTransform.gameObject.GetComponent<TextMeshProUGUI>().text = "PERFECT!";
         errorTransform.gameObject.GetComponent<TextMeshProUGUI>().text = errorText.ToString("0.0") + " ms";
      }
      if (e.judge == "decent")
      {
         judgeTransform.gameObject.SetActive(true);
         errorTransform.gameObject.SetActive(true);
         judgeTransform.gameObject.GetComponent<TextMeshProUGUI>().text = "DECENT";
         errorTransform.gameObject.GetComponent<TextMeshProUGUI>().text = errorText.ToString("0.0") + " ms";
      }
      if (e.judge == "miss")
      {
         judgeTransform.gameObject.SetActive(true);
         errorTransform.gameObject.SetActive(false);
         judgeTransform.gameObject.GetComponent<TextMeshProUGUI>().text = "MISS";
      }
      if (e.combo == 1)
      {
         combo++;
         comboTransform.gameObject.SetActive(true);
         comboTransform.gameObject.GetComponent<TextMeshProUGUI>().text = $"{combo} COMBO";
      }
      if (e.combo == 0)
      {
         combo = 0;
         comboTransform.gameObject.SetActive(false);
      }
   }



}
