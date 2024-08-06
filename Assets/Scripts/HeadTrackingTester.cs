using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.XR;

public class HeadTrackingTester : MonoBehaviour
{
    private string filePath;

    private void Start()
    {
        // Define the file path for CSV logging
        filePath = Path.Combine(Application.persistentDataPath, "HeadTrackingB.csv");

        // Initialize the CSV file with headers
        InitializeCSV();
    }

    private void InitializeCSV()
    {
        // Write the header row if the file does not already exist
        if (!File.Exists(filePath))
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine("Timestamp,HeadPositionX,HeadPositionY,HeadPositionZ,HeadRotationX,HeadRotationY,HeadRotationZ");
            }
        }
    }

    private void Update()
    {
        LogHeadTrackingData();
    }

    private void LogHeadTrackingData()
    {
        // Get head tracking data
        InputDevice headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
        if (headDevice.isValid)
        {
            Vector3 headPosition;
            Quaternion headRotation;

            if (headDevice.TryGetFeatureValue(CommonUsages.devicePosition, out headPosition) &&
                headDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out headRotation))
            {
                // Prepare data for CSV
                string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string positionX = headPosition.x.ToString("F4");
                string positionY = headPosition.y.ToString("F4");
                string positionZ = headPosition.z.ToString("F4");
                string rotationX = headRotation.eulerAngles.x.ToString("F4");
                string rotationY = headRotation.eulerAngles.y.ToString("F4");
                string rotationZ = headRotation.eulerAngles.z.ToString("F4");

                // Log to CSV file
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"{timestamp},{positionX},{positionY},{positionZ},{rotationX},{rotationY},{rotationZ}");
                }
            }
        }
    }
}

