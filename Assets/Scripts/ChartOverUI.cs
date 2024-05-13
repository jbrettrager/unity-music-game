using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ChartOverUI : MonoBehaviour
{
    [SerializeField] Road road;
    [SerializeField] private Transform clearedTransform;
    [SerializeField] private Transform comboNumberTransform;
    [SerializeField] private Transform comboTextTransform;
    [SerializeField] private Transform perfectTransform;
    [SerializeField] private Transform decentTransform;
    [SerializeField] private Transform missTransform;
    [SerializeField] Button returnButton;

    private void Awake()
    {
        clearedTransform.gameObject.SetActive(false);
        comboNumberTransform.gameObject.SetActive(false);
        comboTextTransform.gameObject.SetActive(false);
        perfectTransform.gameObject.SetActive(false);
        decentTransform.gameObject.SetActive(false);
        missTransform.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        returnButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        road.OnChartEnd += Road_OnChartEnd;

    }
    private void Road_OnChartEnd(object sender, Road.OnChartEndEventArgs e)
    {
        clearedTransform.gameObject.GetComponent<TextMeshProUGUI>().text = "CLEARED!";
        clearedTransform.gameObject.SetActive(true);
        comboNumberTransform.gameObject.GetComponent<TextMeshProUGUI>().text = e.maxCombo + "/" + e.noteCount;
        comboNumberTransform.gameObject.SetActive(true);
        comboTextTransform.gameObject.SetActive(true);
        perfectTransform.gameObject.GetComponent<TextMeshProUGUI>().text = "PERFECT: " + e.perfectCount;
        perfectTransform.gameObject.SetActive(true);
        decentTransform.gameObject.GetComponent<TextMeshProUGUI>().text = "DECENT: " + e.decentCount;
        decentTransform.gameObject.SetActive(true);
        missTransform.gameObject.GetComponent<TextMeshProUGUI>().text = "MISS: " + e.missCount;
        missTransform.gameObject.SetActive(true);
        returnButton.gameObject.SetActive(true);
    }
}
