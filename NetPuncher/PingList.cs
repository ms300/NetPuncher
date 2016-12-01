using System;
using System.Collections.Generic;
using System.Threading;

public class PingList
{
    public List<Action> Tasks = new List<Action>();

    public void Start()
    {
        for (var i = 0; i < 32; i++)
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