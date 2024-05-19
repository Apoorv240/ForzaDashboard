using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForzaDashboard.Core;

// 232 bits
// https://forums.forza.net/t/forza-motorsport-7-data-out-feature-details/74013/24
public class Data
{
    public Data(byte[] bytes)
    {
        setFromBytes(bytes);
    }

    #region DataVars
    public int isRaceOn { get; set; } = 0; // s32
    public uint timestampMS { get; set; } = 0; // u32

    public float engineMaxRpm = 0; // f32
    public float engineIdleRpm = 0;
    public float currentEngineRpm { get; set; } = 0;

    float accelX = 0;
    float accelY = 0;
    float accelZ = 0;

    float veloX = 0;
    float veloY = 0;
    float veloZ = 0;

    float angularVelocityX = 0;
    float angularVelocityY = 0;
    float angularVelocityZ = 0;

    float yaw { get; set; }
    float pitch { get; set; }
    float roll { get; set; }

    float normalizedSuspensionTravelFrontLeft = 0;
    float normalizedSuspensionTravelFrontRight = 0;
    float normalizedSuspensionTravelRearLeft = 0;
    float normalizedSuspensionTravelRearRight = 0;

    float tireSlipRatioFrontLeft = 0;
    float tireSlipRatioFrontRight = 0;
    float tireSlipRatioRearLeft = 0;
    float tireSlipRatioRearRight = 0;

    float wheelRotationSpeedFrontLeft = 0;
    float wheelRotationSpeedFrontRight = 0;
    float wheelRotationSpeedRearLeft = 0;
    float wheelRotationSpeedRearRight = 0;

    int wheelOnRumbleStripFrontLeft = 0; // s32
    int wheelOnRumbleStripFrontRight = 0;
    int wheelOnRumbleStripRearLeft = 0;
    int wheelOnRumbleStripRearRight = 0;

    float wheelInPuddleDepthFrontLeft = 0; // f32
    float wheelInPuddleDepthFrontRight = 0;
    float wheelInPuddleDepthRearLeft = 0;
    float wheelInPuddleDepthRearRight = 0;

    float surfaceRumbleFrontLeft = 0;
    float surfaceRumbleFrontRight = 0;
    float surfaceRumbleRearLeft = 0;
    float surfaceRumbleRearRight = 0;

    float tireSlipAngleFrontLeft = 0;
    float tireSlipAngleFrontRight = 0;
    float tireSlipAngleRearLeft = 0;
    float tireSlipAngleRearRight = 0;

    float tireCombinedSlipFrontLeft = 0;
    float tireCombinedSlipFrontRight = 0;
    float tireCombinedSlipRearLeft = 0;
    float tireCombinedSlipRearRight = 0;

    float suspensionTravelMetersFrontLeft = 0;
    float suspensionTravelMetersFrontRight = 0;
    float suspensionTravelMetersRearLeft = 0;
    float suspensionTravelMetersRearRight = 0;

    int carOrdinal = 0; // s32
    int carClass = 0;
    int carPerformanceIndex = 0;
    int drivetrainType = 0;
    int numCylinders = 0;

    float positionX = 0; // f32
    float positionY = 0;
    float positionZ = 0;

    public float speed { get; set; } = 0f;
    float power = 0;
    float torque = 0;

    public float tireTempFrontLeft { get; set; } = 0;
    public float tireTempFrontRight { get; set; } = 0;
    public float tireTempRearLeft { get; set; } = 0;
    public float tireTempRearRight { get; set; } = 0;

    public float boost { get; set; } = 0;
    float fuel = 0;
    float distanceTraveled = 0;
    public float bestLap { get; set; } = 0;
    public float lastLap { get; set; } = 0;
    public float currentLap { get; set; } = 0;
    public float currentRaceTime { get; set; } = 0;

    public ushort lapNumber { get; set; } = 0; // u16
    public byte racePosition { get; set; } = 0; // u8

    public byte accel { get; set; } = 0;
    public byte brake { get; set; } = 0;
    public byte clutch { get; set; } = 0;
    public byte handBrake { get; set; } = 0;
    public byte gear { get; set; } = 0;
    public sbyte steer { get; set; } = 0;// s8

    sbyte normalizedDrivingLine = 0;
    sbyte normalizedAIBrakeDifference = 0;
    #endregion

