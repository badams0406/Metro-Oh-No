using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public abstract void EnterState(Companion aiComp);
    public abstract void UpdateState(Companion aiComp);
   
}
