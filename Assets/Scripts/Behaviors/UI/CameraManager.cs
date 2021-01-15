using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Code copied from Unity Technologies
/// https://github.com/UnityTechnologies/InputSystem_Warriors/blob/V2/InputSystem_Warriors_Project/Assets/Scripts/Managers/CameraManager.cs
/// </summary>

public enum SinglePlayerCameraMode
{
    Stationary,
    FollowPlayer
}

public class CameraManager : Singleton<CameraManager>
{
    [Header("Cameras")]
    public GameObject mainCamera;

    public void SetupManager()
    {
        
    }

    public Camera GetGameplayCamera()
    {
        return mainCamera.GetComponent<Camera>();
    }

}
