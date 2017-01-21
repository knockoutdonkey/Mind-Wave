using UnityEngine;

public static class Logger {

    public static bool isOn = true;
    
    public static void Log(object message) {
        if (isOn) {
            Debug.Log(message.ToString());
        }
    }
}
