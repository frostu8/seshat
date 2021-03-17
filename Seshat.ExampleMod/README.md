# Example Mod
This project is an example mod. Seshat has no publicly exposed API, so the mod
currently does absolutely nothing besides make an entry in the `Player.log`
file, which you can find in `%APPDATA\..\LocalLow\Project Moon\LibraryOfRuina`.

## Building
Because of how C# interprets dependencies, it's impossible to simply reference
Seshat's binary. This may be changed in the future with the use of a relinker,
but that's pretty low priority.

Simply update the `Assembly-CSharp.dll` reference to a modded version of your
own, and this should compile and run perfectly fine.
