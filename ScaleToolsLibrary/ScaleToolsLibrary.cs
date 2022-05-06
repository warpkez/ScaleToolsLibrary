namespace WarpKez.ScaleToolsLibrary;


public class ScaleRecords
{
    // Real world measurements
    public double rwFeet { get; set; }
    public double rwInches { get; set; }

    public double rwMeters { get; set; }
    public double rwCentimeters { get; set; }
    public double rwMillimeters { get; set; }

    // Scale world measurements
    public double swFeet { get; set; }
    public double swInches { get; set; }

    public double swMeters { get; set; }
    public double swCentimeters { get; set; }
    public double swMillimeters { get; set; }

    // Scale used
    public double Scale { get; set; }
}

/// <summary>
/// Basic class for predefined frational increments of an inch.
/// 1/16, 1/8, 1/4, and 1/2
/// </summary>
public class FractionOfInches
{
    public const double sixteenth = 1.0 / 16.0;
    public const double eighth = 1.0 / 8.0;
    public const double quarter = 1.0 / 4.0;
    public const double half = 1.0 / 2.0;
}

public class ScaleTools
{
    /// <summary>
    /// Enumerated metrics
    /// </summary>
    public enum Metrics
    {
        Feet,
        Inches,
        Meters,
        Centimeters,
        Millimeters
    }

    // Rounding to this many decimal places
    private const int precision = 3;
    private const double InchTomm = 25.4;
    private const double FeetToInches = 12.0;

    /// <summary>
    /// Converts real world measurement to Scale world millimeters
    /// </summary>
    /// <param name="measurement">Measurement in any metric</param>
    /// <param name="scale">Modelling scale</param>
    /// <param name="metrics">Feet, inches, meters, centimeters, millimeters</param>
    /// <returns>ScaleRecords</returns>
    public ScaleRecords StructuredRealWorld2ScaleWorld(double measurement, double scale, Metrics metrics)
    {
        ScaleRecords s = new();
        double scaled = 0.0;
        if (measurement < 0) measurement *= -1;
        if (scale < 0) scale *= -1;
        s.Scale = scale;

        if (scale > 0)
        {
            switch (metrics)
            {
                case Metrics.Feet:
                    s.rwFeet = Math.Floor(measurement);
                    s.rwInches = Math.Round((measurement - s.rwFeet) * FeetToInches, precision);

                    scaled = (measurement) / scale;
                    s.swFeet = Math.Floor(scaled);
                    s.swInches = Math.Round((scaled - s.swFeet) * FeetToInches, precision);

                    s.swMillimeters = Math.Round(((s.swFeet * FeetToInches) + s.swInches) * InchTomm, precision);
                    s.swCentimeters = Math.Round(s.swMillimeters / 10, precision);
                    s.swMeters = Math.Round(s.swMillimeters / 1000, precision);
                    break;

                case Metrics.Inches:
                    s.rwFeet = 0;
                    s.rwInches = measurement;

                    scaled = (measurement) / scale;
                    s.swFeet = 0;
                    s.swInches = Math.Round(scaled, precision);

                    s.swMillimeters = Math.Round(scaled * InchTomm, precision);
                    s.swCentimeters = Math.Round(s.swMillimeters / 10, precision);
                    s.swMeters = Math.Round(s.swMillimeters / 1000, precision);
                    break;


                case Metrics.Meters:
                    s.rwMeters = measurement;
                    s.rwCentimeters = measurement * 100;
                    s.rwMillimeters = measurement * 1000;

                    scaled = (measurement) / scale;

                    s.swMeters = Math.Round(scaled, precision);
                    s.swCentimeters = Math.Round(scaled * 100, precision);
                    s.swMillimeters = Math.Round(scaled * 1000, precision);
                    break;

                case Metrics.Centimeters:
                    s.rwMeters = measurement / 100;
                    s.rwCentimeters = measurement;
                    s.rwMillimeters = measurement * 10;

                    scaled = (measurement) / scale;

                    s.swMeters = Math.Round(scaled / 100, precision);
                    s.swCentimeters = Math.Round(scaled, precision);
                    s.swMillimeters = Math.Round(scaled * 10, precision);
                    break;
                case Metrics.Millimeters:
                    s.rwMeters = measurement / 1000;
                    s.rwCentimeters = measurement / 10;
                    s.rwMillimeters = measurement;

                    scaled = (measurement) / scale;

                    s.swMeters = Math.Round(scaled / 1000, precision);
                    s.swCentimeters = Math.Round(scaled / 10, precision);
                    s.swMillimeters = Math.Round(scaled, precision);
                    break;
                default: break;
            }
        }
        return s;
    }

