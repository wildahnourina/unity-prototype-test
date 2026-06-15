using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public bool isRespawning;
    public bool task1Completed = false;

    private TriggerEmitter dialogue_emitter;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        TryGetComponent(out dialogue_emitter);
    }

    private void Start()
    {
        GameIntro();
    }

    private void GameIntro()
    {
        StartCoroutine(GameIntroCo());
    }

    private IEnumerator GameIntroCo()
    {
        UI_FadeScreen fadeScreen = FindFadeScreenUI(); 
        fadeScreen.DoFadeIn();

        yield return new WaitForSeconds(1f);

        dialogue_emitter.TriggerEmit();

        // nanti bisa tambah:
        // fade
        // lock movement
        // music
        // camera pan
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
