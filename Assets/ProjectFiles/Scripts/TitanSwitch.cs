using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//using AlanZucconi.AI.FSM;

public enum GameState
{
    Patrol,
    Attack,
    Grab,
    LoseSight,
    Dead,
    Eat
}

public class TitanSwitch : MonoBehaviour
{
    public FirstPersonController fpc;
    public AudioSource PlayerAudio;

    public TitanController TC;

    public GameState Current = GameState.Patrol;

    public Animator TitanAnim;

    public GameObject GrabBoxActive;
    public GameObject TitanHand;

    public float SearchTime = 5f;

    public bool PlayerSeen = false;
    public bool CanBeGrabbed = false;
    public bool NapeHasBeenStruck = false;
    public bool EatPlayer = false;

    public GameObject VictoryText;
    public GameObject DefeatText;

    public AudioClip TitanWalk;
    public AudioSource TitanSounds;

    // Start is called before the first frame update
    void Start()
    {
        GrabBoxActive.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (Current)
        {
            case GameState.Patrol:              
                GoToRandomPlace();

                if (SeePlayer())
                {
                    Current = GameState.Attack;
                    break;
                }              

                if (NapeStruck())
                {
                    Current = GameState.Dead;
                    break;
                }            
                break;

            case GameState.Attack:
                ChasePlayer();       

                if (PlayerInRange())
                {
                    Current = GameState.Grab;
                    break;
                }
                
                if (!SeePlayer())
                {
                    Current = GameState.LoseSight;
                    break;
                }
                

                if (NapeStruck())
                {
                    Current = GameState.Dead;
                    break;
                }

                break;

            case GameState.Grab:
                GrabPlayer();

                if (!PlayerInRange())
                {
                    Current = GameState.LoseSight;
                    break;
                }

                if (PlayerInEatRange())
                {
                    Current = GameState.Eat;
                    break;
                }

                break;

            case GameState.LoseSight:
                Search();

                if (SeePlayer())
                {
                    Current = GameState.Attack;
                    break;
                }

                if (NapeStruck())
                {
                    Current = GameState.Dead;
                    break;
                }
                break;

            case GameState.Dead:
                TitanDeath();

                break;

            case GameState.Eat:
                Eat();

                break;
        }
    }

    #region StateActions

    void GoToRandomPlace()
    {
        TitanAnim.SetBool("Loop", true);
        TitanAnim.SetBool("InGrabRange", false);

        //TitanSounds.PlayOneShot(TitanWalk);

        TC.Patrol();
    }

    void ChasePlayer()
    {
        TitanAnim.SetBool("InGrabRange", false);

        TC.Chase();
    }

    void GrabPlayer()
    {
        GrabBoxActive.SetActive(true);

        TitanAnim.SetBool("Loop", false);
        TitanAnim.SetBool("InGrabRange", true);
    }

    void TitanDeath()
    {  
        TitanAnim.SetBool("Loop", false);
        TitanAnim.SetBool("InGrabRange", false);
        TitanAnim.SetBool("BeenKilled", true);

        VictoryText.SetActive(true);

        TC.Death();

        StartCoroutine(EndLevel());
    }

    void Search()
    {
        GrabBoxActive.SetActive(false);

        TitanAnim.SetBool("Loop", false);
        TitanAnim.SetBool("InGrabRange", false);
        StartCoroutine(LookAround());
        TC.LoseSight();
    }

    void Eat()
    {
        TitanAnim.SetBool("CanEatPlayer", true);

        DefeatText.SetActive(true);

        PlayerAudio.enabled = false;
        fpc.transform.position = TitanHand.transform.position;
        fpc.transform.parent = GameObject.Find("LeftHand").transform;

        StartCoroutine(EndLevel());
    }

    IEnumerator LookAround()
    {
        print("searching");
        TitanAnim.SetBool("IsSearching", true);
        yield return new WaitForSeconds(SearchTime);
        TitanAnim.SetBool("IsSearching", false);
        Current = GameState.Patrol;
    }

    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(8f);
        fpc.m_MouseLook.SetCursorLock(false);
        fpc.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    #endregion

    #region TransitionActions


    #endregion

    #region TransitionConditions

    public bool SeePlayer()
    {
        return PlayerSeen;
    }

    bool PlayerInRange()
    {
        return CanBeGrabbed;
    }

    bool NapeStruck()
    {
        return NapeHasBeenStruck;
    }

    bool PlayerInEatRange()
    {
        return EatPlayer;
    }

    #endregion
}
