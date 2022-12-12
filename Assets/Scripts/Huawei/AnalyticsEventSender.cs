using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using HuaweiMobileServices.Analytics;

public class AnalyticsEventSender : MonoBehaviour
{
    public void SendEvent(string eventName)
    {
        if (string.IsNullOrEmpty(eventName))
        {
            Debug.Log("[HMS]: Fill Fields");
        }
        else
        {
            HMSAnalyticsKitManager.Instance.SendEventWithBundle(eventName, "01", "01");
        }
    }
}
