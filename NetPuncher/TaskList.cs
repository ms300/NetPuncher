using System;
using System.Collections.Generic;
using System.Threading;

public class TaskList
{
    public List<Action> Tasks = new List<Action>();
    public int MaxThread = 128;
    public void Start()
    {
        for (var i = 0; i < MaxThread; i++)
            StartAsync();
    }

    public void StartAsync()
    {
        lock (Tasks)
        {
            if (Tasks.Count > 0)
            {
                var t = Tasks[Tasks.Count - 1];
                Tasks.Remove(t);
                ThreadPool.QueueUserWorkItem(h =>
                {
                    t();
                    StartAsync();
                });
            }
        }
    }
}