using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    public Manager udpReceive;

    [Header("Left Hand (21 points)")]
    public GameObject[] LeftHandPoints = new GameObject[21];
    public GameObject LeftWholeHand;

    [Header("Right Hand (21 points)")]
    public GameObject[] RightHandPoints = new GameObject[21];
    public GameObject RightWholeHand;

    // add Landmark World Positions for Gesture Detection 
    [HideInInspector] public Vector3[] LeftLandmarks = new Vector3[21];
    [HideInInspector] public Vector3[] RightLandmarks = new Vector3[21];


    void Update()
    {
        string raw = udpReceive.data;
        if (string.IsNullOrEmpty(raw)) return;

        if (!raw.Contains("L:") || !raw.Contains("R:")) return;

        raw = raw.Trim('[', ']');
        string[] both = raw.Split('|');

        string leftData = both[0].Replace("L:", "");
        string rightData = both[1].Replace("R:", "");

        UpdateHand(leftData, LeftHandPoints, LeftWholeHand, LeftLandmarks);
        UpdateHand(rightData, RightHandPoints, RightWholeHand, RightLandmarks);
    }


    // accepts landmark output array
    void UpdateHand(string csv, GameObject[] points, GameObject whole, Vector3[] landmarkArray)
    {
        if (string.IsNullOrEmpty(csv)) return;

        string[] nums = csv.Split(',');
        if (nums.Length != 63) return;

        float x0 = float.Parse(nums[5 * 3]) - float.Parse(nums[17 * 3]);
        float y0 = float.Parse(nums[5 * 3 + 1]) - float.Parse(nums[17 * 3 + 1]);
        float z0 = float.Parse(nums[5 * 3 + 2]) - float.Parse(nums[17 * 3 + 2]);

        float width_pix = Mathf.Sqrt(x0 * x0 + y0 * y0 + z0 * z0);

        float z_hand = 0.05f * width_pix + 7.5f;
        float hand_size = 200 / width_pix;

        whole.transform.localScale = new Vector3(hand_size, hand_size, hand_size);
        whole.transform.localPosition = new Vector3(0, 0, z_hand);

        // Update all 21 points
        for (int i = 0; i < 21; i++)
        {
            float x = 7 - (float.Parse(nums[i * 3]) / 100);
            float y = float.Parse(nums[i * 3 + 1]) / 100;
            float z = float.Parse(nums[i * 3 + 2]) / 100;

            Vector3 localPos = new Vector3(x, y, z);

            points[i].transform.localPosition = localPos;
            points[i].transform.localScale = new Vector3(0.5f / hand_size, 0.5f / hand_size, 0.5f / hand_size);

            landmarkArray[i] = points[i].transform.position;
        }
    }
}
