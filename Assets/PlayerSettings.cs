using UnityEngine;

[CreateAssetMenu(fileName = "Player Settings")]
public class PlayerSettings : ScriptableObject
{
    float lookSpeed = 2;

    public void SetLookSpeed(float value)
    {
        this.lookSpeed = value;
        Debug.Log("Set Look speed " + this.lookSpeed);
    }

    public float GetLookSpeed()
    {
        return this.lookSpeed;
    }
}
