using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    public void OnPlayerCaught()
    {
        StartCoroutine(ChangeSceneCo());
    }

    private IEnumerator ChangeSceneCo()
    {
        // Fade out kalau ada
        // yield return FadeScreen.Out();

        yield return new WaitForSeconds(1.2f); // waktu caught

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
