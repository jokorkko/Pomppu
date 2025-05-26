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
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Onable()
    {
        score_label = ui.Q<Label>("Score");
        best_score_label = ui.Q<Label>("Best");
    }


}
