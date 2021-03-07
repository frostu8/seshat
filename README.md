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
to introduce BaseMod compatability, either through a
[BaseMod compiler](#seshatcompiler) or an entire BaseMod compatability layer.

# Seshat.Compiler
Seshat is planned to use DLLs exclusively for mods. This keeps things simple,
but it adds a layer of complexity to mods, especially to those who do not know
C# programming.

Seshat will ship with a compiler that can automatically write a mod assembly
based on a simple directory tree and human-readable files.
