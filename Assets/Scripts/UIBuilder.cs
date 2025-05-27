using NUnit.Framework.Internal.Commands;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBuilder : MonoBehaviour
{

    public VisualElement ui;

    public Label score_label;
    public Label best_score_label;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        ui = FindFirstObjectByType<UIDocument>().rootVisualElement;
        //Debug.Log(GetComponent<UIDocument>());
        //ui = GetComponent<UIDocument>().rootVisualElement;
        score_label = ui.Q<Label>("Score");
        best_score_label = ui.Q<Label>("Best");

    }

    public void UpdateScore(int points)
    {
        score_label.text = "Score: " + points.ToString();
    }

    public void UpdateBestScore(int highscore)
    {
        best_score_label.text = "Best: " + highscore.ToString();
    }


}
