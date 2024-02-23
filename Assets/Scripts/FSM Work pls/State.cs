using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] protected BaseEnemy enemy;
    [SerializeField] protected State deadState; // Common state among all Entities
    public abstract State PlayCurrentState();
}
