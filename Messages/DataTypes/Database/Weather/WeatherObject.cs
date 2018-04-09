using System;

namespace Messages.DataTypes.Database.Weather
{
    public class WeatherObject
    {
        public WeatherInfo[] weather { get; set; } = null;
    }

    [Serializable]
    public class WeatherInfo
    {
        public DateTime LocalObservationDateTime { get; set; }
        public int EpochTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool IsDayTime { get; set; }
        public Temperature Temperature { get; set; }
        public Realfeeltemperature RealFeelTemperature { get; set; }
        public Realfeeltemperatureshade RealFeelTemperatureShade { get; set; }
        public int RelativeHumidity { get; set; }
        public Dewpoint DewPoint { get; set; }
        public Wind Wind { get; set; }
        public Windgust WindGust { get; set; }
        public int UVIndex { get; set; }
        public string UVIndexText { get; set; }
        public Visibility Visibility { get; set; }
        public string ObstructionsToVisibility { get; set; }
        public int CloudCover { get; set; }
        public Ceiling Ceiling { get; set; }
        public Pressure Pressure { get; set; }
        public Pressuretendency PressureTendency { get; set; }
        public Past24hourtemperaturedeparture Past24HourTemperatureDeparture { get; set; }
        public Apparenttemperature ApparentTemperature { get; set; }
        public Windchilltemperature WindChillTemperature { get; set; }
        public Wetbulbtemperature WetBulbTemperature { get; set; }
        public Precip1hr Precip1hr { get; set; }
        public Precipitationsummary PrecipitationSummary { get; set; }
        public Temperaturesummary TemperatureSummary { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    [Serializable]
    public class Temperature
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Metric
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    [Serializable]
    public class Imperial
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    [Serializable]
    public class Realfeeltemperature
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Realfeeltemperatureshade
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Dewpoint
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Wind
    {
        public Direction Direction { get; set; }
        public Speed Speed { get; set; }
    }

    [Serializable]
    public class Direction
    {
        public int Degrees { get; set; }
        public string Localized { get; set; }
        public string English { get; set; }
    }

    [Serializable]
    public class Speed
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Windgust
    {
        public Speed Speed { get; set; }
    }

    [Serializable]
    public class Visibility
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Ceiling
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Pressure
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Pressuretendency
    {
        public string LocalizedText { get; set; }
        public string Code { get; set; }
    }

    [Serializable]
    public class Past24hourtemperaturedeparture
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Apparenttemperature
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Windchilltemperature
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Wetbulbtemperature
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Precip1hr
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Precipitationsummary
    {
        public Precipitation Precipitation { get; set; }
        public Pasthour PastHour { get; set; }
        public Past3hours Past3Hours { get; set; }
        public Past6hours Past6Hours { get; set; }
        public Past9hours Past9Hours { get; set; }
        public Past12hours Past12Hours { get; set; }
        public Past18hours Past18Hours { get; set; }
        public Past24hours Past24Hours { get; set; }
    }

    [Serializable]
    public class Precipitation
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Pasthour
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Past3hours
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Past6hours
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Past9hours
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Past12hours
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Past18hours
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Past24hours
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Temperaturesummary
    {
        public Past6hourrange Past6HourRange { get; set; }
        public Past12hourrange Past12HourRange { get; set; }
        public Past24hourrange Past24HourRange { get; set; }
    }

    [Serializable]
    public class Past6hourrange
    {
        public Minimum Minimum { get; set; }
        public Maximum Maximum { get; set; }
    }

    [Serializable]
    public class Minimum
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Maximum
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    [Serializable]
    public class Past12hourrange
    {
        public Minimum Minimum { get; set; }
        public Maximum Maximum { get; set; }
    }

    [Serializable]
    public class Past24hourrange
    {
        public Minimum Minimum { get; set; }
        public Maximum Maximum { get; set; }
    }
}





