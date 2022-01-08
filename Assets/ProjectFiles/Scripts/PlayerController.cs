using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public FirstPersonController fpc;

    public TitanSwitch TS;

    public Animator PlayerAnim;

    public Transform AttackPoint;
    public float AttackRange;
    public LayerMask Layer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(AttackPoint.position, AttackPoint.forward, out hitInfo, AttackRange, Layer);

        if (hit)
        {
            print("Nape Struck!");
            if (Input.GetMouseButtonDown(1))
            {
                TS.NapeHasBeenStruck = true;
            }

            Debug.DrawLine(AttackPoint.position, hitInfo.point, Color.red);
        }

        if (Input.GetMouseButtonDown(1))
        {
            PlayerAnim.SetBool("Attacking", true);
            print("its working?");
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(2f);
        PlayerAnim.SetBool("Attacking", false);
    }
}
