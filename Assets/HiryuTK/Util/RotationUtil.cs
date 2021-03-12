using UnityEngine;
using System.Collections;
using TargetJoint2D = UnityEngine.TargetJoint2D;

/// <summary>
/// More like a collection of references of rotation scripts that I used in the
/// past.
/// </summary>
public static class RotationUtil
{
    public static Quaternion AngleFromRight(float angle, Vector3 forward)
        => Quaternion.AngleAxis(angle, forward);

    public static Quaternion FaceTowardsVelocity(Vector3 rbVelocity) =>
        Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0f, 0f, 90f) * rbVelocity);

    public static Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }

    public static Vector2 GetRandomDir()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public static Vector3 GetVectorFromAngle(int angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static float GetAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        while (n < 0) n += 360;

        return n;
    }

    public static Quaternion CreateQuaternionUsingRotationAroundXYZ(float xRotation,  float yRotation, float zRotation)
    {
        return Quaternion.Euler(xRotation, yRotation, zRotation);
    }

    public static Quaternion RotateAQuaternion(Quaternion baseRotation, Vector3 additionalRotation)
    {
        return baseRotation * Quaternion.Euler(additionalRotation);
    }

    public static Vector3 RotateAVector(Vector3 baseDirection, Quaternion additionalRotation)
    {
        //As long as the final multiplier is a vector, the result will be a vector.
        return additionalRotation * baseDirection;
    }

    public static Vector3 RotateAVector(Vector3 baseDirection, Vector3 rotationsAroundAxis)
    {
        return Quaternion.Euler(rotationsAroundAxis) * baseDirection;
    }

    public static Quaternion LookTowardsDirection(Vector3 forward, Vector3 up)
    {
        //This might seem redundant, but I use this wrapper because
        //1. I may mistakenly use Quaternion.Euler, which uses rotational values
        //inside a Vector3 instead of using it as a direction.
        //2. People could also use transform.LookAt (targetPosition), which can
        //create problems because it does not specify an up direction.
        //3. rotation.SetLookRotation is a third way, I think it's "bugged" 
        //(I could be wrong), because you can't go
        //transform.rotation.SetLookRotation(fwd, up), it only works by
        //assignment, ie.. Quaterni0000on r = r.SetLookRotatation(...), transform.rotation = r;
        return Quaternion.LookRotation(forward);
    }

    public static Quaternion QuaternionLerp(Quaternion q1, Quaternion q2, float t)
    {
        //Quaternion.Lerp/Slerp is more reliable than Quaternion.LookRotation(Vector2.Lerp)
        return Quaternion.Lerp(q1, q2, t);

    }

    public static Vector3 GetMouseWordPosition(float z = 10f)
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = z;
        return p;
    }

    public static float PingPongRotation(float min, float max, float pingPongSpeed)
    {
        return min + Mathf.PingPong(Time.time * pingPongSpeed, max);
    }

    public static Quaternion RotateRotwards(Vector3 currentDir, Vector3 targetDirection, float rotationSpeed, Vector3 fixedForwardAxis)
    {
        return Quaternion.LookRotation(
            fixedForwardAxis, 
            Vector3.RotateTowards(currentDir, targetDirection, 
                rotationSpeed * Time.deltaTime, 0f).normalized);
    }

    public static float RotateRotwards_GetRotationVelocity(Vector3 currentDir, Vector3 targetDirection, float rotationSpeed, Vector3 fixedForwardAxis)
    {
        return Vector3.Cross(targetDirection, currentDir).z * -rotationSpeed;
    }

    public static Quaternion RotateBackAndForth(float maxRotation, float speed)
    {
        return Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
    }
}