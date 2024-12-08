using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AttitudeInput
{
    private static AttitudeInput instance = null;

    static public AttitudeInput GetInstance()
    {
        if (instance == null)
        {
            AttitudeInput.instance = new AttitudeInput();
        }

        return instance;
    }


    private AttitudeInput()
    {
        Input.gyro.enabled = true;
    }
    private Quaternion attitude => Input.gyro.attitude;

    private static float Pitch()
    {
        return NormalizedYAngle(GetInstance().attitude);
    }

    private static float Roll()
    {
        return NormalizedZAngle(GetInstance().attitude);
    }

    public static float GetPitchNormalized()
    {
        float pitch = Pitch();
        pitch += 30;
        if (pitch >= 180f)
        {
            //ex: 182 = 182 - 360 = -178
            pitch -= 360;
        }
        return Mathf.Clamp(ValueInRangeMapper.MapRange(pitch, 75, 105, -1, 1), -1f, 1f);
    }

    public static float GetRollNormalized()
    {
        float roll = Roll();
        float normalizedRoll = Mathf.Clamp(ValueInRangeMapper.MapRange(roll, 75, 105, -1, 1), -1f, 1f);
        //Debug.Log($"Normalized roll {normalizedRoll}");
        return normalizedRoll;
    }




    //credit for this method: https://stackoverflow.com/questions/42895305/unity-input-gyro-attitude-accuracy
    /// <summary>
    /// This method normalizes the y euler angle between 0 and 180. When the y euler
    /// angle crosses the 180 degree threshold if then starts to count back down to zero
    /// </summary>
    /// <param name="q">Some Quaternion</param>
    /// <returns>normalized Y euler angle</returns>
    private static float NormalizedXAngle(Quaternion q)
    {
        Vector3 eulers = q.eulerAngles;
        float xAngle = eulers.x;
        if (xAngle >= 180f)
        {
            //ex: 182 = 182 - 360 = -178
            xAngle -= 360;
        }
        return Mathf.Abs(xAngle);
    }

    //credit for this method: https://stackoverflow.com/questions/42895305/unity-input-gyro-attitude-accuracy
    /// <summary>
    /// This method normalizes the y euler angle between 0 and 180. When the y euler
    /// angle crosses the 180 degree threshold if then starts to count back down to zero
    /// </summary>
    /// <param name="q">Some Quaternion</param>
    /// <returns>normalized Y euler angle</returns>
    private static float NormalizedYAngle(Quaternion q)
    {
        Vector3 eulers = q.eulerAngles;
        float yAngle = eulers.y;
        if (yAngle >= 180f)
        {
            //ex: 182 = 182 - 360 = -178
            yAngle -= 360;
        }
        return Mathf.Abs(yAngle);
    }

    //credit for this method: https://stackoverflow.com/questions/42895305/unity-input-gyro-attitude-accuracy
    /// <summary>
    /// This method normalizes the y euler angle between 0 and 180. When the y euler
    /// angle crosses the 180 degree threshold if then starts to count back down to zero
    /// </summary>
    /// <param name="q">Some Quaternion</param>
    /// <returns>normalized Y euler angle</returns>
    private static float NormalizedZAngle(Quaternion q)
    {
        Vector3 eulers = q.eulerAngles;
        float zAngle = eulers.z;
        if (zAngle >= 180f)
        {
            //ex: 182 = 182 - 360 = -178
            zAngle -= 360;
        }
        return Mathf.Abs(zAngle);
    }
}