    /// <summary>
    /// Generates a table derived from real world feet measurements
    /// incrementing the inches by a defined step
    /// </summary>
    /// <param name="feet">Measurement in feet</param>
    /// <param name="step">Fractions of an inche to increment</param>
    /// <param name="scale">Modelling scale</param>
    /// <returns>List<ScaleRecords></returns>
    public List<ScaleRecords> RWFeetToSWMetricList(double feet, double step, double scale)
    {
        List<ScaleRecords> RecordList = new();
        if (step < 0) step *= -1;
        if (feet < 0) feet *= -1;
        if (scale < 0) scale *= -1;

        for (double inches = 0.0; inches < 12.0; inches += step)
        {
            //RecordList = new();
            ScaleRecords s = new();
            s.Scale = scale;
            s.rwFeet = feet;
            s.rwInches = inches;

            double scaled = (((feet * FeetToInches) + inches) * InchTomm) / scale;

            s.swMillimeters = Math.Round(scaled, precision);
            s.swCentimeters = Math.Round((scaled / 10), precision);
            s.swMeters = Math.Round((scaled / 1000), precision);

            RecordList.Add(s);
        }

        return RecordList;
    }

    /// <summary>
    /// Uses the ratio of feet to millimeters eg 3.5mm to 1 Foot.
    /// </summary>
    /// <param name="feet">Measurement in feet</param>
    /// <param name="ratio">Scale ratio</param>
    /// <returns>double</returns>
    public double RWImperialMMRatio(double feet, double ratio) => feet * ratio;

    /// <summary>
    /// Coverts real world imperial measurement to scale world imperial measurements in inches
    /// </summary>
    /// <param name="feet">Measurement in feet</param>
    /// <param name="inches">Measurement in inches</param>
    /// <param name="scale">Modelling scale</param>
    /// <returns>double</returns>
    public double RWImperialToSWImperial(double feet, double inches, double scale)
    {
        if (feet < 0) feet *= -1;
        if (inches < 0) inches *= -1;
        if (scale < 0) scale *= -1;

        double result = -1.0;
        if (scale > 0)
        {
            result = Math.Round(((feet * FeetToInches) + inches) / scale, precision);
        }

        return result;
    }

    /// <summary>
    /// Converts real world imperial measurements to scale world millimeters
    /// </summary>
    /// <param name="feet">Measurement in feet</param>
    /// <param name="inches">Measurement in inches</param>
    /// <param name="scale">Modelling scale</param>
    /// <returns>double</returns>
    public double RWImperialToSWMetric(double feet, double inches, double scale)
    {
        if (feet < 0) feet *= -1;
        if (inches < 0) inches *= -1;
        if (scale < 0) scale *= -1;


        double result = -1.0;
        if (scale > 0)
        {
            result = Math.Round((((feet * FeetToInches) + inches) * InchTomm) / scale, precision);
        }

        return result;
    }

    /// <summary>
    /// Converts real world measurement to Scale world millimeters
    /// </summary>
    /// <param name="measurement">Measurement in any metric</param>
    /// <param name="scale">Modelling scale</param>
    /// <param name="metrics">Feet, inches, meters, centimeters, millimeters</param>
    /// <returns>double</returns>
    public double RealWorldtoSWMetric(double measurement, double scale, Metrics metrics)
    {
        if (measurement < 0) measurement *= -1;
        if (scale < 0) scale *= -1;

        double results = -1.0;

        if (scale > 0)
        {
            switch (metrics)
            {
                case Metrics.Feet:
                    results = ((measurement * FeetToInches) * InchTomm) / scale;
                    break;
                case Metrics.Inches:
                    results = ((measurement) * InchTomm) / scale;
                    break;
                case Metrics.Meters:
                    results = (ConvertMetric(measurement, Metrics.Meters) / scale);
                    break;
                case Metrics.Centimeters:
                    results = (ConvertMetric(measurement, Metrics.Centimeters) / scale);
                    break;
                case Metrics.Millimeters:
                    results = ConvertMetric(measurement, Metrics.Millimeters) / scale;
                    break;
                default: break;
            }
        }

        return Math.Round(results, precision);
    }

    /// <summary>
    /// Convert metric measurements to millimeters for ease of maths
    /// </summary>
    /// <param name="value">Measurement</param>
    /// <param name="metrics">From what metric to millimeters</param>
    /// <returns>double</returns>
    private double ConvertMetric(double value, Metrics metrics)
    {
        if (metrics == Metrics.Meters)
        {
            return value * 1000;
        }
        if (metrics == Metrics.Centimeters)
        {
            return value * 10;
        }

        return value;
    }
}