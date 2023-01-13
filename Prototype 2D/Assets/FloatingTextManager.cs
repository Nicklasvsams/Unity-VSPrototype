using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    [SerializeField] private GameObject textContainer;
    [SerializeField] private GameObject textPrefab;

    private List<FloatingText> textList = new List<FloatingText>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        foreach(FloatingText txt in textList)
        {
            txt.UpdateFloatingText();
        }
    }

    public void Show(string textToShow, int fontSize, Color colour, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText text = GetFloatingText();

        text.floatingText.text = textToShow;
        text.floatingText.fontSize = fontSize;
        text.floatingText.color = colour;
        text.go.transform.position = Camera.main.WorldToScreenPoint(position);
        text.motion = motion;
        text.duration = duration;
        text.go.GetComponent<Outline>().effectColor = Color.black;

        text.Show();
    }

    private FloatingText GetFloatingText()
    {
        FloatingText text = textList.Find(t => !t.active);

        if (text == null)
        {
            text = new FloatingText();
            text.go = Instantiate(textPrefab);
            text.go.transform.SetParent(textContainer.transform);
            text.floatingText = text.go.GetComponent<Text>();

            textList.Add(text);
        }

        return text;
    }
}
