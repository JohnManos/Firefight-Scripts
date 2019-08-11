using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    private static int points = 0;
	private static string lastActiveSceneName;

    public static int GetPoints()
    {    
		return points;
	}
    public static void SetPoints(int value)
	{
		points = value;
	}

	public static string GetLastActiveSceneName()
    {
        return lastActiveSceneName;
    }
	
    public static void SetLastActiveSceneName(string value) 
	{
		lastActiveSceneName = value;
	}
}