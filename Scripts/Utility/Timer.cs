using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public delegate void BeginGame();
    public event BeginGame Begin;

    public Text text;

    public bool count;

    public float time; //set in sec
    public float limit;

    public GameObject goButton, finishedButton;

    private void FixedUpdate()
    {
        if (count)
        {
            if (time <= limit)
            {
                time += Time.deltaTime;
            }
            else
            {
                time = limit;
                OutOfTime();
            }
        }

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        text.text = string.Format("{00:00}:{1:00}", minutes, seconds);
    }

    public void StartGame()
    {
        count = true;
        goButton.SetActive(false);

        Begin?.Invoke();
    }

    public void EndRound()
    {
        count = false;
    }

    public void OutOfTime()
    {
        count = false;
        Debug.LogWarning("TIME IS OUT!");
    }
}
