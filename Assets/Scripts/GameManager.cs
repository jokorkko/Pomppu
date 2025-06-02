using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int score;
    private int high_score;
    public UIBuilder ui;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        high_score = 0;

        ui = GetComponent<UIBuilder>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore()
    {
        score += 1;

        if (score > high_score)
        {
            high_score = score;
            ui.UpdateBestScore(high_score);
        }

        ui.UpdateScore(score);
    }

    public void ResetScore()
    {
        score = 0;
        ui.UpdateScore(score);
    }
    

}
