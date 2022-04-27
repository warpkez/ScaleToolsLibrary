## WarpKez.ScaleToolsLibrary

A C# class library to perform calculations for scale modelling

#### ScaleRecords

Class object for storing the real and scale world measurements

#### ScaleTools

Class containing the mathmatics and logic to perform the convertion.

#### ScaleTools.Metrics

Definitions for Feet, Inches, Meters, Centimeters, and Millimeters.

- All functions with a double return type will either be in the converted scale or -1 if dividing by 0 or a negative number.
- Functions with the return type of ScaleRecords will have all fields as 0 if dividing by 0 or a negative number unless rounding requires a 0.

**public ScaleRecords StructuredRealWorld2ScaleWorld(double measurement, double scale, Metrics metrics)**

Using the real world measurement and scale for a given metric system return a structured response of type ScaleRecords.

**public double RWImperialMMRatio(double feet, double ratio)**

Using real world feet use the ratio to determine the scale measurement.  
Eg 3.5mm to the foot would be `RWImperialMMRatio(1.0, 3.5)`

**public double RWImperialToSWImperial(double feet, double inches, double scale)**

Converts real world feet and inches to scale world decimal inches.

**public double RWImperialToSWMetric(double feet, double inches, double scale)**

As above but in scale world millimeters.

**public double RealWorldtoSWMetric(double measurement, double scale, Metrics metrics)**

Takes real world decimal measurements and returns in millimeters.

---

### ToDo:

- [x] Create initial library and publish test NuGet packages.
- [ ] Add ability to output to list or collection to simplify iterative tasks.
- [ ] Add code to convert decimal inches to inches and fractions.