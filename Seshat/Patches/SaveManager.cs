using GameSave;
using MonoMod;

namespace GameSave
{
    class patch_SaveManager : SaveManager
    {
        [MonoModIgnore]
        [PatchLoadSaveData]
        public new extern bool LoadPlayDataFromSaveFile(int slot, bool retry = true);

        [MonoModIgnore]
        [PatchSavePlayData]
        public new extern bool SavePlayData(int slot, bool force = false);
    }
}
