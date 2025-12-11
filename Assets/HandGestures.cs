using UnityEngine;

public class HandGestures : MonoBehaviour
{
    public HandTracking tracker;       
    public Transform vrCamera;           // Gyro camera

    public float pinchThreshold = 0.035f;
    public float openThreshold = 0.09f;
    public float grabRadius = 0.2f;           
    public float pushForceMultiplier = 0.8f;
    public float throwForceMultiplier = 1.5f; 

    private Rigidbody leftHeld = null;
    private Rigidbody rightHeld = null;

    private Vector3 leftPrevPalm;
    private Vector3 rightPrevPalm;

    private Vector3 leftPalmVelocity;
    private Vector3 rightPalmVelocity;

    void Update()
    {
        HandleHand(tracker.LeftLandmarks, ref leftHeld, ref leftPrevPalm, ref leftPalmVelocity, "Left");
        HandleHand(tracker.RightLandmarks, ref rightHeld, ref rightPrevPalm, ref rightPalmVelocity, "Right");
    }

    void HandleHand(Vector3[] lm, ref Rigidbody held, ref Vector3 prevPalm, ref Vector3 palmVelocity, string handName)
    {
        if (lm == null || lm.Length < 21 || lm[0] == Vector3.zero)
            return;

        Vector3 thumb = lm[4];
        Vector3 index = lm[8];
        Vector3 palm = lm[0];

        float pinchDist = Vector3.Distance(thumb, index);

        // Calculate palm velocity
        palmVelocity = (palm - prevPalm) / Time.deltaTime;

        // pinch
        if (held == null && pinchDist < pinchThreshold)
        {
            TryGrab(palm, lm, ref held, handName);
        }

        // release
        if (held != null && pinchDist > openThreshold)
        {
            TryRelease(ref held, palmVelocity, handName);
        }

        // push
        if (palmVelocity.z < -1.2f) 
        {
            TryPush(palm, lm, palmVelocity, handName);
        }

        Debug.Log($"{handName} PinchDist = {pinchDist:F3}");

        prevPalm = palm;
    }

    void TryGrab(Vector3 palm, Vector3[] lm, ref Rigidbody held, string handName)
    {
        if (held != null) return;

        Vector3 forwardDir = (lm[12] - palm).normalized;

       
        Collider[] colliders = Physics.OverlapSphere(palm, grabRadius);
        float minDist = float.MaxValue;
        Rigidbody closest = null;

        foreach (var col in colliders)
        {
            Rigidbody rb = col.attachedRigidbody;
            if (rb != null && !rb.isKinematic)
            {
                float dist = Vector3.Distance(palm, rb.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = rb;
                }
            }
        }

        
        if (closest != null || Vector3.Distance(lm[4], lm[8]) < 1.0f)
        {
            
            if (closest == null && colliders.Length > 0)
                closest = colliders[0].attachedRigidbody;

            if (closest != null)
            {
                held = closest;
                held.isKinematic = true;
                held.transform.SetParent(vrCamera.transform); 
                Debug.Log($"{handName} grabbed {held.name} (instant pinch)");
            }
        }
    }


    void TryRelease(ref Rigidbody held, Vector3 palmVelocity, string handName)
    {
        if (held != null)
        {
            held.transform.SetParent(null);
            held.isKinematic = false;

            held.linearVelocity = palmVelocity * throwForceMultiplier;

            Debug.Log($"{handName} released {held.name} with velocity {held.linearVelocity}");
            held = null;
        }
    }

    void TryPush(Vector3 palm, Vector3[] lm, Vector3 velocity, string handName)
    {
        Vector3 forwardDir = (lm[12] - palm).normalized;

        if (Physics.Raycast(palm, forwardDir, out RaycastHit hit, grabRadius))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddForce(forwardDir * Mathf.Abs(velocity.z) * pushForceMultiplier, ForceMode.Impulse);
                Debug.Log($"{handName} pushed {rb.name}");
            }
        }
    }
}
