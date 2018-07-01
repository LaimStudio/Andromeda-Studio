import clr
clr.AddReference(AndromedaApi)

import AndromedaApi
task.AppendOutput(AndromedaApi.TestClass.Hello())