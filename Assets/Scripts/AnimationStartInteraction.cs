using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStartInteraction : DefaultInteraction
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    AnimatorVariable[] animatorVariables;

    public override void Interact(Transform agent, bool callEvent = true)
    {
        base.Interact(agent);
        StartAnimation();
        Debug.Log("Animation started");
    }

    public void StartAnimation()
    {
        foreach (AnimatorVariable variable in animatorVariables)
        {
            switch (variable.type)
            {
                case AnimatorVariableType.Bool:
                    animator.SetBool(variable.name, variable.boolValue);
                    Debug.Log("Bool set");
                    break;
                case AnimatorVariableType.Float:
                    animator.SetFloat(variable.name, variable.value);
                    Debug.Log("Float set");
                    break;
                case AnimatorVariableType.Int:
                    animator.SetInteger(variable.name, variable.intValue);
                    Debug.Log("Int set");
                    break;
                case AnimatorVariableType.Trigger:
                    animator.SetTrigger(variable.name);
                    Debug.Log("Triggered");
                    break;
            }
        }
    }
}

[System.Serializable]
public class AnimatorVariable
{
    public string name;

    public AnimatorVariableType type;

    //show only if type is bool
    [Tooltip("you can ignore value if is not needed ")]
    public bool boolValue;
    [Tooltip("you can ignore value if is not needed ")]
    public float value;
    [Tooltip("you can ignore value if is not needed ")]
    public int intValue;

}

public enum AnimatorVariableType
{
    Bool,
    Float,
    Int,
    Trigger
}
