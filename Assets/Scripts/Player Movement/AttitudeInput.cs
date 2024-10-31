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
    
    public static float Pitch => AttitudeInput.NormalizedYAngle(AttitudeInput.GetInstance().attitude);
    public static float Roll => AttitudeInput.NormalizedZAngle(AttitudeInput.GetInstance().attitude);

    public static float PitchNormalized = ValueInRangeMapper.MapRange(AttitudeInput.Pitch, 0, 180, -1, 1);

    public static float RollNormalized = ValueInRangeMapper.MapRange(AttitudeInput.Roll, 0, 180, -1, 1);

    
    
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
        if(xAngle >= 180f)
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
            if(yAngle >= 180f)
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
                float zAngle = eulers.y;
                if(zAngle >= 180f)
                {
                    //ex: 182 = 182 - 360 = -178
                    zAngle -= 360;
                }
                return Mathf.Abs(zAngle);
            } 
}
