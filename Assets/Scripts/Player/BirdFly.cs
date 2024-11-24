using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFly : MonoBehaviour
{
    [SerializeField]
    Animator _anim;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
             _anim.SetBool("BirdAwake", true);
        }
    }
}
