using UnityEngine;

public class FallCatcher : MonoBehaviour
{
    [SerializeField]
    private PathSO _leadsToPath;

    [SerializeField]
    private PathStorageSO _pathStorage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _pathStorage.lastPathTaken = _leadsToPath;

            other.GetComponent<Damageable>().Kill();
        }
    }
}