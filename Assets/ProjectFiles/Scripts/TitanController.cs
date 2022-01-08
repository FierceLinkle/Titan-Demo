using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TitanController : MonoBehaviour
{
    public TitanSwitch TS;

    public Camera cam;

    public NavMeshAgent agent;

    public Transform TitanTransform;
    public Transform TitanRayTransform;
    public Transform PlayerTransform;

    private float RandX;
    private float RandZ;
    private float Y;
    private Vector3 PatrolPoint;

    [Range(0,10)]
    public float Distance = 1;

    public bool IsChasing = false;
    public int SightDistance = 950;
    public int GrabRange = 300;

    // Start is called before the first frame update
    void Start()
    {
        //Test
        //agent.SetDestination(new Vector3(90.0f, 0.0f, -45.0f));

        Y = TitanTransform.position.y;

        RefreshRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsChasing)
        {
            agent.SetDestination(PlayerTransform.position);
        }

        if (((TitanTransform.position.x - PlayerTransform.position.x) * (TitanTransform.position.x - PlayerTransform.position.x) + (TitanTransform.position.z - PlayerTransform.position.z) * (TitanTransform.position.z - PlayerTransform.position.z)) > SightDistance)
        {
            //print("out of sight");
            TS.PlayerSeen = false;
        }

        if (((TitanTransform.position.x - PlayerTransform.position.x) * (TitanTransform.position.x - PlayerTransform.position.x) + (TitanTransform.position.z - PlayerTransform.position.z) * (TitanTransform.position.z - PlayerTransform.position.z)) < GrabRange)
        {
            //print("out of sight");
            TS.CanBeGrabbed = true;
        } else
        {
            TS.CanBeGrabbed = false;
        }

        //print((TitanTransform.position.x - PlayerTransform.position.x) * (TitanTransform.position.x - PlayerTransform.position.x) + (TitanTransform.position.z - PlayerTransform.position.z) * (TitanTransform.position.z - PlayerTransform.position.z));
        //print((TitanTransform.position.x - PatrolPoint.x) * (TitanTransform.position.x - PatrolPoint.x) + (TitanTransform.position.z - PatrolPoint.z) * (TitanTransform.position.z - PatrolPoint.z));
    }

    void RefreshRandomPoint()
    {
        RandX = Random.Range(-90f, 90f);
        RandZ = Random.Range(-90f, 90f);

        PatrolPoint = new Vector3(RandX, Y, RandZ);
        //print(PatrolPoint);
    }

    public void Patrol()
    {
        IsChasing = false;
        agent.SetDestination(PatrolPoint);

        if(((TitanTransform.position.x - PatrolPoint.x) * (TitanTransform.position.x - PatrolPoint.x) + (TitanTransform.position.z - PatrolPoint.z) * (TitanTransform.position.z - PatrolPoint.z)) < Distance)
        {
            StartCoroutine(ChangeRandomPoint());
        }
    }

    IEnumerator ChangeRandomPoint()
    {
        RefreshRandomPoint();
        agent.SetDestination(PatrolPoint);
        yield return new WaitForSeconds(3f);
    }

    public void Chase()
    {
        IsChasing = true;
    }

    public void LoseSight()
    {
        IsChasing = false;
        agent.SetDestination(TitanTransform.position);
    }

    public void Death()
    {
        agent.SetDestination(TitanTransform.position);
    }
}
