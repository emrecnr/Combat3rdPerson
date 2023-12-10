using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TP.CombatSystem.Combat.Target
{
    public class Target : MonoBehaviour
    {
        public event Action<Target> OnDestroyedEvent;

        private void OnDestroy() {
            OnDestroyedEvent?.Invoke(this);
        }
    }
}
