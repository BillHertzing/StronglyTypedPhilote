
#r "nuget: ServiceStack, 5.11.0 "
#r "nuget: ServiceStack.OrmLite, 5.11.0"
#r "nuget: ServiceStack.OrmLite.SqlServer, 5.11.0"
#r "nuget: TimePeriodLibrary.NET, 2.1.1"
#r "C:\Dropbox\whertzing\GitHub\StronglyTypedPhilote\src\ATAP.Utilities.Philote.Interfaces\bin\Debug\net5.0\ATAP.Utilities.Philote.Interfaces.dll"
#r "C:\Dropbox\whertzing\GitHub\StronglyTypedPhilote\src\ATAP.Utilities.Philote\bin\Debug\net5.0\ATAP.Utilities.Philote.dll"
#r "C:\Dropbox\whertzing\GitHub\StronglyTypedPhilote\src\ATAP.Utilities.StronglyTypedIds.Interfaces\bin\Debug\net5.0\ATAP.Utilities.StronglyTypedIds.Interfaces.dll"
#r "C:\Dropbox\whertzing\GitHub\StronglyTypedPhilote\src\ATAP.Utilities.StronglyTypedIds\bin\Debug\net5.0\ATAP.Utilities.StronglyTypedIds.dll"

using System.Linq;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.DataAnnotations;
using ServiceStack.Text;

using ATAP.Utilities.StronglyTypedIds;
using ATAP.Utilities.Philote;

using System.Collections.Concurrent;
using Itenso.TimePeriod;

 public record TestClassWithPhiloteId<TValue> : AbstractStronglyTypedId<TValue>, IAbstractStronglyTypedId<TValue> where TValue : notnull {
    public TestClassWithPhiloteId() : base() { }
    public TestClassWithPhiloteId(TValue value) : base(value) { }
  }
  public interface ITestClassWithPhilote<TValue> : IAbstractPhilote<TestClassWithPhiloteId<TValue>, TValue> where TValue : notnull { }

  public record TestClassWithPhilote<TValue> : AbstractPhilote<TestClassWithPhiloteId<TValue>, TValue>, IAbstractPhilote<TestClassWithPhiloteId<TValue>, TValue>, ITestClassWithPhilote<TValue>
      where TValue : notnull {
    public TestClassWithPhilote(TestClassWithPhiloteId<TValue> iD = default, ConcurrentDictionary<string, IAbstractStronglyTypedId<TValue>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) {
      if (iD != default) { Id = iD; }
      else {
        Id = (typeof(TValue)) switch {
          Type intType when typeof(TValue) == typeof(int) => new TestClassWithPhiloteId<int>() { Value = new Random().Next() } as TestClassWithPhiloteId<TValue>,
          Type GuidType when typeof(TValue) == typeof(Guid) => new TestClassWithPhiloteId<Guid>() as TestClassWithPhiloteId<TValue>,
          // ToDo: replace with new custom exception and localization of exception message
          _ => throw new Exception(FormattableString.Invariant($"Invalid TValue type {typeof(TValue)}")),

        };
      }
      // Attribution [Linq ToDictionary will not implicitly convert class to interface](https://stackoverflow.com/questions/25136049/linq-todictionary-will-not-implicitly-convert-class-to-interface) Educational but ultimately fails
      // The ToDictionary extension method available in LINQ for generic Dictionaries is NOT availabe for ConcurrentDictionaries, the following won't work...
      //  additionalIDs.ToDictionary(kvp => kvp.Key, kvp => (IAbstractStronglyTypedId<TValue>) kvp.Value)
      // As this is a concurrent operation we will need to put a semaphore around the argument passed in
      // attribution [How do you convert a dictionary to a ConcurrentDictionary?](https://stackoverflow.com/questions/27063889/how-do-you-convert-a-dictionary-to-a-concurrentdictionary) from a comment on a question, contributed by Panagiotis Kanavos
      // we have to convert the parameter's value to a cast to a less derived interface
      if (additionalIDs != default) {
        // ToDo : add write semaphore around the parameter before enumerating the Dictionary
        AdditionalIDs = new ConcurrentDictionary<string, IAbstractStronglyTypedId<TValue>>(additionalIDs.Select(kvp => new KeyValuePair<string, IAbstractStronglyTypedId<TValue>>(kvp.Key, (IAbstractStronglyTypedId<TValue>)kvp.Value)));
      }
      else {
        AdditionalIDs = new ConcurrentDictionary<string, IAbstractStronglyTypedId<TValue>>();
      }
      TimeBlocks = timeBlocks != default ? timeBlocks : new List<ITimeBlock>();
    }
  }


