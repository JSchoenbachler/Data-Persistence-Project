using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public GameObject nameInput;
    public void Start() {
        if (SessionManager.Instance.playerName != null) {
            nameInput.GetComponent<InputField>().text = SessionManager.Instance.playerName;
        }
    }
    // Start is called before the first frame update
    public void StartNew() {
        SessionManager.Instance.playerName = nameInput.GetComponent<InputField>().text;
        SceneManager.LoadScene(1);
    }
    public void Exit() {
        SessionManager.Instance.SaveHighScores();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
