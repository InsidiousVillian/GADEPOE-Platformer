using UnityEngine;
using Unity.Cinemachine; // Updated namespace for Cinemachine v3

public class CameraZoneTrigger : MonoBehaviour
{
    [Header("Target Camera for this Zone")]
    // In v3, the component type is simply called CinemachineCamera
    public CinemachineCamera zoneCamera; 

    [Header("Priority Configurations")]
    [SerializeField] private int activePriority = 20;
    [SerializeField] private int inactivePriority = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && zoneCamera != null)
        {
            zoneCamera.Priority = activePriority; // Capital 'P' in modern v3 properties
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && zoneCamera != null)
        {
            zoneCamera.Priority = inactivePriority;
        }
    }
}