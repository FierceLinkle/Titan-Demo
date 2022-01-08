using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AlanZucconi.AI.FSM;

// Implementation of Pac-Man's ghost Blinky using FSM
// FSM library
namespace AlanZucconi.Blinky.FSM
{
    public enum State
    {
        Scatter,
        Attack,
        Flee,
        Dead
    }

    public class BlinkyFSM : MonoBehaviour
    {
        // The state machine
        public FiniteStateMachine<State> FSM;

        // Use this for initialization
        void Start()
        {
            // Initialises the state machine
            FSM = new FiniteStateMachine<State>(State.Scatter);

            // State actions
            FSM[State.Scatter] = GotoRandomPlace;
            FSM[State.Attack ] = ChasePacman;
            FSM[State.Flee   ] = FleeFromPacman;
            FSM[State.Dead   ] = GotoHome;

            // Could also be done like this:
            //FSM.AddAction(State.Scatter, GotoRandomPlace);
            //FSM.AddAction(State.Attack,  ChasePacman);
            //FSM.AddAction(State.Flee,    FleeFromPacman);
            //FSM.AddAction(State.Dead,    GotoHome);

            // State transitions (with no actions associated)
            FSM[State.Scatter, State.Attack] = Timer;

            //FSM[State.Attack, State.Flee   ] = PowerUp;
            FSM[State.Attack, State.Scatter] = Timer;

            //FSM[State.Flee, State.Attack] = PowerDown;
            //FSM[State.Flee, State.Dead  ] = Eaten;

            //FSM[State.Dead, State.Scatter] = BackHome;

            // State transitions (with actions associated)
            FSM.AddTransition(State.Attack, State.Flee, PowerUp, ChangeToBlue);

            FSM.AddTransition(State.Flee, State.Attack, PowerDown, ChangeToRed);
            FSM.AddTransition(State.Flee, State.Dead,   Eaten,     ChangeToEyesOnly);

            FSM.AddTransition(State.Dead, State.Scatter, BackHome, ChangeToRed);
        }

        // Update is called once per frame
        void Update()
        {
            FSM.Update();
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
        void ChangeToBlue()
        {

        }

        void ChangeToEyesOnly()
        {

        }

        void ChangeToRed()
        {

        }
        #endregion

        #region TransitionConditions
        bool Timer()
        {
            return true;
        }

        bool PowerUp()
        {
            return true;
        }

        bool PowerDown()
        {
            return true;
        }

        bool Eaten()
        {
            return true;
        }

        bool BackHome()
        {
            return true;
        }
        #endregion

    }
}