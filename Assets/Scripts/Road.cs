using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Microsoft.VisualBasic.FileIO;
using UnityEngine.UIElements;
using System.IO;
using CsvHelper;
using System;
using System.Globalization;
using TMPro;
using CsvHelper.Configuration;

public class Road : MonoBehaviour
{
    private INote[] notesArray;

    private string[] chartsAdded;
    private float countdownTime;

    private const string currentChart = "C:\\Users\\phnix\\Test Music Game\\Assets\\Scripts\\ChartData\\testNoteData.chart";

    [SerializeField] GameObject leftNotePrefab;
    [SerializeField] GameObject rightNotePrefab;
    [SerializeField] private Player player;

    public event EventHandler<OnHPChangedEventArgs> OnHPChanged;
    public event EventHandler<OnNoteHitEventArgs> OnNoteHit;
    public event EventHandler<OnChartEndEventArgs> OnChartEnd;
    public event EventHandler<OnFailSongEventArgs> OnFailSong;

    List<INote> onScreenNoteList;

    List<NoteModel> noteList;
    [SerializeField] string chart;

    float perfectWindow = 0.032f;
    float decentWindow = 0.080f;

    private int maxCombo;
    private int currentCombo;
    private int perfectCount;
    private int decentCount;
    private int missCount;
    private int noteCount;
    private int currentNote;
    private int playerHP = 100;
    private bool failed = false;

    private float playerTimer = 0;

    [SerializeField] float visualOffset = 0;
    [SerializeField] float audioOffset = 0;


    private string testNotesThing;

    public class OnNoteHitEventArgs : EventArgs
    {
        public string judge;
        public int combo;
        public float error;
    }


    public class OnHPChangedEventArgs : EventArgs
    {
        public float hpChange;

    }

    public class OnChartEndEventArgs : EventArgs
    {
        public string maxCombo;
        public string perfectCount;
        public string decentCount;
        public string missCount;
        public string noteCount;

    }

    public class OnFailSongEventArgs : EventArgs
    {
        public string perfectCount;
        public string decentCount;
        public string missCount;
        public string noteCount;
        public string currentNote;
    }

    private void Awake()
    {
        player.OnCountdownChange += Player_OnCountdownChange;
        noteList = readChart(chart);
        foreach (var NoteModel in noteList)
        {
            parseNotes(NoteModel.time, NoteModel.type);
        }
    }

    private void Update()
    {
        if (countdownTime == 0 && !failed) playerTimer += Time.deltaTime;
        if (noteList.Count > 0)
        {
            float nextNoteTiming = noteList[0].time;
            float missWindow = nextNoteTiming + 0.031f;
            if (playerTimer > missWindow)
            {
                noteList.RemoveAt(0);
                missCount++;
                currentNote++;
                currentCombo = 0;
                playerHP -= 10;
                OnHPChanged?.Invoke(this, new OnHPChangedEventArgs { hpChange = -0.1f });
                OnNoteHit?.Invoke(this, new OnNoteHitEventArgs { judge = "miss", combo = 0 });
                if (playerHP <= 0)
                {
                    failed = true;
                    OnFailSong?.Invoke(this, new OnFailSongEventArgs { perfectCount = perfectCount.ToString(), currentNote = currentNote.ToString(), decentCount = decentCount.ToString(), missCount = missCount.ToString(), noteCount = noteCount.ToString() });
                }
            }
        }
        else
        {
            OnChartEnd?.Invoke(this, new OnChartEndEventArgs { maxCombo = maxCombo.ToString(), perfectCount = perfectCount.ToString(), decentCount = decentCount.ToString(), missCount = missCount.ToString(), noteCount = noteCount.ToString() });
        }
    }
    public void acceptInput(string inputType)
    {
        if (noteList[0] != null)
        {
            string nextNoteType = noteList[0].type;
            float nextNoteTiming = noteList[0].time + audioOffset;
            float inputTimingDifference = Math.Abs(nextNoteTiming - playerTimer);
            float trueTimingDifference = nextNoteTiming - playerTimer;


            Debug.Log(inputTimingDifference);

            if (inputTimingDifference <= perfectWindow && nextNoteType == inputType)
            {
                currentNote++;
                perfectCount++;
                currentCombo++;
                if (currentCombo > maxCombo) maxCombo = currentCombo;
                noteList.RemoveAt(0);
                playerHP += 3;
                OnHPChanged?.Invoke(this, new OnHPChangedEventArgs { hpChange = +0.03f });
                OnNoteHit?.Invoke(this, new OnNoteHitEventArgs { judge = "perfect", combo = 1, error = trueTimingDifference });

            }
            else if (inputTimingDifference <= decentWindow && nextNoteType == inputType)
            {
                currentNote++;
                decentCount++;
                currentCombo++;
                if (currentCombo > maxCombo) maxCombo = currentCombo;
                noteList.RemoveAt(0);
                playerHP += 1;
                OnHPChanged?.Invoke(this, new OnHPChangedEventArgs { hpChange = +0.01f });
                OnNoteHit?.Invoke(this, new OnNoteHitEventArgs { judge = "decent", combo = 1, error = trueTimingDifference });
            }
        }

    }

    private void Player_OnCountdownChange(object sender, Player.OnCountdownChangeEventArgs e)
    {
        countdownTime = e.countdown;
    }


    private void instantiateNote(float xPosition, string type)
    {
        Vector3 notePosition = new Vector3(xPosition + visualOffset, 0f, 0f);
        if (type == "left")
        {
            Instantiate(leftNotePrefab, notePosition, Quaternion.identity);
        }
        if (type == "right")
        {
            Instantiate(rightNotePrefab, notePosition, Quaternion.identity);
        }
    }
    private void parseNotes(float sValue, string type)
    {
        float convertConstant = 27f;
        float noteXPosition = sValue * convertConstant;
        if (type == "left")
        {
            instantiateNote(noteXPosition, "left");
        }
        if (type == "right")
        {
            instantiateNote(noteXPosition, "right");
        }

    }


    public List<NoteModel> readChart(string chart)
    {
        List<NoteModel> result = new List<NoteModel>();

        if (!File.Exists(chart))
        {
            throw new Exception("Chart Not Found " + chart);
        }
        using var reader = new StreamReader(chart);
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Read header
            csv.Read();
            csv.ReadHeader();

            // Get file path from the header
            string songFilePath = csv.GetField<string>(0);

            // Iterate over each record and extract data points
            while (csv.Read())
            {
                noteCount++;
                var time = csv.GetField<float>(0);
                var type = csv.GetField<string>(1);
                result.Add(new NoteModel { time = time, type = type });
            }
        }
        return result;
    }






    //2.103081 seconds, X position is 56.7832 is 27 units a second, 0.027 units per ms
    //to go from units to seconds ->  units / 27
}
