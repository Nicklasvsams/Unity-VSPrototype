using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    [SerializeField] public bool active;
    [SerializeField] public GameObject go;
    [SerializeField] public Text floatingText;
    [SerializeField] public Vector3 motion;
    [SerializeField] public float duration;
    [SerializeField] private float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active) return;

        if (Time.time - lastShown > duration) Hide();

        go.transform.position += motion * Time.deltaTime;
    }
}
