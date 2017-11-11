# LiteDB not working in Unity3D

LiteDB 4.0.0 release. Built from sources with VS2017, Debug profile, 3.5 target. Release profile does not work either.
Unity3D 2017.2.0f3 Personal.

To reproduce open project in Unity3D, open Test Runner window and run the test. It fails, but it should not. Oddly, VS2017 debugger does not work with DLL code.

A possible workaround is to enable experimental 4.6 runtime in Unity3D and use either 3.5 or 4.0 target in LiteDB.