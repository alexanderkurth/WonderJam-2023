using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class InteractorComponent : MonoBehaviour
{

    private InteractableComponent m_Target;
    public InteractableComponent Target { get { return m_Target; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(m_Target != null)
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
}
