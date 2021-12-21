using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RigiArcher.Magic{

    public class RopeMagic : MagicBase
    {
        protected override void UseMagic()
        {
            StartCoroutine(Foo());
        }

        private IEnumerator Foo()
        {
            yield return new WaitForSeconds(2);
            Debug.Log("Finish spell");
            SpellFinish.Invoke();
        }
    }

}
