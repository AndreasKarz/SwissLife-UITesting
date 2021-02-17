using NUnit.Framework;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]

[assembly: Guid("5fee6864-f46f-4d7f-97c5-01c2cfa1b3fb")]

[assembly: Parallelizable(ParallelScope.Fixtures)]

[assembly: Property("logger", "trx")]