using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigiArcher.MeshSocket{

    public class MeshSocket : MonoBehaviour
    {
        public MeshSocketManager.SocketIdEnum SocketId;

        // reference
        Transform _attachPoint;

        private void Start() {
            _attachPoint = transform.GetChild(0);
        }

        public void Attach(Transform objectTransform){
            objectTransform.SetParent(_attachPoint, false);
        }
    }

}
