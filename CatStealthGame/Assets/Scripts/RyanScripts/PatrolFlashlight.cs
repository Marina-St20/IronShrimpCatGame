using System;
using UnityEngine;

public class PatrolFlashlight : MonoBehaviour
{
    [SerializeField] public float viewDistance = 2f;
    [SerializeField] public float viewAngle = 45f;
    private LayerMask wallLayerMask;
    private GameObject Player;

    void Awake()
    {
        wallLayerMask = LayerMask.GetMask("Wall");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 toPlayer = Player.transform.position - transform.position;

        if(toPlayer.magnitude < viewDistance)
        {
            float angle = Vector2.Angle(transform.up, toPlayer);

            if(angle < viewAngle)
            {
                if(CanSeePlayer())
                {
                    Player.GetComponent<PlayerResources>().Die();
                }
            }
        }
    }

    private bool CanSeePlayer()
    {
        Vector2 origin = transform.position;
        Vector2 dir = (Player.transform.position - transform.position).normalized;
        float dist = Vector2.Distance(Player.transform.position, transform.position);
        RaycastHit2D raycastHit = Physics2D.Raycast(origin, dir, dist, wallLayerMask);

        if(raycastHit)
        {
            return false;
        }

        return true;
    }
}
