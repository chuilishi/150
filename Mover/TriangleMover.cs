// using System;
// using System.Collections;
// using System.Collections.Generic;
// using EasyButtons;
// using UnityEngine;
//
// public class TriangleMover : MoveBase
// {
//     public float turnTime = 1f;
//     [Header("上下左右")] public Vector2 originPos = Vector2.zero;
//     public Vector3 direction;
//     public float radius;
//     private float Timer;
//     private object timer;
//     public Transform parent;
//     private bool _hasParent;
//     
//     [Button]
//     public void AdjustPos()
//     {
//         if (parent != null)
//         {
//             transform.position = new Vector3(parent.position.x+originPos.x*radius,
//                 parent.position.y + originPos.y*radius, 0);
//         }
//     }
//     private void OnValidate()
//     {
//         AdjustPos();
//     }
//
//     public override void Start()
//     {
//         base.Start();
//         isMoving = true;
//         if (parent != null) _hasParent = true;
//         timer = turnTime;
//         stepLength = radius * Mathf.Sqrt(3) / turnTime;
//         AdjustPos();
//         StartCoroutine(MoveCo());
//         
//     }
//     
//     private void FixedUpdate()
//     {
//         if (isMoving)
//         {
//             Move();
//             var f = (float)timer;
//             timer = f-0.02f;
//         }
//     }
//     public void Move()
//     {
//         if (!isMoving) return;
//         transform.Translate(direction * (stepLength * Time.deltaTime));
//     }
//     IEnumerator MoveCo()
//     {
//         var baseVector = (parent.transform.position - transform.position).normalized;
//         while (true)
//         {
//             baseVector = (parent.transform.position - transform.position).normalized;
//             direction = Quaternion.Euler(0, 0, 30)*baseVector;
//             yield return new WaitUntil(() =>
//             {
//                 var t = (float)timer;
//                 if (t < 0)
//                 {
//                     timer = turnTime;
//                     return true;
//                 }
//                 return false;
//             });
//         }
//     }
// }
