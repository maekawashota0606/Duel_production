using System.Collections;
using UnityEngine;

public class InputProvider : SingletonMonoBehaviour<InputProvider>, IInput
{
    public enum FireType
    {
        Circle,
        Cross,
        Square,
        Triangle
    }

    #region デバッグ用マウスドラッグ距離算出関連
#if UNITY_EDITOR
    private Vector3 mouseOrigin1P = Vector3.zero;
    private Vector3 mouseOrigin2P = Vector3.zero;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            mouseOrigin1P = Input.mousePosition;
        if (Input.GetMouseButtonDown(1))
            mouseOrigin2P = Input.mousePosition;
    }
#endif
    #endregion

    #region 1P
#if UNITY_EDITOR


    public bool GetFireDown1P(FireType type)
    {
        switch (type)
        {
            case FireType.Circle:
                return Input.GetKeyDown(KeyCode.D);
            case FireType.Cross:
                return Input.GetKeyDown(KeyCode.S);
            case FireType.Square:
                return Input.GetKeyDown(KeyCode.A);
            case FireType.Triangle:
                return Input.GetKeyDown(KeyCode.W);
            default:
                return false;
        }
    }

    public bool GetFireUp1P(FireType type)
    {
        switch (type)
        {
            case FireType.Circle:
                return Input.GetKeyUp(KeyCode.D);
            case FireType.Cross:
                return Input.GetKeyUp(KeyCode.S);
            case FireType.Square:
                return Input.GetKeyUp(KeyCode.A);
            case FireType.Triangle:
                return Input.GetKeyUp(KeyCode.W);
            default:
                return false;
        }
    }

    public bool GetFire1P(FireType type)
    {
        switch (type)
        {
            case FireType.Circle:
                return Input.GetKey(KeyCode.D);
            case FireType.Cross:
                return Input.GetKey(KeyCode.S);
            case FireType.Square:
                return Input.GetKey(KeyCode.A);
            case FireType.Triangle:
                return Input.GetKey(KeyCode.W);
            default:
                return false;
        }
    }


    public Vector3 GetLeftStick1P()
    {
        if (Input.GetMouseButton(0))
            return (mouseOrigin1P - Input.mousePosition).normalized;
        else
            return Vector3.zero;
    }

    public Vector3 GetRightStick1P()
    {
        // 一旦左右同じで
        if (Input.GetMouseButton(0))
            return (mouseOrigin1P - Input.mousePosition).normalized;
        else
            return Vector3.zero;
    }

#else
    public bool GetFireDown1P(FireType type)
    {
        switch (type)
        {
            case FireType.Circle:
                return Input.GetButtonDown("Circle_0");
            case FireType.Cross:
                return Input.GetButtonDown("Cross_0");
            case FireType.Square:
                return Input.GetButtonDown("Square_0");
            case FireType.Triangle:
                return Input.GetButtonDown("Triangle_0");
            default:
                return false;
        }
    }

    public bool GetFireUp1P(FireType type)
    {
        switch (type)
        {
            case FireType.Circle:
                return Input.GetButtonUp("Circle_0");
            case FireType.Cross:
                return Input.GetButtonUp("Cross_0");
            case FireType.Square:
                return Input.GetButtonUp("Square_0");
            case FireType.Triangle:
                return Input.GetButtonUp("Triangle_0");
            default:
                return false;
        }
    }

    public bool GetFire1P(FireType type)
    {
        switch (type)
        {
            case FireType.Circle:
                return Input.GetButton("Circle_0");
            case FireType.Cross:
                return Input.GetButton("Cross_0");
            case FireType.Square:
                return Input.GetButton("Square_0");
            case FireType.Triangle:
                return Input.GetButton("Triangle_0");
            default:
                return false;
        }
    }

    public Vector3 GetLeftStick1P()
    {
        float x = Input.GetAxis("LStick_Horizontal_0");
        float y = Input.GetAxis("LStick_Vertical_0");
        return new Vector3(x, y);
    }

    public Vector3 GetRightStick1P()
    {
        float x = Input.GetAxis("RStick_Horizontal_0");
        float y = Input.GetAxis("RStick_Vertical_0");
        return new Vector3(x, y);
    }
