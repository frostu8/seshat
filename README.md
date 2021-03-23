# Seshat
Seshat is a mod loader for the game
[Library of Ruina](https://store.steampowered.com/app/1256670/Library_Of_Ruina/).
It provides an easy way for the end user to install mods, and a powerful, yet
simple API. Powered by [MonoMod](https://github.com/MonoMod/MonoMod/).

# Why Seshat?
Considering that there is already a readily used mod loader on the market
called [BaseMods](https://www.nexusmods.com/libraryofruina/mods/1), Seshat
would end up splitting the mod community in half. I have no desire to do so,
but I do wish for a more powerful mod loader. I develop Seshat for the
following reasons.

## Open Source
Seshat is open-source, meaning anyone and everyone can contribute to it. Open
sourcing Seshat's codebase on Github allows people to create issues and
contribute directly to the code with pull requests. The mods are created by the 
community, so why shouldn't the mod loader be?

## New API
Seshat bundles with a featureful and complete API to increase interopability
between mods and relieve the load off of mod makers, an API that was also not
able to be written in Harmony patches alone. Seshat's API exposes methods for:

* Fetching and modifying combat pages, key pages, E.G.O. gifts and other items
represented by IDs.
* Adding custom characters, backgrounds and audio clips for story sequences.
* Extending `BattleUnitBuf`, `DiceCardAbility` and `DiceCardSelfAbility` to
create custom abilities, and being able to add them with a declarative syntax.
* Loading custom artwork at runtime.
* Patching game methods with Harmony.

## MonoMod
The use of 0x0ade's tool MonoMod essentially future-proofs Seshat from game 
updates. MonoMod allows users to install older versions of Seshat on newer 
versions of the game, which means that users will no longer have to wait for
the mod loader to update to the newest version of the game. For developers,
this isolates Seshat's version from the game, allowing developers and
contributors to focus more on improving the mod loader than updating it to the
newest game version.

## BaseMod Compatability
While not a priority yet, as Seshat is in its infantile stages, there is plans 
to introduce BaseMod compatability through a compatability layer.

# Building
Clone this repository locally on your computer. Install the needed NuGet
packages (this should be done automatically on the first load of the project in
Visual Studio), and that's it.

I've written a simplified version of `MonoMod.exe` to be used for modding
Library of Ruina called
[SeshatInstaller.Headless](https://github.com/frostu8/SeshatInstaller.Headless).
Download the
[latest release](https://github.com/frostu8/SeshatInstaller.Headless/releases/latest)
and extract the contents to a folder of your choice. Then, in the command line,
run `.\SeshatInstaller.Headless.exe` with the path to the `Seshat.dll` file.

```batch
.\Seshat.Installer.exe "path\to\Seshat.dll"

# if your Library of Ruina installation directory is unable to be found:
.\Seshat.Installer.exe -i "path\to\Assembly-CSharp" "path\to\Seshat.dll"
```

The dependencies `Assembly-CSharp.dll`, `UnityEngine.UI.dll`  and 
`UnityEngine.CoreModule.dll` are both provided in this repository as stripped 
libraries. They work pretty well until there is an update to Library of Ruina, 
and these assemblies are stale.  They're not required; you can simply change 
the dependency paths to the ones you have installed on your computer and it 
will work perfectly fine.
