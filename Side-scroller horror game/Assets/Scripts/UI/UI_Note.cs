using UnityEngine;
using TMPro;

public class UI_Note : MonoBehaviour
{
    private UI ui;
    [SerializeField] private TextMeshProUGUI noteContent;

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
    }

    public void Show(string text)
    {
        gameObject.SetActive(true);
        noteContent.text = text;
    }

    public void Hide() 
    {
        gameObject.SetActive(false);
        ui.StopPlayerControls(false);    
    } 

}
