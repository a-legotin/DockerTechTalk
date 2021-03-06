﻿using System.Collections.Concurrent;
using System.Net;

namespace HTTPListenerSimple
{
  public class RequestRepository : IRepository<HttpListenerRequest>
  {
    public ConcurrentBag<HttpListenerRequest> Repository { get; set; }

    public void Add(HttpListenerRequest item)
    {
      if (Repository == null) Repository = new ConcurrentBag<HttpListenerRequest>();
      Repository.Add(item);
    }

    public int CountItems()
    {
      if (Repository == null) Repository = new ConcurrentBag<HttpListenerRequest>();
      return Repository.Count;
    }

    public static IRepository<HttpListenerRequest> GetRepository()
    {
      return new RequestRepository();
    }
  }
}