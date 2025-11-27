using UnityEngine;
using UnityEngine.UI;

public class UIPause : MonoBehaviour
{
    public Button resumeButton;
    public Button restartButton;
    public Button exitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resumeButton.onClick.AddListener(() =>
        {
            UIManager.instance.Resume();
        });
        restartButton.onClick.AddListener(() =>
        {
            UIManager.instance.Restart();
        });
        exitButton.onClick.AddListener(() =>
        {
            UIManager.instance.Exit();
        });
    }


}
