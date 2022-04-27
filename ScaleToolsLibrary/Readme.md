## WarpKez.ScaleToolsLibrary

A C# class library to perform calculations for scale modelling

#### ScaleRecords

Contains the parameters for storing the real and scale world measurements

#### ScaleTools

Class containing the mathmatics and logic to perform the convertion.

- All functions with a double return type will either be in the converted scale or -1 if dividing by 0 or a negative number.
- Functions with the return type of ScaleRecords will have all fields as 0 if dividing by 0 or a negative number unless rounding requires a 0.

**public ScaleRecords StructuredRealWorld2ScaleWorld(double measurement, double scale, Metrics metrics)**

Using the real world measurement and scale for a given metric system return a structured response of type ScaleRecords.
Otherwise it return empty of the scale is 0 or negative.