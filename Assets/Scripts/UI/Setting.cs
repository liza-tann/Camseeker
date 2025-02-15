using UnityEngine.Events;
using UnityEngine;

namespace PlayerController
{

    public class Setting : MonoBehaviour
    {

        [SerializeField] private UnityEvent[] Default_Values;

        public void Default()
        {
            foreach (var item in Default_Values)
            {
                item?.Invoke();
            }
        }

    }

}