    public void setFromBytes(byte[] bytes)
    {
        /*if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);*/

        int idx = 0;

        this.isRaceOn = toInt32(bytes, ref idx);
        timestampMS = toUInt32(bytes, ref idx);

        engineMaxRpm = toFloat(bytes, ref idx);
        engineIdleRpm = toFloat(bytes, ref idx);
        currentEngineRpm = toFloat(bytes, ref idx);

        accelX = toFloat(bytes, ref idx);
        accelY = toFloat(bytes, ref idx);
        accelZ = toFloat(bytes, ref idx);

        veloX = toFloat(bytes, ref idx);
        veloY = toFloat(bytes, ref idx);
        veloZ = toFloat(bytes, ref idx);

        angularVelocityX = toFloat(bytes, ref idx);
        angularVelocityY = toFloat(bytes, ref idx);
        angularVelocityZ = toFloat(bytes, ref idx);

        yaw = toFloat(bytes, ref idx);
        pitch = toFloat(bytes, ref idx);
        roll = toFloat(bytes, ref idx);

        normalizedSuspensionTravelFrontLeft = toFloat(bytes, ref idx);
        normalizedSuspensionTravelFrontRight = toFloat(bytes, ref idx);
        normalizedSuspensionTravelRearLeft = toFloat(bytes, ref idx);
        normalizedSuspensionTravelRearRight = toFloat(bytes, ref idx);

        tireSlipRatioFrontLeft = toFloat(bytes, ref idx);
        tireSlipRatioFrontRight = toFloat(bytes, ref idx);
        tireSlipRatioRearLeft = toFloat(bytes, ref idx);
        tireSlipRatioRearRight = toFloat(bytes, ref idx);

        wheelRotationSpeedFrontLeft = toFloat(bytes, ref idx);
        wheelRotationSpeedFrontRight = toFloat(bytes, ref idx);
        wheelRotationSpeedRearLeft = toFloat(bytes, ref idx);
        wheelRotationSpeedRearRight = toFloat(bytes, ref idx);

        wheelOnRumbleStripFrontLeft = toInt32(bytes, ref idx); // s32
        wheelOnRumbleStripFrontRight = toInt32(bytes, ref idx);
        wheelOnRumbleStripRearLeft = toInt32(bytes, ref idx);
        wheelOnRumbleStripRearRight = toInt32(bytes, ref idx);

        wheelInPuddleDepthFrontLeft = toFloat(bytes, ref idx); // f32
        wheelInPuddleDepthFrontRight = toFloat(bytes, ref idx);
        wheelInPuddleDepthRearLeft = toFloat(bytes, ref idx);
        wheelInPuddleDepthRearRight = toFloat(bytes, ref idx);

        surfaceRumbleFrontLeft = toFloat(bytes, ref idx);
        surfaceRumbleFrontRight = toFloat(bytes, ref idx);
        surfaceRumbleRearLeft = toFloat(bytes, ref idx);
        surfaceRumbleRearRight = toFloat(bytes, ref idx);

        tireSlipAngleFrontLeft = toFloat(bytes, ref idx);
        tireSlipAngleFrontRight = toFloat(bytes, ref idx);
        tireSlipAngleRearLeft = toFloat(bytes, ref idx);
        tireSlipAngleRearRight = toFloat(bytes, ref idx);

        tireCombinedSlipFrontLeft = toFloat(bytes, ref idx);
        tireCombinedSlipFrontRight = toFloat(bytes, ref idx);
        tireCombinedSlipRearLeft = toFloat(bytes, ref idx);
        tireCombinedSlipRearRight = toFloat(bytes, ref idx);

        suspensionTravelMetersFrontLeft = toFloat(bytes, ref idx);
        suspensionTravelMetersFrontRight = toFloat(bytes, ref idx);
        suspensionTravelMetersRearLeft = toFloat(bytes, ref idx);
        suspensionTravelMetersRearRight = toFloat(bytes, ref idx);

        carOrdinal = toInt32(bytes, ref idx); // s32
        carClass = toInt32(bytes, ref idx);
        carPerformanceIndex = toInt32(bytes, ref idx);
        drivetrainType = toInt32(bytes, ref idx);
        numCylinders = toInt32(bytes, ref idx);

        positionX = toFloat(bytes, ref idx); // f32
        positionY = toFloat(bytes, ref idx);
        positionZ = toFloat(bytes, ref idx);

        speed = toFloat(bytes, ref idx);
        power = toFloat(bytes, ref idx);
        torque = toFloat(bytes, ref idx);

        tireTempFrontLeft = toFloat(bytes, ref idx);
        tireTempFrontRight = toFloat(bytes, ref idx);
        tireTempRearLeft = toFloat(bytes, ref idx);
        tireTempRearRight = toFloat(bytes, ref idx);

        boost = toFloat(bytes, ref idx);
        fuel = toFloat(bytes, ref idx);
        distanceTraveled = toFloat(bytes, ref idx);
        bestLap = toFloat(bytes, ref idx);
        lastLap = toFloat(bytes, ref idx);
        currentLap = toFloat(bytes, ref idx);
        currentRaceTime = toFloat(bytes, ref idx);

        lapNumber = toUInt16(bytes, ref idx); // u16
        racePosition = toByte(bytes, ref idx); // u8

        accel = toByte(bytes, ref idx);
        brake = toByte(bytes, ref idx);
        clutch = toByte(bytes, ref idx);
        handBrake = toByte(bytes, ref idx);
        gear = toByte(bytes, ref idx);
        steer = toSByte(bytes, ref idx); // s8

        normalizedDrivingLine = toSByte(bytes, ref idx);
        normalizedAIBrakeDifference = toSByte(bytes, ref idx);
    }

    #region UtilMethods
    private int toInt32(byte[] bytes, ref int index)
    {
        int retVal = BitConverter.ToInt32(bytes, index);
        index += 4;
        return retVal;
    }

    private uint toUInt32(byte[] bytes, ref int index)
    {
        uint retVal = BitConverter.ToUInt32(bytes, index);
        index += 4;
        return retVal;
    }

    private float toFloat(byte[] bytes, ref int index)
    {
        float retVal = BitConverter.ToSingle(bytes, index);
        index += 4;
        return retVal;
    }

    private ushort toUInt16(byte[] bytes, ref int index)
    {
        ushort retVal = BitConverter.ToUInt16(bytes, index);
        index += 2;
        return retVal;
    }

    private byte toByte(byte[] bytes, ref int index)
    {
        byte retVal = bytes[index];
        index += 1;
        return retVal;
    }

    private sbyte toSByte(byte[] bytes, ref int index)
    {
        sbyte retVal = (sbyte)bytes[index];
        index += 1;
        return retVal;
    }
    #endregion
}
