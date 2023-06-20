using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class HandAnimationManager : MonoBehaviour
{
    public InputActionProperty activateAnimationAction;
    public InputActionProperty selectAnimationAction;
    public Animator handAnim;

    void Update()
    {
        float triggerValue = activateAnimationAction.action.ReadValue<float>();
        handAnim.SetFloat("Trigger", triggerValue);

        float gripValue = selectAnimationAction.action.ReadValue<float>();
        handAnim.SetFloat("Grip", gripValue);
    }
}
