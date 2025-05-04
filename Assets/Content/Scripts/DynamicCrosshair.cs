using UnityEngine;

namespace Content.Scripts
{
    public class DynamicCrosshair : MonoBehaviour
    {
        public Transform leftBar;
        public Transform rightBar;
        public Transform upperBar;
        public Transform bottomBar;

        Vector3 leftBarDefaultPos;
        Vector3 rightBarDefaultPos;
        Vector3 upperBarDefaultPos;
        Vector3 bottomBarDefaultPos;


        void Awake()
        {
            leftBarDefaultPos = leftBar.localPosition;
            rightBarDefaultPos = rightBar.localPosition;
            upperBarDefaultPos = upperBar.localPosition;
            bottomBarDefaultPos = bottomBar.localPosition;
        }

        void Update()
        {
            var recoilValue = RecoilManager.Instance.CurrentRotation.magnitude * .25f + 1;
            
            leftBar.localPosition = leftBarDefaultPos * recoilValue;
            rightBar.localPosition = rightBarDefaultPos * recoilValue;
            upperBar.localPosition = upperBarDefaultPos * recoilValue;
            bottomBar.localPosition = bottomBarDefaultPos * recoilValue;
        }
    }
}
