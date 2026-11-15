using UnityEngine;

public class ExistencePunch : MonoBehaviour
{
    [SerializeField] private float time;
    void Start()
    {
        Invoke("EndIt", time);
    }

    private void EndIt() 
    {
        Destroy(gameObject);
    }
}
