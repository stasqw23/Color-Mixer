using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwichScene : MonoBehaviour
{
    [SerializeField] Button _restartWon;
    [SerializeField] Button _next;
    [SerializeField] Button _restartLose;
    [SerializeField] int _nexScene;

    void Start()
    {
        _restartWon.onClick.AddListener(() => Restart());
        _restartLose.onClick.AddListener(() => Restart());
        _next.onClick.AddListener(() => Next());


    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Next()
    {
        SceneManager.LoadScene(_nexScene);
    }
}
