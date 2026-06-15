using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    private void Start()
    {
        transform.root.GetComponentInChildren<UI_Options>(true).LoadUpVolume();
        transform.root.GetComponentInChildren<UI_FadeScreen>().DoFadeIn();

        AudioManager.instance.StartBGM("main_menu");
    }

    public void PlayBTN()
    {
        AudioManager.instance.PlayGlobalSFX("button_click");
        GameManager.instance.ChangeScene("SampleScene");
    }

    public void QuitGameBTN()
    {
        Application.Quit();
    }
}
