# Abstract StronglyTypedId records within another Strongly Typed record

This repository focuses on the concept of a StronglyTypedId  (STID). If you  have not worked with this concept before, please see also

* [Series: Using strongly-typed entity IDs to avoid primitive obsession](https://andrewlock.net/series/using-strongly-typed-entity-ids-to-avoid-primitive-obsession/) by Andrew Lock
* [Using C# 9 records as strongly-typed ids](https://thomaslevesque.com/2020/10/30/using-csharp-9-records-as-strongly-typed-ids/) by Thomas Levesque

This repository is a small abstract of the ATAP.Utilities repository which focuses on just the `StronglyTypedId` record type and the `Philote` record type, and is built to support discussions with other OSS developers on these concepts.

The type of the value of a STID, as found in existing systems and databases, is overwhelmingly int, Guid, or string. Conceptual operations on STIDs are exactly the same regardless of the value's type, which makes the STID particularly suited for implementation with an Abstract Type. Furthermore, since IDs should be immutable, C# records can supply a lot of of the necessary boiler plate. Combining these, as Mssr. Levesque demonstrates, an abstract record makes a good choice as the base implementation of the `StronglyTypedId` type.

STIDs assign a unique ID to an instance of a type that includes a STID as a field or property. The ID needs

1) to override ToString()
1) to be able to be serialized/deserialized
1) to be able to be written/read from a databases
1) to provide the above capabilities in a secure and efficient manner

Previously, the ATAP repositories / libraries have used a struct, not a class, for their implementation of `StronglyTypedId` (see the answer to this StackOverflow question(TBD)), and the ServiceStack JSON serializers/deserializers. This repository focuses on extending Mssr. Levesque's code by incorporating the StronglyTypedId as one property of a class/record, and extending the JsonSerialzers' Converters to handle `Interfaces`, `ISTID`, and `ICollection<ISTID>`.

In the ATAP.Utilities repository, the choice of a specific serializer library is deferred until runtime, and controlled by a configuration setting. Unit tests there use a Fixture which implements a DI container `NInject` and supplies an `ISerializer` service. `Newtonsoft`, `Systrem.Text.Json`, and (soon, tbd, `ServiceStack`) are the specific implementation instances that supply the runtime service.

Contrasting that, in this repository and for simplicity, `System.Text.Json` and `NewtonSoft` serializers for `StronglyTypedId` are implemented using individual hard-coded fixtures. Unit Tests are duplicated to use each Fixture, which results in files and classes having names ending in UnitTestsNewtonsoft001 and UnitTestsSystemTextJson001

I'm looking for a better solution so that the Unit tests only have to be written once, and can be run against multiple fixtures each of which directly incorporates a single serialization library. Eventually dynamic loading of the serializer library will be supported, as well as a service implementation using ServiceStack. Please use the Issues for this repository if you would like to contribute.

In Mssr. Levesque's work, he uses `ProductID` and `OrderId` as examples of concrete types for the abstract `StronglyTypedId`. In this repository, I've used `GuidStronglyTypedId` and `IntStronglyTypedId` as concrete records that implement Mssr. Levesque's abstract record.

There are Unit Tests for int and Guid concrete records, testing the `TypeConverter` methods. There are tests for the `Serializer`, testing the `Serialize` and `Deserialize` methods. There are tests of the `ToString()` method. ToString() uses the Invariant culture.

Currently the `TypeConverters` work, The JSON serializers/deserializers for `System.text.Json` work for StronglyTypedId<TValue> and IStronglyTypedId<TValue>.

The JSON serializers/deserializers for `Newtonsoft` and it's Unit Tests are currently in development.

# `AbstractPhilote<TId, TValue>`

This repository also defines an abstract generic record `AbstractPhilote<TId, TValue>`. The full definition includes constraints on the generic type parameters, `where TId : AbstractStronglyTypedId<TValue>, new() where TValue : notnull`
The `AbstractPhilote<TId, TValue>` record contains an `ID` auto-implemented of type `StronglyTypedId<TValue>`, an auto-implemented  `AdditionalIDs` of type `ConcurrentDictionary<string,StronglyTypedId<TValue>` for aliases and an auto-implemented `TimeBlocks` of type `IEnumerable<ITimeBlock>` for timestamps.  I'm using the time/date library `TimePeriodLibrary.NET` by Jani Giannoudisr from GitHub for rich timestamp features.

When a Philote is added to a class as a field or property, it is expected that the first generic type parameter is an implementation of `StronglyTypedID<TValue>`, as indicated by the constraint `where TId : AbstractStronglyTypedId<TValue>,`, and furhtermore it muust have a `new()` constructor as indicated by the `new()` in the constraint. this ensures the Deserializer can create a new instances.  The second generic type parameter `TValue` is passed along to the implementation of the `TId<TValue>`.

During runtime, a choice has to be made between int or Guid for the runtime type of TValue.

The JSON serializers/deserializers for a Philote, using either `Newtonsoft` or `Systrem.Text.Json` and all related Unit Tests are currently in development.


Attributions:
* [Series: Using strongly-typed entity IDs to avoid primitive obsession](https://andrewlock.net/series/using-strongly-typed-entity-ids-to-avoid-primitive-obsession/) by Andrew Lock
* [Using C# 9 records as strongly-typed ids](https://thomaslevesque.com/2020/10/30/using-csharp-9-records-as-strongly-typed-ids/) by Thomas Levesque
* [Time Period Library for .NET](https://github.com/Giannoudis/TimePeriodLibrary) by Jani Giannoudisr
