using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine.InputSystem;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private PhotonView photonView;

    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;

    [Header("Hand Animations")]
    [Space]
    public InputActionProperty left_PinchAnimationAction;
    public InputActionProperty left_GripAnimationAction;
    public InputActionProperty right_PinchAnimationAction;
    public InputActionProperty right_GripAnimationAction;
    [Space]
    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    private void Start()
    {
        photonView = GetComponent<PhotonView>(); // Get the PhotonView component attached to this GameObject

        XROrigin rig = FindObjectOfType<XROrigin>();
        headRig = rig.transform.Find("Camera Offset/Main Camera");
        leftHandRig = rig.transform.Find("Camera Offset/Left Controller");
        rightHandRig = rig.transform.Find("Camera Offset/Right Controller");

        if (photonView.IsMine)
        {
            // Disable rendering for the local player's objects
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            // Map the position and rotation of the head and hands using XR input device data
            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);

            // Update hand animations for the local player
            Left_HandAnimation();
            Right_HandAnimation();
        }
    }

    // Maps the position and rotation of a target transform using XR input device data from a rig transform
    void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }

    // Update left hand animations based on input actions
    void Left_HandAnimation()
    {
        float triggerValue = left_PinchAnimationAction.action.ReadValue<float>();
        leftHandAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = left_GripAnimationAction.action.ReadValue<float>();
        leftHandAnimator.SetFloat("Grip", gripValue);
    }

    // Update right hand animations based on input actions
    void Right_HandAnimation()
    {
        float triggerValue = right_PinchAnimationAction.action.ReadValue<float>();
        rightHandAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = right_GripAnimationAction.action.ReadValue<float>();
        rightHandAnimator.SetFloat("Grip", gripValue);
    }
}
