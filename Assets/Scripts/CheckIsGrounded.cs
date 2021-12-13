using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigiArcher{

    public class CheckIsGrounded : MonoBehaviour, ICheckIsGrounded
    {
        [Header("Setting")]
        [SerializeField] float _groundedOffset;
        [SerializeField] float _groundedRadius;
        [SerializeField] LayerMask _groundLayers;
        [Tooltip("How much sec to wait to set isGrounded false when not on ground")]
        [SerializeField] float _groundedExtentTime;
        
        [Header("State")]
        [SerializeField] bool _isGround;
        [SerializeField] float _extentDelta = 0f;
        public bool IsGrounded {get=>_isGround; private set=> _isGround = value;}


        private void FixedUpdate() {
            // set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _groundedOffset, transform.position.z);
			bool nowIsGrounded = Physics.CheckSphere(spherePosition, _groundedRadius, _groundLayers, QueryTriggerInteraction.Ignore);

            if(nowIsGrounded){
                IsGrounded = true;
                _extentDelta = 0f;
            }
            else{
                if(IsGrounded == true){
                    _extentDelta += Time.fixedDeltaTime;
                    if(_extentDelta >= _groundedExtentTime){
                        IsGrounded = false;
                    }
                }
            }
        }


        private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (IsGrounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;
			
			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - _groundedOffset, transform.position.z), _groundedRadius);
		}
        
    }

    public interface ICheckIsGrounded{
        public bool IsGrounded{get;}
    }

}
