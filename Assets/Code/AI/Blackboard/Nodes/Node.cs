using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLIGames.Blackboard
{
    public abstract class Node : ScriptableObject
    {
        public enum State { Running, Failure, Success }
        
        [SerializeField] private State state;
        public State CurrentState { get => state; set => state = value; }

        [SerializeField] private bool started;
        public bool Started { get => started; set => started = value; }
        
        [SerializeField] private string guid;
        public string GUID { get => guid; set => guid = value; }
        
        [SerializeField] private Vector2 position;
        public Vector2 Position { get => position; set => position = value; }
        
        public State Update() {

            if (!started) {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if (state != State.Running) {
                OnStop();
                started = false;
            }

            return state;
        }

        public virtual Node Clone() {
            return Instantiate(this);
        }

        public void Abort() {
            BehaviourTree.Traverse(this, (node) => {
                node.started = false;
                node.state = State.Running;
                node.OnStop();
            });
        }

        public virtual void OnDrawGizmos() { }
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();
    }   
}

