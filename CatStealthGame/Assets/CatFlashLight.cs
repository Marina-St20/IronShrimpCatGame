using Unity.VisualScripting;
using UnityEngine;

public class CatFlashLight : MonoBehaviour
{
    public Quaternion dir;
    public Transform head;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        head = GetComponentInParent<CatAimController>().headPivot;
    }

    // Update is called once per frame
    void Update()
    {
        dir = (Quaternion)head.rotation;
        transform.rotation = dir;
        transform.position = head.position;
    }
}
