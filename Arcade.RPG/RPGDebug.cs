namespace Arcade.RPG;

using global::System;
using global::System.Diagnostics;

public class RPGDebug {
    /* Reflectively print file name, line number, and timestamp */
    public void Log(string message) {
        var stackFrame = new StackFrame(1, true);
        var fileName = stackFrame.GetFileName();
        var lineNumber = stackFrame.GetFileLineNumber();
        long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        
        fileName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
        string prefix = $"{fileName}:{lineNumber}";

        string text = $"{prefix} [{now}]: {message}";

        Debug.WriteLine(text);
    }
}