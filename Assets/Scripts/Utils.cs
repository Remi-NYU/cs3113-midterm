using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // Turns the given vector3 into a vector2 by disregarding z
    public static Vector2 Vec2ToVec3(Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }
}