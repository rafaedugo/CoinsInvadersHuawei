using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HmsPlugin;
using Huawei.Scripts.Location;
using HuaweiMobileServices.Location;
using HuaweiMobileServices.Location.Location;
using TMPro;
public class LocationKit : MonoBehaviour
{
    public GameObject objGroup;
    private static string TAG = "GeofenceDemo";
    public Text debugText;

    //[SerializeField] private Text resultText;
    [SerializeField] private TextMeshProUGUI latitudeText;
    [SerializeField] private TextMeshProUGUI longitudeText;

    private double latitude;
    private double longitude;
    private FusedLocationProviderClient fusedLocationProviderClient;
    private LocationRequest locationRequest;
    private LocationCallback locationCallback;
    private void Awake()
    {
        //HMSPushKitManager.Instance.Init();
        HMSLocationManager.Instance.RequestFineLocationPermission();
        HMSLocationManager.Instance.RequestCoarseLocationPermission();
        HMSLocationManager.Instance.RequestBackgroundLocationPermissions();


        //HMSLocationManager.Instance.onLocationResult += OnLocationResult;
        GetLocation();
    }
    private void OnEnable()
    {
        HMSLocationManager.Instance.onLocationResult += OnLocationResult;
    }
    private void OnDisable()
    {
        HMSLocationManager.Instance.onLocationResult -= OnLocationResult;
    }
    /*void Start()
    {
        //HMSPushKitManager.Instance.Init();
        HMSLocationManager.Instance.RequestFineLocationPermission();
        HMSLocationManager.Instance.RequestCoarseLocationPermission();
        HMSLocationManager.Instance.RequestBackgroundLocationPermissions();


        HMSLocationManager.Instance.onLocationResult += OnLocationResult;
        GetLocation();
    }*/
    public void GetLocation()
    {
        debugText.text = "Location";
        HMSLocationManager.Instance.DefineLocationCallback();

        fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient();
        locationRequest = new LocationRequest();
        locationRequest.SetInterval(10000);
        locationRequest.SetNumUpdates(1);

        if (locationCallback == null) locationCallback = HMSLocationManager.Instance.DefineLocationCallback();

        fusedLocationProviderClient
            .RequestLocationUpdates(locationRequest, locationCallback, Looper.GetMainLooper())
            .AddOnSuccessListener(
                (update) => { Debug.Log($"{TAG} RequestLocationUpdates success"); debugText.text = "Susses"; })
            .AddOnFailureListener((exception) =>
            {
                Debug.LogError($"{TAG} RequestLocationUpdates Fail " + exception.WrappedCauseMessage + " " +
                               exception.WrappedExceptionMessage + " HMS RequestLocationUpdates Error code: " +
                               exception.ErrorCode);
                objGroup.SetActive(false);
                debugText.text = "ExceptionMessage";
            });
    }
    private void OnLocationResult(LocationResult locationResult)
    {

        debugText.text = "1";
        objGroup.SetActive(true);


        Debug.Log($"{TAG} OnLocationResult success");

        var location = locationResult.GetLastLocation();
        debugText.text = "2";
        if (location.GetLatitude().GetType() == latitude.GetType() && location.GetLongitude().GetType() == longitude.GetType())
        {
            latitude = location.GetLatitude();
            longitude = location.GetLongitude();
            debugText.text = "3 n go";
        }
        else
        {
            objGroup.SetActive(false);
            debugText.text = "fail in 3";
        }

        debugText.text = "4";
        latitudeText.text = latitude.ToString();
        longitudeText.text = longitude.ToString();
        debugText.text = "5";
    }


}
