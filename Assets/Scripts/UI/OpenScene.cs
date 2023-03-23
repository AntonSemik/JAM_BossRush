using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    [SerializeField] int openSceneId;

    public delegate void OnOpenScene();
    public static OnOpenScene onOpenScene;

    private void Awake()
    {
        onOpenScene += LoadScene;
    }

    private void OnDestroy()
    {
        onOpenScene -= LoadScene;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(openSceneId);
    }
}
