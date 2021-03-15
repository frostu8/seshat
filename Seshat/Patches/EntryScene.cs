// extern function with no attribute is patched by MonoMod
#pragma warning disable CS0626 

using Seshat;

class patch_EntryScene : EntryScene
{
    public extern void orig_Awake();
    public new void Awake()
    {
        SeshatLoader.LoadAuto();
        orig_Awake();
    }
}
