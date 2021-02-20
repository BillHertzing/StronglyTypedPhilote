# Astract Strongly Typed ID records within another strongly typed record

This repository focuses on the concept of a StronglyTypedId  (STID) . If you  have not worked with this concept before, please see also
* [Series: Using strongly-typed entity IDs to avoid primitive obsession](https://andrewlock.net/series/using-strongly-typed-entity-ids-to-avoid-primitive-obsession/) by Andrew Lock
* [Using C# 9 records as strongly-typed ids](https://thomaslevesque.com/2020/10/30/using-csharp-9-records-as-strongly-typed-ids/) by Thomas Levesque

This repository is a small abstract of the ATAP.Utilities repository that focuses on just the `StronglyTypedID` record type and the `Philote` record type, and is built to support discussions with other OSS developers on these concepts.

The type of the value of a STID,as found in existing systems and databases, is overwhelmingly int, Guid, or string. Conceptual operations on STIDs are exactly the same regardless of the value's type, which makes the STID particularly suited for implementation with an Abstract Type. Furthermore, since IDs should be immutable, C# records can supply a lot of of the necessary boiler plate. Combining these, an abstract record makes a good choice as the base implementation of the StronglyTypedID type.

STIDs assign a unique ID to an instance of a type that includes a STID as a field or property. The ID needs
a) to override ToString()
a) to be able to be serialized/deserialzed
a) to be able to be written/read from a databases
a) to provide the above capabilities in an efficient manner

Previously, the ATAP repositories / libraries have used a struct, not a class, for it's implementation of `StronglyTypedID` (see the answer to this Stackoverflow question(TBD)), and the ServiceStack JSON serializers/deserializers. The large commented section in the  StronglytypedIds.cs is the prior implemntation, that of a Guid value in a struct.

In the ATAP.Utilities repository, the choice of a specific serializer library is deferred until runtime, and controlled by a configuration setting. Unit tests there use a Fixture which implements a DI container `NInject` and supplies a `ISerializer` service. `Newtonsoft`, `Systrem.Text.Json`, and (soon, tbd, `ServiceStack`) are the specific implementation instances that supply the runtime service.

In this repository, only `System.Text.Json` is being implemented. Implementations for `Newtonsoft`, `ServiceStack` and dynamic selection and loading will be added to the roadmap.

In Mssr. Levesque's work, he uses `ProductID` and `OrderId` as examples of concrete types for the abstract `StronglyTypedId`. In this repository, I've tried GuidStronglyTypedId and IntStronglyTypedID as records that implement Mssr. Levesque's abstract class.

There are Unit tests for int and Guid concrete classes, testing the `TypeConverter` methods. There are tests for the `Serializer`, testing the `Serialize` and `Deserialize` methods. There are the `ToString` tests

Currently the `TypeConverters` pass their tests, but the JSON serializers (for `System.text.Json`) does NOT work.

Also in the ATAP.Utilities repository, the StronglyTyped `Guid` struct is a AutoProperty of a generic class `Philote<T>`. The `Philote<T>` class also contains a `IEnum<>` for aliases and an `IEnum<>` for timestamps. When designing the new Philote and STID, the serialized stream should support `IEnums` and `IDictionary` as well.

Completness would indicate the need for an `enum` to specify the three types that values can be. That would allow a serialized onjet to indicate the value's type of each id using a `tinyint`
