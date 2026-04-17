using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public bool isRespawning;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(string sceneName, float delayIfNeeded = 0f)
    {
        StartCoroutine(ChangeSceneCo(sceneName, delayIfNeeded));
    }

    private IEnumerator ChangeSceneCo(string sceneName, float delayIfNeeded = 0f)
    {
        if (delayIfNeeded != 0f)
            yield return new WaitForSeconds(delayIfNeeded);

        UI_FadeScreen fadeScreen = FindFadeScreenUI(); //kenapa gak pake ui.instance aja, karena di main menu, script UI gak di attach

        fadeScreen.DoFadeOut(); // transperent > black

        yield return fadeScreen.fadeEffectCo;

        SceneManager.LoadScene(sceneName);

        fadeScreen = FindFadeScreenUI();//di deklarasi lagi karena sudah ganti scene, might be lost reference
        fadeScreen.DoFadeIn(); // black > transperent
    }

    private UI_FadeScreen FindFadeScreenUI()
    {
        if (UI.instance != null)
            return UI.instance.fadeScreenUI;
        else
            return FindFirstObjectByType<UI_FadeScreen>(); //khusus untuk main menu karena disitu UI nya null
    }
}
