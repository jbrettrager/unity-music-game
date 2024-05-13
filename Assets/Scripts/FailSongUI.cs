using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FailSongUI : MonoBehaviour
{
    [SerializeField] Road road;
    [SerializeField] private Transform failedTransform;
    [SerializeField] private Transform failPositionTransform;
    [SerializeField] private Transform perfectTransform;
    [SerializeField] private Transform decentTransform;
    [SerializeField] private Transform missTransform;
    [SerializeField] Button returnButton;

    private void Awake()
    {
        failedTransform.gameObject.SetActive(false);
        failPositionTransform.gameObject.SetActive(false);
        perfectTransform.gameObject.SetActive(false);
        decentTransform.gameObject.SetActive(false);
        missTransform.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        returnButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        road.OnFailSong += Road_OnFailSong;
    }

    private void Road_OnFailSong(object sender, Road.OnFailSongEventArgs e)
    {
        failedTransform.gameObject.GetComponent<TextMeshProUGUI>().text = "FAILED...";
        failedTransform.gameObject.SetActive(true);
        failPositionTransform.gameObject.GetComponent<TextMeshProUGUI>().text = e.currentNote + "/" + e.noteCount;
        failPositionTransform.gameObject.SetActive(true);
        perfectTransform.gameObject.GetComponent<TextMeshProUGUI>().text = "PERFECT: " + e.perfectCount;
        perfectTransform.gameObject.SetActive(true);
        decentTransform.gameObject.GetComponent<TextMeshProUGUI>().text = "DECENT: " + e.decentCount;
        decentTransform.gameObject.SetActive(true);
        missTransform.gameObject.GetComponent<TextMeshProUGUI>().text = "MISS: " + e.missCount;
        missTransform.gameObject.SetActive(true);
        returnButton.gameObject.SetActive(true);
    }
}
