using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SawArea : MonoBehaviour
{
      [SerializeField] private float damagePerSecond = 1f;
       [SerializeField] private float pushForceAway = 1f;
       [SerializeField] private float pushForceUp = 1f;

       private void OnTriggerEnter(Collider other)
       {
           Debug.Log("Player Hit");
           if (!other.TryGetComponent<BasicHealth>(out var health)) return;
           health.Damage(1);
           Debug.Log(health);
           var direction = other.transform.position - transform.position;
           direction.Normalize();
           other.GetComponent<Rigidbody>().AddForce(direction * pushForceAway + Vector3.up * pushForceUp,
               ForceMode.VelocityChange);
           other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
       }
}