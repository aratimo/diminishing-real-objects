using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.XR;

public class HeadTrackingController : MonoBehaviour
{
    private string filePath;

    private void Start()
    {
        // Define the file path for CSV logging
        filePath = Path.Combine(Application.persistentDataPath, "HeadTrackingData.csv");

        // Initialize the CSV file with headers
        InitializeCSV();

        // Start the coroutine to log data every 2 seconds
        StartCoroutine(StartLogging());
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

    private IEnumerator StartLogging()
    {
        while (true)
        {
            LogHeadTrackingData();
            yield return new WaitForSeconds(5f); // Wait for 2 seconds
        }
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
                string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string positionX = headPosition.x.ToString("F4");
                string positionY = headPosition.y.ToString("F4");
                string positionZ = headPosition.z.ToString("F4");
                string rotationX = headRotation.eulerAngles.x.ToString("F4");
                string rotationY = headRotation.eulerAngles.y.ToString("F4");
                string rotationZ = headRotation.eulerAngles.z.ToString("F4");

                // Log to console
                string logTag = "HeadTracking";
                string logMessage = $"Head Position: ({positionX}, {positionY}, {positionZ}) Head Rotation: ({rotationX}, {rotationY}, {rotationZ})";
                Debug.Log($"[{logTag}] {logMessage}");

                // Log to CSV file
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"{timestamp},{positionX},{positionY},{positionZ},{rotationX},{rotationY},{rotationZ}");
                }
            }
        }
    }
}