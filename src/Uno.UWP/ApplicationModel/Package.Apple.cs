#if __IOS__ || __MACOS__
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Foundation;
using Uno.Extensions;
using Uno.UI;
using Windows.Storage;

namespace Windows.ApplicationModel
{
	public partial class Package
	{
		private const string BundleDisplayNameKey = "CFBundleDisplayName";

		private string GetDisplayName() =>
			NSBundle.MainBundle.InfoDictionary[BundleDisplayNameKey]?.ToString() ?? string.Empty;

#if __APPLE_UIKIT__
		private bool GetInnerIsDevelopmentMode() => IsAdHoc;

		private static bool IsAdHoc
			// See https://github.com/bitstadium/HockeySDK-iOS/blob/develop/Classes/BITHockeyHelper.m
			=> !NSBundle.MainBundle.PathForResource("embedded", "mobileprovision").IsNullOrEmpty();
#else
		private bool GetIsDevelopmentMode() => false; //detection not possible on macOS
#endif

		private string GetInstalledLocation()
			=> NSBundle.MainBundle.BundlePath;

		private DateTimeOffset GetInstallDate()
		{
			var urlToDocumentsFolder = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentationDirectory, NSSearchPathDomain.User);

			var installDate = NSFileManager.DefaultManager.GetAttributes(urlToDocumentsFolder[0].Path, out var error).CreationDate;

			return (DateTimeOffset)(DateTime)installDate;
		}

		internal static void SetEntryAssembly(Assembly entryAssembly)
		{
			// TODO: Specific to SKIA!
		}
	}
}
#endif
