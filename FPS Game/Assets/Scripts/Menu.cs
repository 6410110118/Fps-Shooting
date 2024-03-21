using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour
{
    private AudioSource source
    {
        get { return GetComponent<AudioSource>(); }
    }
    private bool startable;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToControls()
    {
        transform.Find("Controls").gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
