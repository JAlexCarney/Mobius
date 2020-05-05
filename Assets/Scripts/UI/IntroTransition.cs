using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class IntroTransition : MonoBehaviour
{
    public GameObject transitionPrefab;
    Animator animator;
    Animator transition;
    int frameCount;
    bool isAnimating;
    public UnityEvent transitionEvent;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        transition = GameObject.Find("Transition").GetComponent<Animator>();
        frameCount = 0;
    }

    private void FixedUpdate()
    {
        if (isAnimating)
        {
            frameCount++;
            if (frameCount == 654)
            {
                transition.enabled = true;
            }
            else if (frameCount == 720)
            {
                Destroy(GameObject.Find("Transition"));
                SceneHandler scenehandeler = GameObject.Find("SceneHandler").GetComponent<SceneHandler>();
                GameObject transition = Instantiate(transitionPrefab);
                transition.transform.parent = GameObject.Find("MetaManager").transform;
                scenehandeler.transitionObj = transition;
                transition.GetComponent<Animator>().enabled = false;
                isAnimating = false;
                transitionEvent.Invoke();
            }
        }
    }

    public void Transition()
    {
        isAnimating = true;
        animator.enabled = true;
    }
}
