using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeedX = 7f;
    [SerializeField] private float moveSpeedY = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask roadLayerMask;
    [SerializeField] private GameObject editNotePrefab;
    [SerializeField] private Road road;
    public event EventHandler<OnCountdownChangeEventArgs> OnCountdownChange;
    private float countdownTime = 4f;
    private float endCountdown = 3f;
    private float countdown;
    private bool endSong = false;
    private const string path = "C:\\Users\\phnix\\Test Music Game\\Assets\\Scripts\\ChartData\\testNoteData.chart"; //TEST VARIABLE WILL REMOVE
    private Vector3 roadDir = new Vector3(0f, 0f, 1f);
    private float interactDistance = Mathf.Infinity;


    private void Start()
    {
        gameInput.OnNoteLeftAction += GameInput_OnNoteLeftAction;
        gameInput.OnNoteRightAction += GameInput_OnNoteRightAction;
        gameInput.OnNoteEditAction += GameInput_OnNoteEditAction;
        road.OnFailSong += Road_OnFailSong;
        road.OnChartEnd += Road_OnChartEnd;
        StartCoroutine(StartCountdown());

    }

    IEnumerator StartCountdown()
    {
        float currentTime = countdownTime;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            countdown = Mathf.Ceil(currentTime);
            OnCountdownChange?.Invoke(this, new OnCountdownChangeEventArgs { countdown = countdown });
            yield return null;
        }
    }

    public class OnCountdownChangeEventArgs : EventArgs
    {
        public float countdown;

    }
    private void Road_OnChartEnd(object sender, Road.OnChartEndEventArgs e)
    {
        if (endCountdown == 0) endSong = true;
    }
    private void Road_OnFailSong(object sender, Road.OnFailSongEventArgs e)
    {
        endSong = true;
    }
    private void GameInput_OnNoteLeftAction(object sender, System.EventArgs e)
    {


        if (Physics.Raycast(transform.position, roadDir, out RaycastHit raycastHit, interactDistance, roadLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out Road road))
            {
                road.acceptInput("left");
            }
        }
    }
    private void GameInput_OnNoteRightAction(object sender, System.EventArgs e)
    {


        if (Physics.Raycast(transform.position, roadDir, out RaycastHit raycastHit, interactDistance, roadLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out Road road))
            {
                road.acceptInput("right");
            }
        }
    }

    private void GameInput_OnNoteEditAction(object sender, System.EventArgs e)
    {
        Instantiate(editNotePrefab, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (countdown == 0 && !endSong)
        {
            handleMovement();
        }

    }

    private void handleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector2 scrollDir = new Vector2(inputVector.x + 1, 0f);

        Vector2 moveDir = new Vector2(inputVector.x, inputVector.y);

        float playerSize = 0.7f;
        if (Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, playerSize))
        {
            if (!raycastHit.transform.TryGetComponent(out Bound bound))
            {
                transform.position += (Vector3)inputVector * moveSpeedY * Time.deltaTime;
            }
        }
        else
        {
            transform.position += (Vector3)inputVector * moveSpeedY * Time.deltaTime;
        }
        transform.position += (Vector3)scrollDir * moveSpeedX * Time.deltaTime;
    }
}
