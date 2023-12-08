using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public List<Target> _targetList = new List<Target>();

    private void OnTriggerEnter(Collider other) // Target in Range
    {
        if (other.TryGetComponent<Target>(out Target target))
            _targetList.Add(target);
    }

   private void OnTriggerExit(Collider other) // Target out range
    {
        if (other.TryGetComponent<Target>(out Target target))
            _targetList.Add(target);
    }
}
