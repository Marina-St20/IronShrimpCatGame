using Unity.VisualScripting;
using UnityEngine;

public class CatFlashLight : MonoBehaviour
{
    public Quaternion dir;
    public Transform head;
    public Transform offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        head = GetComponentInParent<CatAimController>().headPivot;
        offset.position = GetComponentInParent<Transform>().position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        head = GetComponentInParent<CatAimController>().headPivot;
        dir = (Quaternion)head.rotation;
        transform.rotation = dir;
        transform.position = head.position + offset.position;
    }
}
