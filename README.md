# Microsoft.Maui.Graphics

Microsoft.Maui.Graphics is a cross-platform graphics library for iOS, Android, Windows, macOS, Tizen and Linux completely in C#.  With this library you can use a common API to target multiple abstractions allowing you to share your drawing code between platforms, or mix and match graphics implentations within a singular application.

# Motivation

Within the dotnet ecosystem there are multiple graphics libraries available depending on your target platforms; however, if you are doing cross-platform development there is not a unified graphics abstraction.  Some legacy API's (System.Drawing, I'm looking at you) only have limited support/usefulness on non-Windows platforms.  SkiaSharp runs almost everywhere these days, but for many use cases the native graphics abstractions are needed.

# Goals
* No dependencies on System.Drawing
* Support all graphics operations within an abstraction that the underlying abstraction supports.

# Status
This is an experimental library; however it's based on code that's been in use in production applications for over 10 years.  Because it was refactored out of another code base, some things may have been broken in that process.

# Disclaimer
There is no official support. Use at your own Risk.

# Supported Platforms
Platform               | Supported Abstractions |
-----------------------|-------------------------------------------|
Xamarin.iOS            | CoreGraphics & SkiaSharp | 
Xamarin.Android        | Android.Graphics & SkiaSharp |
Xamarin.Mac            | CoreGraphics & SkiaSharp |
WPF                    | SharpDX, SkiaSharp, Xaml & GDI |
UWP                    | SharpDX, Win2D, Xaml, SkiaSharp |
WinForms               | SharpDX, SkiaSharp & GDI |
Tizen                  | SkiaSharp |
Linux                  | SkiaSharp |
Xamarin.Forms          | Dependent on native platform support (noted above) |

# Main Abstractions
* Canvas - You can draw to a any of the supported abstractions with a common drawing canvas API and a support of common operations and primitives
    * Rectangle, Point and Color primitives
    * Shapes (Rectangles, Rounded Rectangles, Ellipses, Arcs)
    * Paths
    * Images
    * Fonts 
    * Shadows
    * Image and pattern fills
    * Clipping
    * etc...
* Fonts - You can access fonts with a common API
* Attributed text - You can draw attributed text with a common API
* Bitmaps - You can create and draw on bitmap images with a common API
* PDF - You can create PDF's using a common API

# Known Limitations
* Attributed text is not currently supported with SkiaSharp
* The included Blazor (Canvas) implementation no longer compiles, but is included as a reminder to get it working again
