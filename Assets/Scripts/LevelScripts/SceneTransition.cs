using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Animator animator;
    // public int sceneToTransitionTo;
    public string sceneToTransitionTo;
    private float howLongToSitAtGamerOver = 10f;
    private Scene whatSceneIsThis;
    private int sceneBuildIndex;
    private string thisSceneName;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeToScene()
    {
        animator.SetTrigger("FadeOutTrigger");
    }

/*
    public void FadeToMenu()
    {
        FadeToScene(0);
    }
    */

    public void FadeOutDone () // for some reason I can't get sceneToTransitionTo to retain it's value  swapping to a few functions for now to remedy
    {
        whatSceneIsThis = SceneManager.GetActiveScene();
        Debug.Log($"We're currently on the following scene: {whatSceneIsThis.name}");
        Debug.Log($"We're currently on scene build index: {whatSceneIsThis.buildIndex}");
        sceneBuildIndex = whatSceneIsThis.buildIndex;
        thisSceneName = whatSceneIsThis.name;

        // based on what scene we're on, transition to the next one
        switch (thisSceneName)
        {
            case "MainMenuScene":
                sceneToTransitionTo = "GameOpeningScene";
                Debug.Log($"Scene we should be swapping to: {sceneToTransitionTo}");
                break;
            case "GameOpeningScene":
                sceneToTransitionTo = "Level_1";
                break;
            case "Level_1":
                sceneToTransitionTo = "GameOverScene";
                break;
            case "GameOverScene":
                sceneToTransitionTo = "MainMenuScene";
                break;
            default:
                Debug.Log("Couldn't figure out what scene to transition to");
                break;
        }
        SceneManager.LoadScene(sceneToTransitionTo);
    }
}
