using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigiArcher.MeshSocket{

    public class MeshSocketManager : MonoBehaviour
    {
        public enum SocketIdEnum{
            LeftHand,
        }

        Dictionary<SocketIdEnum, MeshSocket> _socketMap = new Dictionary<SocketIdEnum, MeshSocket>();

        private void Awake() {
            // populate socket map
            MeshSocket[] sockets = GetComponentsInChildren<MeshSocket>();
            foreach(MeshSocket socket in sockets){
                _socketMap.Add(socket.SocketId, socket);
            }
        }

        public void Attach(Transform objectTransform, SocketIdEnum socketId){
            _socketMap[socketId].Attach(objectTransform);
        }

    }

}
