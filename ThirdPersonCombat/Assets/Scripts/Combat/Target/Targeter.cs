using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


namespace TP.CombatSystem.Combat.Target
{
    public class Targeter : MonoBehaviour
    {
        [SerializeField] private CinemachineTargetGroup cinemachineTarGro;
        private List<Target> _targetList = new List<Target>();
        public Target CurrentTarget {get; private set;}

        private void OnTriggerEnter(Collider other) // Target in Range
        {
            if (other.TryGetComponent<Target>(out Target target))
            {
                _targetList.Add(target);
                target.OnDestroyedEvent += TargetOnDestroyHandler;
            }
                
        }

        private void OnTriggerExit(Collider other) // Target out range
        {
            if (other.TryGetComponent<Target>(out Target target))
            {
                TargetOnDestroyHandler(target);
            }
        }
        public bool SelectTarget()
        {
            if (_targetList.Count == 0) return false;
           CurrentTarget = _targetList[0];
            cinemachineTarGro.AddMember(CurrentTarget.transform,1f,2f);
           return true;
        }
        public void Cancel()
        {
            if(CurrentTarget == null) return;
            cinemachineTarGro.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }
        
        private void TargetOnDestroyHandler(Target target)
        {
            if(CurrentTarget == target)
            {
                cinemachineTarGro.RemoveMember(CurrentTarget.transform);
                CurrentTarget = null;
            }
            target.OnDestroyedEvent -= TargetOnDestroyHandler;
            _targetList.Remove(target);
        }
    }

}


