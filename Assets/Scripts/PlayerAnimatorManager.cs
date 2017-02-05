using UnityEngine;
using System.Collections;


namespace Com.MyCompany.MyGame
{
    public class PlayerAnimatorManager : Photon.PunBehaviour
    {

        //#region PUBLIC PROPERTIES
        public float DirectionDampTime = .25f;
        //#endregion


        #region MONOBEHAVIOUR MESSAGES

        private Animator animator;
        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
        }

        // Update is called once per frame
        void Update()
        {

            if (photonView.isMine == false && PhotonNetwork.connected == true)
            {
                return;
            }

            if (!animator)
            {
                return;
            }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (v < 0)
            {
                v = 0;
            }

            Debug.Log(h + " : " + v);

            animator.SetFloat("Speed", h * h + v * v);
            animator.SetFloat("Direction", h, DirectionDampTime, Time.deltaTime);

            // deal with Jumping
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            // only allow jumping if we are running.
            if (stateInfo.IsName("Base Layer.Run"))
            {
                // When using trigger parameter
                if (Input.GetButtonDown("Jump")) animator.SetTrigger("Jump");
            }

            if (Input.GetButtonDown("Fire1")) animator.SetTrigger("Hi");
        }


        #endregion
    }
}