using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    [SerializeField] private List<Vector2> Points = new List<Vector2>();
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private bool Loop = false;
    private int CurrentPoint = 0;
    private int Direction = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Points.Count == 0) return;
        transform.position = Points[0];
    }

    // Update is called once per frame
    void Update()
    {
        //if there are no points to move to return
        if(Points.Count == 0) return;

        Vector2 PointToGoTo = Points[CurrentPoint];
        
        //rotate and move the patrol to a point
        float step = MoveSpeed * Time.deltaTime;
        Vector3 vector3 = new Vector3(PointToGoTo.x, PointToGoTo.y);
        Vector3 dir = vector3 - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = Vector2.MoveTowards(transform.position, Points[CurrentPoint], step);

        //check if we have reached the point
        if(Vector3.Distance(transform.position, PointToGoTo) < 0.001f)
        {
            UpdateCurrentPoint();
        }
    }

    private void UpdateCurrentPoint()
    {
        if(Loop)
        {
            CurrentPoint = (CurrentPoint + 1) % Points.Count;
        }
        else
        {
            CurrentPoint += Direction;
            if(CurrentPoint == Points.Count - 1 || CurrentPoint == 0)
            {
                Direction *= -1;
            }
        }
    }
}
