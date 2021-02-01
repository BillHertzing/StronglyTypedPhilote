
using System;
using System.Text.Json;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System.Reflection;
using ATAP.Utilities.StronglyTypedID;
using ATAP.Utilities.StronglyTypedIDs.JsonConverter.SystemTextJson;

namespace ATAP.Utilities.StronglyTypedId.UnitTests {
  public class Fixture {
    public JsonSerializerOptions JsonSerializerOptions { get; set; }
    public Fixture() {
      JsonSerializerOptions = new JsonSerializerOptions();
    }
  }
  public partial class StronglyTypedIDSerializationUnitTests001 : IClassFixture<Fixture> {
    protected Fixture Fixture { get; }
    protected ITestOutputHelper TestOutput { get; }

    public StronglyTypedIDSerializationUnitTests001(ITestOutputHelper testOutput, Fixture fixture) {
      Fixture = fixture;
      TestOutput = testOutput;
      // Add Converters
      Fixture.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverterFactory().CreateConverter(typeof(int),this.Fixture.JsonSerializerOptions));
      Fixture.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverterFactory().CreateConverter(typeof(Guid),this.Fixture.JsonSerializerOptions));
    }

  }

}
