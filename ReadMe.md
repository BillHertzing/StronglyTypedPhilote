# Abstract `StronglyTypedId` records within another `StronglyTyped` record

Welcome to the documentation site for the StronglyTypedPhilote repository. This site contains information on building, testing and using the StronglyTypedId and StronglyTypedPhilote classes and methods. It also has concept documentation on how the assemblies are expected to be used and how they are built.

This repository is a training, experimentation and discussion repository, aimed at explaining and testing the concept of the abstract record StronglyTypedId and a class StronglyTypedPhilote that incorporates and extends the StronglytypedId concept. If you have not worked with the concept of a `StronglyTypedId` before, please see also

* [Series: Using strongly-typed entity Ids to avoid primitive obsession](https://andrewlock.net/series/using-strongly-typed-entity-ids-to-avoid-primitive-obsession/) by Andrew Lock
* [Using C# 9 records as strongly-typed ids](https://thomaslevesque.com/2020/10/30/using-csharp-9-records-as-strongly-typed-ids/) by Thomas Levesque
* [Strongly typed Guid as generic struct](https://stackoverflow.com/questions/53748675/strongly-typed-guid-as-generic-struct) The 'accepted' answer by Eric Lippert

For the high-level overview of, the purpose for, and the contents of this repository, start with the repository [ReadMe](/ReadMe.md) (this document)

To get started using the code examples, see the [QuickStart](/SolutionDocumentation/StronglyTypedPhiloteQuickStart.md)

This documentation website follows the conventions of sites built with the [DocFx tool](https://dotnet.github.io/docfx/). Across the top are major sections, along the left side is a Table Of Contents (TOC) that changes per the major section content, and along the right side is a content-specific TOC that changes according to which document is being viewed.

If you are new to using this repository, please start with the [Getting Started](/SolutionDocumentation/GettingStarted.html) guide.

The code and content of this repository are under the [MIT License](/LICENSE.md

This repository is a small abstract of the ATAP.Utilities repository. This repository focuses on just the `StronglyTypedId` record type and the `AbstractPhilote` record type, and is built to support discussions with other OSS developers on these concepts.

The type of the value of a `StronglyTypedId`, as found in existing systems and databases, is overwhelmingly int, Guid, or string. Conceptual operations on the `StronglyTypedId` are exactly the same regardless of the value's type, which makes the `StronglyTypedId` particularly suited for implementation with an Abstract Type. Furthermore, since Ids should be immutable, C# records can supply a lot of of the necessary boiler plate. Combining these, as Mssr. Levesque demonstrates, an abstract record makes a good choice as the base implementation of the `StronglyTypedId` type.

For this repository, I have renamed Mssr. Levesque's `StronglyTypedId` to `AbstractStronglyTypedId<TValue>` to help me reason betterabout the code. Adding the word 'Abstract' reminds me that `AbstractStronglyTypedId` cannot be used as a concrete implementation.

The `AbstractStronglyTypedId<TValue>` and any type that derives from it needs:

1) to override ToString()
1) to be able to be serialized/deserialized
1) to be able to be written/read from a databases
1) to provide the above capabilities in a secure and efficient manner

Previously, the ATAP repositories / libraries have used a struct, not a class, for their implementation of `StronglyTypedId` (see the answer to this StackOverflow question[Strongly typed Guid as generic struct](https://stackoverflow.com/questions/53748675/strongly-typed-guid-as-generic-struct) from Eric Lippert, and used the ServiceStack JSON serializers/deserializers to implement a JsonConverter for this type.

This repository focuses on ensuring the previous `Guid` `StronglyTypedId` struct can be replaced in the ATAP.Utilities repository by extending Mssr. Levesque's code to ensure the (renamed) record type `AbstractStronglyTypedId<TValue>` and corresponding interface `IAbstractStronglyTypedId<TValue>` can be serialized and deserialized correctly. This repository includes code to ensure that `IEnumerable<AbstractStronglyTypedId<TValue>>`, `Dictionary<string, AbstractStronglyTypedId<TValue>>`, and `ConcurrentDictionary<string, AbstractStronglyTypedId<TValue>>` will serialize and deserialize correctly, and that `IEnumerable<IAbstractStronglyTypedId<TValue>>`, `Dictionary<string, IAbstractStronglyTypedId<TValue>>`, and `ConcurrentDictionary<string, IAbstractStronglyTypedId<TValue>>` will serialize correctly . Deserialization of the interfaces does NOT work, because C# does not support deserialization of inherited interfaces. There is currently a roadmap item to investigate/implement a way to deserialize into an appropriate interface.

## Serialization libraries

In the ATAP.Utilities repository, the choice of a specific serializer library is deferred until runtime, and controlled by a configuration setting. Unit tests there use a Fixture which implements a DI container `NInject` and supplies an `ISerializer` service. `Newtonsoft`, `System.Text.Json`, and (soon, tbd, `ServiceStack`) are the specific implementation instances that supply the runtime service.

Contrasting that, in this repository and for simplicity, `System.Text.Json` and `Newtonsoft` serializers for `AbstractStronglyTypedId<TValue>` are implemented using individual hard-coded fixtures. Unit Tests are duplicated to use each Fixture, which results in files and classes having names ending in UnitTestsNewtonsoft001 and UnitTestsSystemTextJson001

I'm looking for a better solution so that the Unit tests only have to be written once, and can be run against multiple fixtures each of which directly incorporates a single serialization library. [Issue #4](https://github.com/BillHertzing/StronglyTypedPhilote/issues/4) addresses this enhancement in detail. The ATAP.Utilities has a working dynamic loader that will load a serialization library at runtime and inject it into a DI-container. [Issue #5](https://github.com/BillHertzing/StronglyTypedPhilote/issues/5) addresses an enhancement to this repository that will incorporate the dynamic loader and runtime-loaded serialization library testing to ensure the code here works with a dynamically loaded serialization library.

[Issue #6](https://github.com/BillHertzing/StronglyTypedPhilote/issues/6) address implementation of a shim package for ServiceStack so the ``AbstractStronglyTypedId<TValue>` and `Philote` can be used with that serialization library.

## Unit tests for the `AbstractStronglyTypedId<TValue>`

In Mssr. Levesque's work, he uses `ProductId` and `OrderId` as examples of concrete types for the abstract `StronglyTypedId`. In this repository, I've used `GuidStronglyTypedId` and `IntStronglyTypedId` as concrete records that implement Mssr. Levesque's abstract record.

There are Unit Tests for int and Guid concrete records, testing the `TypeConverter` methods. There are tests for the `Serializer`, testing the `Serialize` and `Deserialize` methods. There are tests of the `ToString()` method. The ToString() tests use the Invariant culture.

Currently the `TypeConverters` work, The JSON serializers/deserializers for `System.text.Json` work for `StronglyTypedId<TValue>` and `IStronglyTypedId<TValue>`.

The JSON serializers/deserializers for `Newtonsoft` and it's Unit Tests are currently in development.

## `AbstractPhilote<TId, TValue>`

This repository also defines an abstract generic record `AbstractPhilote<TId, TValue>`. The full definition includes constraints on the generic type parameters, `where TId : AbstractStronglyTypedId<TValue>, new() where TValue : notnull`
The `AbstractPhilote<TId, TValue>` record contains an `Id` auto-implemented property of type `StronglyTypedId<TValue>`, an auto-implemented property `AdditionalIds` of type `ConcurrentDictionary<string,StronglyTypedId<TValue>` for aliases and an auto-implemented property `TimeBlocks` of type `IEnumerable<ITimeBlock>` for timestamps. I'm using the time/date library `TimePeriodLibrary.NET` by Jani Giannoudisr from GitHub for rich timestamp features.

The constructors for the base class must be able to return an instance of a `TId` type variable. This s simple if the TValue is `int`. But the Guid type does not have a new(). Instead the base class constructor uses [Activator.CreateInstance Alternative](https://trenki2.github.io/blog/2018/12/28/activator-createinstance-faster-alternative/) by Trenki. This code replaces the Activator.CreateInstance with classes that use Compiled Expression Trees.  Not only does it allow the creation of the Id auto-property, it does so faster than the Reflection-based static method, and caches the instance factory for the `TId`.

When a Philote is added to a class as a field or property, it is expected that the first generic type parameter is an implementation of `StronglyTypedId<TValue>`, as indicated by the constraint `where TId : AbstractStronglyTypedId<TValue>,`, and furthermore it must have a `new()` constructor as indicated by the `new()` in the constraint. this ensures the Deserializer can create a new instances. The second generic type parameter `TValue` is passed along to the implementation of the `TId<TValue>`.

During runtime, a choice has to be made between `int` or `Guid` for the runtime type of `TValue`.

The JSON serializers/deserializers for a Philote, using either `Newtonsoft` or `Systrem.Text.Json` and all related Unit Tests are currently in development.

## Attributions

* [Strongly typed Guid as generic struct](https://stackoverflow.com/questions/53748675/strongly-typed-guid-as-generic-struct) Answer  as provided by Eric Lippert,
* [Series: Using strongly-typed entity Ids to avoid primitive obsession](https://andrewlock.net/series/using-strongly-typed-entity-ids-to-avoid-primitive-obsession/) by Andrew Lock
* [Using C# 9 records as strongly-typed ids](https://thomaslevesque.com/2020/10/30/using-csharp-9-records-as-strongly-typed-ids/) by Thomas Levesque
* [Time Period Library for .NET](https://github.com/Giannoudis/TimePeriodLibrary) by Jani Giannoudisr
