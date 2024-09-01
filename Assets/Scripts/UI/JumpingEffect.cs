using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEffect : MonoBehaviour
{
    
//     [SerializeField]bool _scale;
//     [SerializeField]bool _pos;
//     [SerializeField] private ParticleSystem particles;
//     [SerializeField] float jumpHeight = 1.2f; 
//     [SerializeField] float jumpHeight1 = 15f; 
//     [SerializeField]  float speed = 2f; 
//     Vector3 _originalScale;
//     RectTransform _rectTransform;
//     Vector2 _originalPosition;
//     bool _hasTriggeredParticles = false;


//     void Start()
//     {
//         _rectTransform = GetComponent<RectTransform>();
//         _originalPosition = _rectTransform.anchoredPosition;
//         _originalScale = transform.localScale; 
//     }
//     void Update()
//     {
//         if(_scale == true){
//         float scale = Mathf.PingPong(Time.time * speed, jumpHeight - 1) + 1;
//         transform.localScale = _originalScale * scale;
//         }
//         if(_pos == true){
//             float newY = _originalPosition.y + Mathf.Sin(Time.time * speed) * jumpHeight1;
//         _rectTransform.anchoredPosition = new Vector2(_originalPosition.x, newY);
//         particlesFun(newY);
//         }
        
//     }


// void particlesFun(float newY){
//      if (Mathf.Abs(newY - (_originalPosition.y - jumpHeight)) < 0.01f)
//         {
//             if (!_hasTriggeredParticles && particles != null)
//             {
//                 particles.Play();  // Trigger the particle effect
//                 _hasTriggeredParticles = true; // Prevent triggering multiple times in a single cycle
//             }
//         }
//         else
//         {
//             _hasTriggeredParticles = false; // Reset for the next cycle
//         }
// }
// i wasted Time T T

}