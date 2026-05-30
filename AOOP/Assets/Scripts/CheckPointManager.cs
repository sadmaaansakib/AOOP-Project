using UnityEngine;

public static class CheckpointManager
{
    public static void SaveCheckpoint(Vector3 position)
    {
        PlayerPrefs.SetFloat("CheckpointX", position.x);
        PlayerPrefs.SetFloat("CheckpointY", position.y);
        PlayerPrefs.SetFloat("CheckpointZ", position.z);
        PlayerPrefs.Save();
    }

    public static Vector3 LoadCheckpoint()
    {
        float x = PlayerPrefs.GetFloat("CheckpointX", 0);
        float y = PlayerPrefs.GetFloat("CheckpointY", 0);
        float z = PlayerPrefs.GetFloat("CheckpointZ", 0);
        return new Vector3(x, y, z);
    }

    public static bool HasCheckpoint()
    {
        return PlayerPrefs.HasKey("CheckpointX");
    }
}
