using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace RigiArcher.CharacterInput
{

    /// <summary>
    /// this interface will broadcast characterInput as Unity Event
    /// </summary>
    public interface ICharacterInputBroadcaster
    {
        public UnityEvent InputJumpEvent { get;}
        public UnityEvent InputFireEvent { get;}
        public UnityEvent InputAttackEvent { get;}
        public UnityEvent<Vector2> InputLookEvent { get;}
        public UnityEvent<Vector2> InputMoveEvent { get;}
    }

}

