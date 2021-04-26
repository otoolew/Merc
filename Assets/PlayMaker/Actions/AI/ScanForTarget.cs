using System.Linq;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("AI")]
    [Tooltip("Scans for a target.")]
    public class ScanForTarget : ComponentAction<VisionPerception>
    {
        [RequiredField]
        [CheckForComponent(typeof(VisionPerception))]
        [Tooltip("The VisionPerception Component.")]
        public FsmOwnerDefault gameObject;
        
        [Tooltip("Event to send if a target is in sight.")]
        public FsmEvent trueEvent;
		
        [Tooltip("Event to send if no target was found.")]
        public FsmEvent falseEvent;
		
        [Tooltip("Store the result in a Character variable.")]
        [UIHint(UIHint.Variable)]
        public FsmBool hasTarget;
        
        [Tooltip("Store the result in a Character variable.")]
        [UIHint(UIHint.Variable)]
        public FsmGameObject storeResult;
        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;
        private VisionPerception controller => cachedComponent;
        
        public override void Reset()
        {
            base.Reset();
            gameObject = null;
            trueEvent = null;
            falseEvent = null;
            storeResult = null;
            everyFrame = false;
        }
        
        public override void OnPreprocess()
        {
            //Debug.Log("CustomStateAction -> OnPreprocess()");
        }
        
        public override void OnEnter()
        {
            Debug.Log("CustomStateAction -> OnEnter()");
            PerformScan();
            if (!everyFrame)
            {
                Finish();
            }
        }
        
        public override void OnExit()
        {
            Debug.Log("CustomStateAction -> OnExit()");
        }
        
        public override void OnUpdate()
        {
            //PerformScan();

            //Debug.Log("ScanForTarget -> OnPreprocess()");
        }
        
        public override void OnActionTargetInvoked(object targetObject)
        {
            Debug.Log("CustomStateAction -> OnActionTargetInvoked(object targetObject)");
        }
        
        public void PerformScan()
        {
            if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
                return;
            
            if (controller.FindBestTargetFromList())
            {
                Debug.Log("ScanForTarget -> GetTarget " + controller.CurrentTarget.name);
                storeResult.Value = controller.CurrentTarget.gameObject;
                hasTarget.Value = controller.HasTarget;
                if (!(trueEvent is null))
                {
                    Fsm.Event(trueEvent);
                }
            }
            else
            {
                if (!(falseEvent is null))
                {
                    Fsm.Event(falseEvent);
                }
            }
        }
    }
}