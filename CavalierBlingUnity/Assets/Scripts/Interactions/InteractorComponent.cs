using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class InteractorComponent : MonoBehaviour
{

    private InteractableComponent m_Target;
    public InteractableComponent Target { get { return m_Target; } }
    public float offScreenCooldown = 1f;

    private Coroutine _isOffScreenCo = null; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameMode.Instance.isGameOver) return; 
       CheckOffScreen();
    }

    public void RegisterInteractable(InteractableComponent target)
    {
        if(m_Target != null)
        {
            RemoveInteractable(target);
        }

        m_Target = target;
        target.OnInteractionAdded();
    }

    public void RemoveInteractable(InteractableComponent target)
    {
        target.OnInteractionRemoved();
        m_Target = null;
    }

    public void TriggerInteraction()
    {
        Debug.Log("INTERACTION ADDED");
        if (m_Target != null)
        {
            m_Target.TriggerInteraction();
        }
    }

    public void OnInteractionChanged(InputAction.CallbackContext context)
    {
        if (context.started) //Pressed
        {
            if(m_Target != null)
            {
                TriggerInteraction();
            }
        }
    }

    public void CheckOffScreen()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        bool isOffScreen = !Screen.safeArea.Contains(pos);
        if (isOffScreen)
        {
            if (_isOffScreenCo == null)
            {
                _isOffScreenCo = StartCoroutine(CheckOffScreenCo());
            }
        }
        else 
        {
            if (_isOffScreenCo != null)
            {
                StopCoroutine(_isOffScreenCo);
                _isOffScreenCo = null;
            }
        }
    }

    private IEnumerator CheckOffScreenCo()
    {
        yield return new WaitForSeconds(offScreenCooldown);
        GameMode.Instance.GameOver(GameMode.GameOverCondition.OutOfScreen);
    }
}
