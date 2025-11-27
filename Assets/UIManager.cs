using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private InputSystem_Actions inputSystem;
    public bool isPaused = false;
    public GameObject pauseMenu;
    public UIPause uiPause;

    public  Transform canvas;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        inputSystem = new InputSystem_Actions();
    }
    void Start()
    {
        canvas = Instantiate(canvas.gameObject,transform).transform;
        uiPause = Instantiate(pauseMenu, canvas.transform).GetComponent<UIPause>();
        uiPause.gameObject.SetActive(false);
    }
    void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.UI.Escape.performed += OnEscape;
    }

    void OnDisable()
    {
        inputSystem.Disable();
        inputSystem.UI.Escape.performed -= OnEscape;
    }

    void OnEscape(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;
        if(!isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Resume();
    }
    public void Exit()
    {
        Application.Quit();
    }

    void Pause()
    {
        uiPause.gameObject.SetActive(true);
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        uiPause.gameObject.SetActive(false);
    }
}
