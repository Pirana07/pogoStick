using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpanim : MonoBehaviour
{
  public bool _isGroundedanim;

private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Ground"))
    {
        _isGroundedanim = true;
    }
}

private void OnTriggerExit2D(Collider2D other)
{
    if (other.CompareTag("Ground"))
    {
        _isGroundedanim = false;
    }
}

}
