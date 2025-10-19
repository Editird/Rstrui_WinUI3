# Fluent System Restore
Fluent System Restore is a modern Windows system restore point management tool built with WinUI 3. It features a sleek Fluent Design interface that enables users to intuitively browse, select, and restore system restore points with ease.

## Features
- **Modern Fluent UI Interface**: Clean, intuitive design with full support for dark/light themes
- **System Restore Status Check**: Quickly verify if system restore is enabled on your system
- **Comprehensive Restore Point Browser**: View all available system restore points in an organized manner
- **Detailed Information Display**: Access complete restore point details including name, creation time, type, description, and more
- **One-Click Restoration**: Effortlessly restore your system to any selected restore point

## Installation & Usage

### Prerequisites
- **Operating System**: Windows 10 21H1 (build 19043) or later
- **Framework**: .NET 8.0 Runtime
- **Development Tools**: Windows App SDK 1.8

### Development Setup
1. Open `Rstrui_WinUI3.sln` in Visual Studio 2026 (Run as administrator)
2. Build the solution
3. Debug using `Rstrui_WinUI3 (Unpackaged)` configuration

## Packaged not working?
Unfortunately, due to Microsoft's permission restrictions on MSIX (especially the WMI component), it seems I can't do much about this. We can only wait to see if Microsoft officially plans to open up this feature in the future. Alternatively, if you have any ideas on how to bypass Microsoft's restrictions through certain techniques to enable access to Windows system restore points in a WinUI 3 packaged app, we would welcome your contributions!