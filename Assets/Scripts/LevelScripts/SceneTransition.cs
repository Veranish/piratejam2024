using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Animator animator;
    private int sceneToTransitionTo;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            FadeToScene(1);
        }
    }

    public void FadeToScene(int sceneIndex)
    {
        sceneToTransitionTo = sceneIndex;
        animator.SetTrigger("FadeOutTrigger");
    }

    public void FadeOutDone ()
    {
        SceneManager.LoadScene(sceneToTransitionTo);
    }
}
