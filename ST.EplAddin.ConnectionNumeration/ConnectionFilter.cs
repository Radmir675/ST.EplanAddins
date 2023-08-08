// Decompiled with JetBrains decompiler
// Type: ST.EplAddin.ConnectionNumeration.ConnectionFilter
// Assembly: ST.EplAddin.ConnectionNumeration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 16E8408A-E298-4C32-9D31-7775C7701E17
// Assembly location: C:\Users\tembr\Desktop\AddIns\ST.EplAddin.ConnectionNumeration.dll

using Eplan.EplApi.DataModel;

namespace ST.EplAddin.ConnectionNumeration
{
  internal class ConnectionFilter : ICustomFilter
  {
    public bool IsMatching(StorableObject objectToCheck)
    {
      Connection connection = objectToCheck as Connection;
      return connection.KindOfWire ==Connection.Enums.KindOfWire.IndividualConnection && connection.Properties.CONNECTION_HAS_CDP.ToBool();
    }
  }
}
