using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class status : MonoBehaviour
{
    public int Turns = 20;
    public int Scores = 0;
    public Transform gameOver, touchControl;
    Transform ScoresText, TurnsText;
    // Start is called before the first frame update
    void Start()
    {
        ScoresText = transform.GetChild(0).GetChild(0);
        TurnsText = transform.GetChild(1).GetChild(0);
        ScoresText.GetComponent<TextMeshPro>().text = Scores.ToString();
        TurnsText.GetComponent<TextMeshPro>().text = Turns.ToString();
    }
    public void AddTurns(int t)
    {
        Turns += t;
        TurnsText.GetComponent<TextMeshPro>().text = Turns.ToString();
        if(Turns == 0)
        {
            StartCoroutine("EndLevel");
        }
    }

    public void AddScores(int n)
    {
        Scores += factorial(n) * 10;
        ScoresText.GetComponent<TextMeshPro>().text = Scores.ToString();
    }

    IEnumerator EndLevel()
    {
        var canvasGroup = gameOver.GetComponent<CanvasGroup>();
        for (float t = 0; t <= 1f; t += Time.deltaTime)
        {

            canvasGroup.alpha = t;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        touchControl.gameObject.active = false;
        Application.Quit();
    }

    int factorial(int s)
    {
        int result = 0;
        for(int i = 1; i <= s; i++)
        {
            result += i;
        }
        return result;
    }
}
