using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class TimeEvent{
        public string eventName;
        public long timestamp;

        public TimeEvent(string eventName, long timestamp)
        {
            this.eventName = eventName;
            this.timestamp = timestamp;
        }
    }