﻿using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivony.Caching.Memcached
{
  public class MemcachedProvider : ICacheProvider
  {

    MemcachedClient client;

    public MemcachedProvider( MemcachedClientConfiguration configuration ) : this( new MemcachedClient( configuration ) ) { }


    public MemcachedProvider( MemcachedClient client )
    {
      this.client = client;
    }

    void ICacheProvider.Clear()
    {
      client.FlushAll();
    }

    object ICacheProvider.Get( string key )
    {
      return client.Get( key );
    }

    void ICacheProvider.Remove( string cacheKey )
    {
      var result = client.ExecuteRemove( cacheKey );
      if ( result.Success == false )
        throw new Exception( result.Message, result.Exception );
    }

    void ICacheProvider.Set( string key, object value, CachePolicyItem cachePolicy )
    {
      client.Store( StoreMode.Set, key, value, cachePolicy.Expires );
    }

    public void Dispose()
    {
      client.Dispose();
    }
  }
}
