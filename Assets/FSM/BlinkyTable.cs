using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementation of Pac-Man's ghost Blinky using FSM
// Matrix style
namespace AlanZucconi.Blinky.Table
{
    public enum State
    {
        None,
        Scatter,
        Attack,
        Flee,
        Dead
    }
    public enum Event
    {
        None,
        PowerUp,
        PowerDown,
        Eaten,
        BackHome,
        Timer
    }
    public class BlinkyTable : MonoBehaviour
    {

        public State[,] Table = new State[4, 5];
        public State Current = State.Attack;

        // Use this for initialization
        void Start()
        {

            Table[(int)State.Scatter, (int)Event.Timer] = State.Attack;

            Table[(int)State.Attack, (int)Event.Timer] = State.Scatter;
            Table[(int)State.Attack, (int)Event.PowerUp] = State.Flee;

            Table[(int)State.Flee, (int)Event.PowerDown] = State.Attack;
            Table[(int)State.Flee, (int)Event.Eaten] = State.Dead;

            Table[(int)State.Dead, (int)Event.BackHome] = State.Scatter;
        }

        // Update is called once per frame
        void Update()
        {
            //Update(Event.PowerDown);
        }


        // "Event" has happened
        public void Update(Event e)
        {
            State next = Table[(int)Current, (int)e];
            if (next == State.None)
                return;

            Current = next;
        }
    }
}