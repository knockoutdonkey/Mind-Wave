using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SetSunlight : MonoBehaviour
{


    private bool fetching;
    private SunData sunData;
    private LocationData locationData;
    private SunDataResults sunDatar;
    private bool ready = false;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(FetchLoc());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            this.GetComponent<Camera>().backgroundColor = getColor();
        }
    }

    private IEnumerator FetchLoc()
    {
        string api = "http://freegeoip.net/json/";
        Debug.Log(api);
        WWW apicall = new WWW(api);
        yield return apicall;
        locationData = JsonUtility.FromJson<LocationData>(apicall.text);

        api = "http://api.sunrise-sunset.org/json?lat=" + locationData.latitude + "&lng=" + locationData.longitude + "&date=today&formatted=0";
        Debug.Log(api);
        WWW apicall2 = new WWW(api);
        yield return apicall2;
        string s = apicall2.text.Substring(11);
        s = s.Substring(0, s.Length - 16);
        s =  s + "}\n";
        sunData = JsonUtility.FromJson<SunData>(s);
        ready = true;
    }

    private Color32 getColor()
    {
     
        DateTime now = DateTime.UtcNow;

        if(now < ConvertTime(sunData.astronomical_twilight_begin))
            return new Color32(29, 39, 66,255);
        if (now < ConvertTime(sunData.nautical_twilight_begin))
            return new Color32(59, 71, 116, 255);
        if (now < ConvertTime(sunData.civil_twilight_begin))
            return new Color32(71, 96, 155, 255);
        if (now < ConvertTime(sunData.sunrise))
            return new Color32(59, 180, 238, 255);
        if (now < ConvertTime(sunData.sunset))
            return new Color32(71, 96, 155, 255);
        if (now < ConvertTime(sunData.civil_twilight_end))
            return new Color32(59, 71, 116, 255);
        if (now < ConvertTime(sunData.nautical_twilight_end))
            return new Color32(29, 39, 66, 255);

        return Color.black;
    }

    private DateTime ConvertTime(string time)
    {
       return new DateTime(Convert.ToInt32(time.Substring(0, 4)), Convert.ToInt32(time.Substring(5, 2)), Convert.ToInt32(time.Substring(8, 2)), Convert.ToInt32(time.Substring(11, 2)), Convert.ToInt32(time.Substring(14, 2)), Convert.ToInt32(time.Substring(17, 2)));
    }

}

[System.Serializable]
public class SunDataResults
{
    public List<SunData> results;
}

[System.Serializable]
public class SunData
{
    public String sunrise;
    public String sunset;
    public String solar_noon;
    public String day_length;
    public String civil_twilight_begin;
    public String civil_twilight_end;
    public String nautical_twilight_begin;
    public String nautical_twilight_end;
    public String astronomical_twilight_begin;
    public String astronomical_twilight_end;
}

[System.Serializable]
public class LocationData
{
    public String ip;
    public String country_code;
    public String country_name;
    public String region_code;
    public String region_name;
    public String city;
    public String zip_code;
    public String time_zone;
    public String metro_cod;
    public float latitude;
    public float longitude;
}