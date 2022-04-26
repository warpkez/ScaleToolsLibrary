// See https://aka.ms/new-console-template for more information
using WarpKez.ScaleToolsLibrary;
using static WarpKez.ScaleToolsLibrary.ScaleTools;

Console.WriteLine("Hello, World!");

ScaleTools stl = new();
Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++");
Console.WriteLine($"{stl.RWImperialToSWImperial(1, 0, 87)} Inches");
Console.WriteLine($"{stl.RWImperialToSWMetric(1, 0, 87)} mm");

Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++");
Console.WriteLine($"{stl.RealWorldtoSWMetric(1, 87, Metrics.Meters)} mm");
Console.WriteLine($"{stl.RealWorldtoSWMetric(100,87, Metrics.Centimeters)} mm");
Console.WriteLine($"{stl.RealWorldtoSWMetric(1000,87,Metrics.Millimeters)} mm");

Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++");
Console.WriteLine($"{stl.RealWorldtoSWMetric(1, 87, Metrics.Feet)} mm");
Console.WriteLine($"{stl.RealWorldtoSWMetric(12, 87, Metrics.Inches)} mm");

Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++");
Console.WriteLine($"{stl.RWImperialMMRatio(1.5, 12)} mm");

Console.WriteLine("---------------------------------------------------------");

double d = stl.RWImperialToSWImperial(1, 0, 87);
Console.WriteLine(d.ToString().Length);
Console.WriteLine(Math.Truncate(d));