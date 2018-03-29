using System;
using System.Data;

namespace TestApp
{
    internal interface IDbRequest : IDisposable
    {
        IDataReader OpenConnectionAndRequestData();
        void CloseConnection();
    }
}