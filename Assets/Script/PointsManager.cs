using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    private int points;

    [SerializeField]
    private float pointsInterval = 0.5f;

    [SerializeField]
    private UnityEvent<int> onPointsChanged;

    private Coroutine pointsCoroutine;

    [SerializeField]
    private Text[] pointsText;

    public void StartCounting()
    {
        points = 0;
        UpdatePointsText();
        onPointsChanged?.Invoke(points);

        if (pointsCoroutine != null)
            StopCoroutine(pointsCoroutine);

        pointsCoroutine = StartCoroutine(CountPoints());
    }

    public void StopCounting()
    {
        if (pointsCoroutine != null)
        {
            StopCoroutine(pointsCoroutine);
            pointsCoroutine = null;
        }
    }

    private IEnumerator CountPoints()
    {
        while (true)
        {
            yield return new WaitForSeconds(pointsInterval);
            points++;
            onPointsChanged?.Invoke(points);
            UpdatePointsText();
        }
    }

    public void CalculateHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (points > highScore)
        {
            PlayerPrefs.SetInt("HighScore", points);
            PlayerPrefs.Save();
            highScore = points;
        }

        foreach (var text in pointsText)
        {
            text.text = "HIGH SCORE: " + highScore;
        }
    }

    private void UpdatePointsText()
    {
        foreach (var text in pointsText)
        {
            text.text = points.ToString();
        }
    }
}