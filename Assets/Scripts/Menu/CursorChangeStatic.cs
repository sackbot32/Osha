using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CursorChangeStatic
{
    public static Texture2D selectCursor;
    public static Texture2D aimCursor;

    private static Vector2 selectCursorLock = Vector2.zero;
    private static Vector2 aimCursorLock = new Vector2(16f,16f);

    public static bool isAim;


    public static void SetTextures(Texture2D select,Texture2D aim)

    {
        selectCursor = select;
        aimCursor = aim;
    }
    /// <summary>
    /// Changes the cursor to the menu kind of cursor
    /// </summary>
    public static void ChangeToSelectCursor()
    {
        isAim = false;
        Cursor.SetCursor(selectCursor, selectCursorLock, CursorMode.Auto);
    } 
    /// <summary>
    /// Changes the cursor to a crosshair
    /// </summary>
    public static void ChangeToAimCursor()
    {
        isAim = true;
        Cursor.SetCursor(aimCursor, aimCursorLock, CursorMode.Auto);
    }


}
