# Hole Raster Calculator

## What is the purpose of this module?

This module collects the input holes into hole rasters for easier handling.

## How to initialize the module?

First, the HoleRasterCalculator and EngineeringFramework packages should be installed to the client project.
Then, the project can be initalized by loading the required classes into an `IServiceProvider` class instance, from which the calculator will be retrieved.

An example for initializing the project:

    var serviceCollection = new ServiceCollection();
            new EngineeringFrameworkModule(
                "HoleRasterCalculatorExample",
                @".\Preferences\ExampleApplicationConfiguration.xml",
                "en", "hu-HU")
                .LoadModule(serviceCollection);
    new HoleRasterCalculatorModule().LoadModule(serviceCollection);
    
    var serviceProvider = serviceCollection.BuildServiceProvider();

## How to initialize the input for the calculator?

For multiple holes:

1. The x and y coordinates, the tags, diameters and depths of the holes should be collected and stored in collections.
For example:
    `var xValues = new List<decimal>() { 6m, 2m };
     var yValues = new List<decimal>() { 3.14m, 0m };
     var tags = new List<string>() { "Example1", "Example2" };
     var diameters = new List<decimal>() { 9.81m, 3m };
     var depths = new List<decimal>() { 6.4m, 2.75m };`
2. These collections must be passed to an `IHoleFactory` instance which will create the required format for the calculator itself.
The `IHoleFactory` can be accessed via the previously mentioned `IServiceProvider`.
For example:
	`var factory = serviceProvider.GetRequiredService<IHoleFactory>();`
    `ICollection<IHole> holes = factory.CreateHoles(xValues, yValues, tags, diameters, depths);`

For one hole:

1. The procedure is alomst the same as with more holes, except that only one of each values (coordinates, tags, diameters, depths) are required.
For example:
    `var xValue = 6m;
     var yValue = 0m;
     var tags = "Example3";
     var diameter = 9.81m;
     var depth = 2.75m;`
2. These values must be passed to an `IHoleFactory` instance which will create the required format for the calculator itself.
The `IHoleFactory` can be accessed via the previously mentioned `IServiceProvider`.
For example:
	`var factory = serviceProvider.GetRequiredService<IHoleFactory>();`
    `IHole hole = factory.CreateHole(xValue, yValue, tag, diameter, depth);`
3. Notice that the return type was not the same as in the multiple holes example. Since the calculator expects a collection, we must gather our separate hole objects to a collection call the calculator.
For example:
	`var holes = new List<IHole>();`
    `holes.Add(hole);`

## How to call the calculator

Now that the input has been parsed into the correct format, the `IHoleRasterCalculator.CalculateHoleRasters` method can be called to calculate the hole rasters.
We retrieve the `IHoleRasterCalculator` instance from the `IServiceProvider`, and then call its method.
The results are stored in a collection of `IHoleRaster` model instances.

An example for calling the calculator and retrieving the results:
    
    using (var scope = serviceProvider.CreateScope())
    {
        var services = scope.ServiceProvider;
        var calculator = services.GetRequiredService<IHoleRasterCalculator>();
        IEnumerable<IHoleRaster> holeRasters = calculator.CalculateHoleRasters(holes);
    }

## Possbile error messages for incorrect input

If an invalid input is provided (something is missing, or the data is incorrect, etc.) feedback messages are sent[^1] and the components return an empty collections.
These are stored in a scoped `IFeedbackCollector` object.

The possible feedback types:

| Feedback type                                      | When does it happen? |
| -------------------------------------------------- | -------------------- |
| ERROR_XValuesNotProvided | The xValues parameter of the factory is an empty list. |
| ERROR_YValuesNotProvided | The yValues parameter of the factory is an empty list. |
| ERROR_TagValuesNotProvided | The tags parameter of the factory is an empty list. |
| ERROR_DiameterValuesNotProvided | The diameters parameter of the factory is an empty list. |
| ERROR_DepthValuesNotProvided | The depths parameter of the factory is an empty list. |
| ERROR_XAndYValueCountsAreNotEqual | The number of given X and Y coordinates differ. |
| ERROR_CoordinateAndTagCountsAreNotEqual | The number of given coordinates and tags differ. |
| ERROR_CoordinateAndDiameterCountsAreNotEqual | The number of given coordinates and diameters differ. |
| ERROR_CoordinateAndDepthCountsAreNotEqual | The number of given coordinates and depths differ. |
| ERROR_HoleCollectionIsNull | The given holes collection is a null object. |
| ERROR_HoleIsNull | A given hole is a null object. |
| ERROR_HoleTagNotFound | A given hole tag is an empty string. |
| ERROR_HoleCoordinatesNotFound | The coordinates of a given hole is a null object. |
| ERROR_HoleDiameterIsNotPositive | The diameter of a given hole is not positive. |
| ERROR_HoleDepthIsNotPositive | The depth of a given hole is not positive. |

## A full example of usage


    // initialize the module
    var serviceCollection = new ServiceCollection();
            new EngineeringFrameworkModule(
                "HoleRasterCalculatorExample",
                @".\Preferences\ExampleApplicationConfiguration.xml",
                "en", "hu-HU")
                .LoadModule(serviceCollection);
    new HoleRasterCalculatorModule().LoadModule(serviceCollection);
    var serviceProvider = serviceCollection.BuildServiceProvider();

    // test data
    var xValues = new List<decimal>() { 6m, 2m };
    var yValues = new List<decimal>() { 3.14m, 0m };
    var tags = new List<string>() { "Example1", "Example2" };
    var diameters = new List<decimal>() { 9.81m, 3m };
    var depths = new List<decimal>() { 6.4m, 2.75m };

    using (var scope = serviceProvider.CreateScope())
    {
        var services = scope.ServiceProvider;

        // generate input for the calculator
        var factory = serviceProvider.GetRequiredService<IHoleFactory>();
        var holes = factory.CreateHoles(xValues, yValues, tags, diameters, depths);

        // get and call the calculator
        var calculator = services.GetRequiredService<IHoleRasterCalculator>();
        IEnumerable<IHoleRaster> holeRasters = calculator.CalculateHoleRasters(holes);
    }

[^1]: For null references, and empty types, exceptions are thrown.