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
}

public class ScaleTools
{
    public enum Metrics
    {
        Feet,
        Inches,
        Meters,
        Centimeters,
        Millimeters
    }

    // Rounding to this many decimal places
    private const int precision = 2;
    private const double Inch2mm = 25.4;

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
        double result = -1.0;
        if (scale != 0)
        {
            result = Math.Round(((feet * 12) + inches) / scale, precision);
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
        double result = -1.0;
        if (scale != 0)
        {
            result = Math.Round((((feet * 12) + inches) * 25.4) / scale, precision);
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
        double results = -1.0;

        if (scale != 0)
        {
            switch (metrics)
            {
                case Metrics.Feet:
                    results = ((measurement * 12) * Inch2mm) / scale;
                    break;
                case Metrics.Inches:
                    results = ((measurement) * Inch2mm) / scale;
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