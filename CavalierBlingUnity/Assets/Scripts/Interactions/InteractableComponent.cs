using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableComponent : MonoBehaviour
{
    [Tooltip("In seconds")]
    [SerializeField]
    private float m_MashTotalTime = 2.0f;

    [Tooltip("In seconds")]
    [SerializeField]
    public float m_MaxTimeBewteenMash = 0.2f;

    [SerializeField]
    UnityEvent m_OnInteractionSucessEvent;

    [SerializeField]
    public float m_LootableMoney = 0.0f;

    private bool m_IsInteractionStarted = false;
    private float m_MashTimer = 0.0f;
    private bool m_InteractionDone = false;
    private float m_TimeSinceLastInput = 0.0f;

    public void Update()
    {
        if (m_InteractionDone)
            return;

        if (m_IsInteractionStarted)
        {
            m_MashTimer += Time.deltaTime;
            m_TimeSinceLastInput += Time.deltaTime;

            if(m_TimeSinceLastInput >= m_MaxTimeBewteenMash)
            {
                OnInteractionFailed();
                return;
            }

            if (m_MashTimer >= m_MashTotalTime)
            {
                OnInteractionSucessfull();
            }
        }
    }

    public void OnInteractionAdded()
    {
    }

    public void OnInteractionRemoved()
    {
        m_IsInteractionStarted = false;
    }

    public void TriggerInteraction()
    {
        if (m_InteractionDone)
            return;

        m_IsInteractionStarted = true;
        m_TimeSinceLastInput = 0.0f;

        mMadnessManager.Instance.IncreaseMadnessLevel(mMadnessManager.Instance.GetIncrease());
    }

    private void OnInteractionFailed()
    {
        m_MashTimer = 0.0f;
        m_TimeSinceLastInput = 0.0f;

        m_IsInteractionStarted = false;
    }

    private void OnInteractionSucessfull()
    {
        m_InteractionDone = true;
        m_OnInteractionSucessEvent.Invoke();

        IEnumerator coroutine;
        coroutine = mMadnessManager.Instance.PeakMadness();
        mMadnessManager.Instance.StartCoroutine(coroutine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_InteractionDone)
            return;

        InteractorComponent interactor = other.GetComponent<InteractorComponent>();

        if(interactor)
        {
            interactor.RegisterInteractable(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractorComponent interactor = other.GetComponent<InteractorComponent>();

        if (interactor)
        {
            if(interactor.Target == this)
            {
                interactor.RemoveInteractable(this);
            }
        }
    }
}
