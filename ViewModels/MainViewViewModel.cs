using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Threading.Tasks;
using ForzaDashboard.Core;
using System.Net.Sockets;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics.Metrics;
using Avalonia.Media;

namespace ForzaDashboard.ViewModels;

public partial class MainViewViewModel : ObservableObject
{
    [ObservableProperty]
    Data? dat;

    [ObservableProperty]
    int? rpm;

    [ObservableProperty]
    int rpmFull = 0;

    [ObservableProperty]
    string gear = "0";

    [ObservableProperty]
    string speed = "0";

    [ObservableProperty]
    int rpmSweepAngle = 0;

    [ObservableProperty]
    ushort lapNum = 0;

    [ObservableProperty]
    byte position;

    [ObservableProperty]
    int steerStartAngle = 0;

    [ObservableProperty]
    string bestLapTime = "0";

    [ObservableProperty]
    string currentLapTime = "0";

    [ObservableProperty]
    string lastLapTime = "0";

    [ObservableProperty]
    string raceTime = "0";

    [ObservableProperty]
    int accelHeight = 0;

    [ObservableProperty]
    int brakeHeight = 0;

    [ObservableProperty]
    string boost;

    [ObservableProperty]
    string clutchPercentage;

    [ObservableProperty]
    string handBrakePercentage;

    [ObservableProperty]
    string tireColorFL;

    [ObservableProperty]
    string tireColorFR;

    [ObservableProperty]
    string tireColorBL;

    [ObservableProperty]
    string tireColorBR;

    UdpClient udpClient;

    public MainViewViewModel()
    {
        udpClient = new UdpClient(8080);
        int counter = 0;
        Task.Run(async () =>
        {
            while (true)
            {
                counter++;
                var recievedResults = await udpClient.ReceiveAsync();
                Dat = new Data(recievedResults.Buffer);

                Rpm = (int)Dat.currentEngineRpm / 1000;
                RpmSweepAngle = (Dat.isRaceOn == 1) ? (int)(300 * (Dat.currentEngineRpm / Dat.engineMaxRpm)) : 0;
                RpmFull = (int)Dat.currentEngineRpm;
                Gear = (Dat.gear.ToString() == "0") ? "R" : Dat.gear.ToString();
                Speed = ((int)(Dat.speed * 2.237)).ToString() + " MPH";
                LapNum = (ushort)(Dat.lapNumber + 1);
                Position = Dat.racePosition;
                SteerStartAngle = (int)(100 * Dat.steer / 127 - 100);
                BestLapTime = TimeSpan.FromSeconds(Dat.bestLap).ToString(@"mm\:ss\.fff");
                CurrentLapTime = TimeSpan.FromSeconds(Dat.currentLap).ToString(@"mm\:ss\.fff");
                LastLapTime = TimeSpan.FromSeconds(Dat.lastLap).ToString(@"mm\:ss\.fff");
                RaceTime = TimeSpan.FromSeconds(Dat.currentRaceTime).ToString(@"mm\:ss\.fff");
                AccelHeight = (int)(180 * Dat.accel / 255);
                BrakeHeight = (int)(180 * Dat.brake / 255);
                Boost = ((int)Dat.boost).ToString() + " PSI";
                ClutchPercentage = ((int)(100 * Dat.clutch / 255)).ToString() + "%";
                HandBrakePercentage = ((int)(100 * Dat.handBrake / 255)).ToString() + "%";
                // cold high - 125
                // cold low - 115 #abe5fd

                // warm low - 215
                // warm high - 260
                Gear = Dat.tireTempFrontLeft.ToString();
                ClutchPercentage = calcColorFromTemp(Dat.tireTempFrontLeft).ToString();
                TireColorFL = calcColorFromTemp(Dat.tireTempFrontLeft).ToString();
                TireColorFR = calcColorFromTemp(Dat.tireTempFrontRight);
                TireColorBL = calcColorFromTemp(Dat.tireTempRearLeft);
                TireColorBR = calcColorFromTemp(Dat.tireTempRearRight);
            }
        });
    }

    string calcColorFromTemp(float temp)
    {
        if (temp >= 215)
        {
            float t = (temp - 215) / 150f;
            t = Math.Clamp(t, 0f, 1f);
            Console.WriteLine(t);
            return colorLerp(new Color(), Color.FromArgb(1, 234, 63, 53), t).ToString();
        }
        if (temp > 125 && temp < 215)
        {
            return "Transparent";
        } 
        else if (temp <= 125)
        {
            float t = (temp - 80) / 45f;
            t = Math.Clamp(t, 0f, 1f);
            Console.WriteLine(t);
            return colorLerp(Color.FromArgb(1, 171, 229, 253), new Color(), t).ToString();//"#abe5fd";
        }
        else
        {
            return "Transparent";
        }
    }

    Color colorLerp(Color a, Color b, float t)
    {
        return new Color(
            (byte)(a.A + (b.A - a.A) * t),
            (byte)(a.R + (b.R - a.R) * t),
            (byte)(a.G + (b.G - a.G) * t),
            (byte)(a.B + (b.B - a.B) * t));
    }

    ~MainViewViewModel()
    {
        udpClient.Close();
    }
}