#endif
    #endregion



#region 2P
#if UNITY_EDITOR
    public bool GetFireDown2P(FireType type)
    {
        switch (type)
        {
            case FireType.Circle:
                return Input.GetKeyDown(KeyCode.L);
            case FireType.Cross:
                return Input.GetKeyDown(KeyCode.K);
            case FireType.Square:
                return Input.GetKeyDown(KeyCode.J);
            case FireType.Triangle:
                return Input.GetKeyDown(KeyCode.I);
            default:
                return false;
        }
    }

    public bool GetFireUp2P(FireType type)
    {
        switch (type)
        {
            case FireType.Circle:
                return Input.GetKeyUp(KeyCode.L);
            case FireType.Cross:
                return Input.GetKeyUp(KeyCode.K);
            case FireType.Square:
                return Input.GetKeyUp(KeyCode.J);
            case FireType.Triangle:
                return Input.GetKeyUp(KeyCode.I);
            default:
                return false;
        }
    }

    public bool GetFire2P(FireType type)
    {
        switch (type)
        {
            case FireType.Circle:
                return Input.GetKey(KeyCode.L);
            case FireType.Cross:
                return Input.GetKey(KeyCode.K);
            case FireType.Square:
                return Input.GetKey(KeyCode.J);
            case FireType.Triangle:
                return Input.GetKey(KeyCode.I);
            default:
                return false;
        }
    }

    public Vector3 GetLeftStick2P()
    {
        if (Input.GetMouseButton(1))
            return (mouseOrigin2P - Input.mousePosition).normalized;
        else
            return Vector3.zero;
    }

    public Vector3 GetRightStick2P()
    {
        // 一旦左右同じで
        if (Input.GetMouseButton(1))
            return (mouseOrigin2P - Input.mousePosition).normalized;
        else
            return Vector3.zero;
    }
#else
    public bool GetFireDown2P(FireType type)
    {
        switch (type)
        {
            case FireType.Circle:
                return Input.GetButtonDown("Circle_1");
            case FireType.Cross:
                return Input.GetButtonDown("Cross_1");
            case FireType.Square:
                return Input.GetButtonDown("Square_1");
            case FireType.Triangle:
                return Input.GetButtonDown("Triangle_1");
            default:
                return false;
        }
    }

    public bool GetFireUp2P(FireType type)
    {
        switch (type)
        {
            case FireType.Circle:
                return Input.GetButtonUp("Circle_1");
            case FireType.Cross:
                return Input.GetButtonUp("Cross_1");
            case FireType.Square:
                return Input.GetButtonUp("Square_1");
            case FireType.Triangle:
                return Input.GetButtonUp("Triangle_1");
            default:
                return false;
        }
    }

    public bool GetFire2P(FireType type)
    {
        switch (type)
        {
            case FireType.Circle:
                return Input.GetButton("Circle_1");
            case FireType.Cross:
                return Input.GetButton("Cross_1");
            case FireType.Square:
                return Input.GetButton("Square_1");
            case FireType.Triangle:
                return Input.GetButton("Triangle_1");
            default:
                return false;
        }
    }

    public Vector3 GetLeftStick2P()
    {
        float x = Input.GetAxis("LStick_Horizontal_1");
        float y = Input.GetAxis("LStick_Vertical_1");
        return new Vector3(x, y);
    }

    public Vector3 GetRightStick2P()
    {
        float x = Input.GetAxis("RStick_Horizontal_1");
        float y = Input.GetAxis("RStick_Vertical_1");
        return new Vector3(x, y);
    }
#endif
    #endregion
}