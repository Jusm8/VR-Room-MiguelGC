using UnityEngine;
using System;

public class WallClock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    [Header("Usar hora real del sistema")]
    public bool useLocalTime = true;

    [Header("Hora manual (si no usas la real)")]
    [Range(0, 23)] public int worldHour = 12;
    [Range(0, 59)] public int worldMinute = 0;
    [Range(0, 59)] public int worldSecond = 0;

    void Update()
    {
        DateTime time;

        if (useLocalTime)
        {
            // Hora del sistema
            time = DateTime.Now;
        }
        else
        {
            // Hora del mundo virtual
            time = new DateTime(1, 1, 1, worldHour, worldMinute, worldSecond)
                   .AddSeconds(Time.time);
        }

        float hours   = time.Hour % 12 + time.Minute / 60f;
        float minutes = time.Minute + time.Second / 60f;
        float seconds = time.Second + time.Millisecond / 1000f;

        // Cada vuelta completa son 360 grados:
        // - Horas: 12 horas → 30 grados por hora
        // - Minutos / segundos: 60 → 6 grados por unidad

        if (hourHand != null)
            hourHand.localRotation = Quaternion.Euler(hours * 30f, 0f, 0f);

        if (minuteHand != null)
            minuteHand.localRotation = Quaternion.Euler(minutes * 6f, 0f, 0f);

        if (secondHand != null)
            secondHand.localRotation = Quaternion.Euler(seconds * 6f, 0f, 0f);
    }
}
