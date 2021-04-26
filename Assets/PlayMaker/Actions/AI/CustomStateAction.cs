using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;

namespace MLIGames.AI.PlayMaker.Actions
{
    [ActionCategory("AA Custom")]
    [HutongGames.PlayMaker.Tooltip("Sample PlayMaker Action")]
    public class CustomStateAction : FsmStateAction
    {
        // public override void Init(FsmState state)
        // {
        //     base.Init(state);
        //     Debug.Log("CustomStateAction -> Init: State " + state.Name);
        // }

        // public override void InitEditor(Fsm fsmOwner)
        // {
        //     base.InitEditor(fsmOwner);
        //     Debug.Log("CustomStateAction -> InitEditor: FsmOwner " + fsmOwner.OwnerName);
        // }
        
        public override void OnPreprocess()
        {
            //Debug.Log("CustomStateAction -> OnPreprocess()");
        }
        
        public override void OnEnter()
        {
            Debug.Log("CustomStateAction -> OnEnter()");
        }
        
        public override void OnExit()
        {
            Debug.Log("CustomStateAction -> OnExit()");
        }
        
        public override void OnActionTargetInvoked(object targetObject)
        {
            Debug.Log("CustomStateAction -> OnActionTargetInvoked(object targetObject)");
        }

        #region Monobehaviour
        
        public override void Awake()
        {
            //Debug.Log("CustomStateAction -> Awake()");
        }
                
        public override void OnUpdate()
        {
            //Debug.Log("CustomStateAction -> OnUpdate()");
        }
                
        public override void OnFixedUpdate()
        {
            //.Log("CustomStateAction -> OnFixedUpdate()");
        }
                
        public override void OnLateUpdate()
        {
            //Debug.Log("CustomStateAction -> OnLateUpdate()");
        }
        
        #endregion        
        
        
        #region Values

        public override string AutoName() => (string) "CustomStateAction";
        
        public override float GetProgress() => 0.0f;
        
        public override bool Event(FsmEvent fsmEvent) => false;

        #endregion
        
        #region Collision

        public override void DoCollisionEnter(Collision collisionInfo)
        {
            Debug.Log("CustomStateAction -> DoCollisionEnter(Collision collisionInfo)");
        }

        public override void DoCollisionStay(Collision collisionInfo)
        {
            Debug.Log("CustomStateAction -> DoCollisionStay(Collision collisionInfo)");
        }

        public override void DoCollisionExit(Collision collisionInfo)
        {
            Debug.Log("CustomStateAction -> DoCollisionExit(Collision collisionInfo)");
        }
        
        public override void DoCollisionEnter2D(Collision2D collisionInfo)
        {
 
        }

        public override void DoCollisionStay2D(Collision2D collisionInfo)
        {

        }

        public override void DoCollisionExit2D(Collision2D collisionInfo)
        {

        }
        
        public override void DoParticleCollision(GameObject other)
        {

        }
        
        public override void DoControllerColliderHit(ControllerColliderHit collider)
        {
            Debug.Log("CustomStateAction -> DoControllerColliderHit(ControllerColliderHit collider)");
        }
        #endregion

        #region Trigger

        public override void DoTriggerEnter(Collider other)
        {
            Debug.Log("CustomStateAction -> DoTriggerEnter(Collider other)");
        }

        public override void DoTriggerStay(Collider other)
        {
            Debug.Log("CustomStateAction -> DoTriggerStay(Collider other)");
        }

        public override void DoTriggerExit(Collider other)
        {
            Debug.Log("CustomStateAction -> DoTriggerExit(Collider other)");
        }
        
        public override void DoTriggerEnter2D(Collider2D other)
        {
            
        }

        public override void DoTriggerStay2D(Collider2D other)
        {

        }

        public override void DoTriggerExit2D(Collider2D other)
        {

        }
        
        #endregion

        #region Joints

        public override void DoJointBreak(float force)
        {

        }

        public override void DoJointBreak2D(Joint2D joint)
        {

        }

        #endregion

        #region Animator
        public override void DoAnimatorMove()
        {

        }

        public override void DoAnimatorIK(int layerIndex)
        {

        }
        #endregion

        #region Debugging
        
        public override void OnGUI()
        {

        }
        public override void OnDrawActionGizmos()
        {

        }

        public override void OnDrawActionGizmosSelected()
        {

        }
        
        #endregion

    }
}