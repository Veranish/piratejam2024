using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Animator animator;
    private int sceneToTransitionTo;
    public float howLongToSitAtGamerOver = 10f;

    void Start()
    {
        // if this is game over scene, wait a duration, then FadeToScene Menu
        Scene whatSceneIsThis = SceneManager.GetActiveScene();

        // game over is currently build index 3   probably should change this to it's name, since that wont change
        if (whatSceneIsThis.name == "GameOverScene")
        {
            Invoke("FadeToMenu", howLongToSitAtGamerOver);
        }

        Debug.Log($"We're currently on the following scene: {whatSceneIsThis.name}");
        Debug.Log($"We're currently on scene build index: {whatSceneIsThis.buildIndex}");

    }

    // Update is called once per frame
    void Update()
    {
        // right now just looking for F press to test  eventually need other ways to call methods
        if (Input.GetKey(KeyCode.F))
        {
            FadeToScene(3);
        }
    }

    public void FadeToScene(int sceneIndex)
    {
        sceneToTransitionTo = sceneIndex;
        animator.SetTrigger("FadeOutTrigger");
    }

    public void FadeToMenu()
    {
        FadeToScene(0);
    }

    public void FadeOutDone ()
    {
        SceneManager.LoadScene(sceneToTransitionTo);
    }
}
