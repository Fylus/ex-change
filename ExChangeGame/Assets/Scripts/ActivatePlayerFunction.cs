using UnityEngine;
using UnityEngine.Events;

public class ActivatePlayerFunction : MonoBehaviour
{
    
    [SerializeField] private UnityEvent onActivate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onActivate.Invoke();
        }
        
    }
}
