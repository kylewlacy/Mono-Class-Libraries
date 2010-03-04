//
// System.Runtime.Remoting.Proxies.ProxyAttribute.cs
//
// Author: Duncan Mak  (duncan@ximian.com)
//
// Copyright (C) Ximian, Inc 2002.
//

//
// Copyright (C) 2004 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Channels;

#if NET_2_0
using System.Runtime.InteropServices;
#endif

namespace System.Runtime.Remoting.Proxies {

	[AttributeUsage (AttributeTargets.Class)]
#if NET_2_0
	[ComVisible (true)]
#endif
	public class ProxyAttribute : Attribute, IContextAttribute
	{
		public ProxyAttribute ()
		{
		}

		public virtual MarshalByRefObject CreateInstance (Type serverType)
		{
			RemotingProxy proxy = new RemotingProxy (serverType, ChannelServices.CrossContextUrl, null);
			return (MarshalByRefObject) proxy.GetTransparentProxy();
		}

		public virtual RealProxy CreateProxy (ObjRef objRef, Type serverType, object serverObject, Context serverContext)
		{
			return RemotingServices.GetRealProxy (RemotingServices.GetProxyForRemoteObject (objRef, serverType));
		}

#if NET_2_0
		[ComVisible (true)]
#endif
		public void GetPropertiesForNewContext (IConstructionCallMessage msg)
		{
			// Nothing to add
		}

#if NET_2_0
		[ComVisible (true)]
#endif
		public bool IsContextOK (Context ctx, IConstructionCallMessage msg)
		{
			return true;
		}
	}
}
