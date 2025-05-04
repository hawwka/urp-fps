using UnityEngine;

namespace Content.Scripts
{
    public class RecoilManager : MonoBehaviour
    {
        public static RecoilManager Instance;


        Vector3 currentRotation;
        Vector3 targetRotation;
        
        
        [SerializeField] float recoilX;
        [SerializeField] float recoilY;
        [SerializeField] float recoilZ;

        [SerializeField] float snappiness;
        [SerializeField] float returnSpeed;
        
        
        void Awake()
        {
            if (Instance)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }

        void LateUpdate()
        {
            targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
            currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedTime);
            
            transform.localRotation = Quaternion.Euler(currentRotation);
        }


        public void ApplyRecoil()
        {
            targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
        }
    }
}