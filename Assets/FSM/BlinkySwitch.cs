using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementation of Pac-Man's ghost Blinky using FSM
// Matrix style
namespace AlanZucconi.Blinky.Switch
{
    // The list of available states
    public enum State
    {
        Scatter,
        Attack,
        Flee,
        Dead
    }

    public class BlinkySwitch : MonoBehaviour
    {
        // The current state of the game
        public State Current = State.Scatter;

        // Update is called once per frame
        void Update()
        {
            switch (Current)
            {
                case State.Scatter:
                    GotoRandomPlace();

                    // Scatter -> Attack
                    if (Timer())
                    {
                        Current = State.Attack;
                        break;
                    }
                    break;

                case State.Attack:
                    ChasePacman();

                    // Attack -> Flee
                    if (PowerUp())
                    {
                        ChangeToBlue();
                        Current = State.Flee;
                        break;
                    }

                    // Attack -> Scatter
                    if (Timer())
                    {
                        Current = State.Scatter;
                        break;
                    }
                    break;

                case State.Flee:
                    FleeFromPacman();

                    // Flee -> Attack
                    if (PowerDown())
                    {
                        ChangeToRed();
                        Current = State.Attack;
                        break;
                    }

                    // Flee -> Dead
                    if (Eaten())
                    {
                        ChangeToEyesOnly();
                        Current = State.Dead;
                        break;
                    }
                    break;

                case State.Dead:
                    GotoHome();

                    // Dead -> Scatter
                    if (BackHome())
                    {
                        ChangeToRed();
                        Current = State.Scatter;
                        break;
                    }
                    break;
            }

        }

        #region StateActions
        void GotoRandomPlace()
        {

        }

        void ChasePacman()
        {

        }

        void FleeFromPacman()
        {

        }

        void GotoHome()
        {

        }
        #endregion

        #region TransitionActions
        void ChangeToBlue ()
        {

        }

        void ChangeToEyesOnly ()
        {

        }

        void ChangeToRed ()
        {

        }
        #endregion

        #region TransitionConditions
        bool Timer()
        {
            return true;
        }

        bool PowerUp ()
        {
            return true;
        }

        bool PowerDown ()
        {
            return true;
        }

        bool Eaten ()
        {
            return true;
        }

        bool BackHome ()
        {
            return true;
        }
        #endregion

    }
}