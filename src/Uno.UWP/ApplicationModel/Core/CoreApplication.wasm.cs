﻿#nullable enable

using System;
using Uno.Foundation.Logging;
using Uno.ApplicationModel.Core;
using Uno.Foundation.Extensibility;
using System.Runtime.InteropServices.JavaScript;
using static __Windows.ApplicationModel.Core.CoreApplicationNative;
using System.Diagnostics;

namespace Windows.ApplicationModel.Core;
partial class CoreApplication
{
	private static Action<object?>? _invalidateRender;
	private static ICoreApplicationExtension? _coreApplicationExtension;

	static partial void InitializePlatform()
	{
		NativeInitialize();

		if (ApiExtensibility.CreateInstance(typeof(CoreApplication), out _coreApplicationExtension))
		{
		}
	}

	internal static void SetInvalidateRender(Action<object?> invalidateRender)
	{
		Trace.Assert(_invalidateRender is null);
		_invalidateRender ??= invalidateRender;
	}

	internal static void QueueInvalidateRender(object? visual)
		=> _invalidateRender?.Invoke(visual);
}
