// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Shake : MonoBehaviour
// {
//     [SerializeField] private CameraFollow _cameraFollow; // Reference to the CameraFollow script
//     [SerializeField] private float x; 
//     [SerializeField] private float y; 
 

//     // Update is called once per frame
//      private void OnTriggerEnter2D(Collider2D other) {
//         if(other.tag == "Player"){
//         TriggerGroundShake();
//         }
//     }
    
//       void TriggerGroundShake()
//     {
//         if (_cameraFollow != null)
//         {
//             _cameraFollow.TriggerLandingShake(x, y); // Adjust the duration and magnitude as needed
//         }
//     }
// }