public static void WaitForDebugger()
{
  Process currentProcess = Process.GetCurrentProcess();
  Console.WriteLine($"Attach Debugger (VS Code) to process {currentProcess.Id} {currentProcess.ProcessName}");

  while (!Debugger.IsAttached)
  {
  }
}

//Console.WriteLine("Hello from the Dotnet c# script interpreter");
//Console.WriteLine($"Current Working Directory is {System.IO.Directory.GetCurrentDirectory()}");
// WaitForDebugger();

string host = "::1";
int port = 1433;
string databaseName = "StronglyTypedIdTestDatabase";

var defaultConnectionString = $"Server={host}:{port};Database={databaseName};Trusted_Connection=True";
defaultConnectionString = $"Server={host};Integrated Security=True;Database={databaseName};MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Application Name=Testing";
var dbFactory = new OrmLiteConnectionFactory(
    defaultConnectionString,
    SqlServer2017Dialect.Provider);

// ToDo: In ATAP DB Management package, create a function which raises a custom exception?
if (dbFactory == null) { throw new Exception($"Failed to connect to any database with the connection string: \"{defaultConnectionString}\""); }

using (var db = dbFactory.Open())
{
  Console.WriteLine("Opened");
  if (db.CreateTableIfNotExists<TestClassWithPhilote<int>>())
  {
    Console.WriteLine("Table did NOT exist");
  }
  else
  {
    Console.WriteLine("Table did Exist");
  }
  // ToDo: wrap in try catch
  var x = db.Insert(new TestClassWithPhilote<int>());
  Console.WriteLine($"x = {x.ToString()}");

  TestClassWithPhilote<int> result = db.Single<TestClassWithPhilote<int>>(x => x.ID != null);
  result.PrintDump();

  List<TestClassWithPhilote<int>> resultlist = db.Select<TestClassWithPhilote<int>>(x => x.ID != null);
  resultlist.PrintDump();



  // public class T1
  // {
  //   [AutoIncrement]
  //   [PrimaryKey]
  //   public int Id { get; set; }  public class GComment : IGComment {
  //   public string Name { get; set; }

  //   public T1(string name) : this(1, "A") { }
  //   public T1(int id, string name) { Id = id; Name = name; }
  // }

  // T1 result;
  // IntStronglyTypedId resultITI;
  // var r = db.CreateTableIfNotExists<T1>();
  // Console.WriteLine($"r is : {r}");
  // if (db.CreateTableIfNotExists<T1>())
  // {
  //   Console.WriteLine("Calling Insert");
  //   db.Insert(new T1(1, "A"));
  // }
  // else
  // {
  //       db.Insert(new T1( "B"));

  //   Console.WriteLine("Exists?");
  // }

  // result = db.SingleById<T1>(1);
  // Console.WriteLine("dumping result");
  // result.PrintDump(); //= {Id: 1, Name:Name

  // IntStronglyTypedId TestId = new IntStronglyTypedId(100);

  // if (db.CreateTableIfNotExists<IntStronglyTypedId>())
  // {
  //   Console.WriteLine("Calling Insert on TestId");
  //   db.Insert(TestId);
  // }
  // else
  // {
  //       db.Insert(TestId);

  //   Console.WriteLine("table exists, inserting duplicate TestId...");
  // }

  // resultITI = db.SingleById<IntStronglyTypedId>(100);
  // Console.WriteLine("dumping resultITI");
  // resultITI.PrintDump();
}

// Console.WriteLine($"result :{result}");
// Console.WriteLine($"resultITI :{resultITI}");
