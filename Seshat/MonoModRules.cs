using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.InlineRT;
using MonoMod.Utils;

namespace MonoMod
{
    /// <summary>
    /// Patches the gargantuan LoadPlayDataFromSaveSlot(); function.
    /// </summary>
    [MonoModCustomMethodAttribute("PatchLoadSaveData")]
    class PatchLoadSaveDataAttribute : Attribute { }

    /// <summary>
    /// Patches the gargantuan SavePlayData(); function.
    /// </summary>
    [MonoModCustomMethodAttribute("PatchSavePlayData")]
    class PatchSavePlayDataAttribute : Attribute { }

    static class MonoModRules
    {
        static TypeDefinition Seshat;

        public static void PatchSavePlayData(MethodDefinition method, CustomAttribute attrib)
        {
            if (!method.HasBody)
                return;

            if (Seshat == null)
                Seshat = MonoModRule.Modder.FindType("Seshat.Seshat")?.Resolve();
            if (Seshat == null)
                return;

            MethodDefinition m_RunGameDataSave = Seshat.FindMethod("System.Void RunGameDataSave(GameSave.SaveData)");
            if (m_RunGameDataSave == null)
                return;

            VariableDefinition loc_SaveData = method.Body.Variables[2];

            Mono.Collections.Generic.Collection<Instruction> instrs = method.Body.Instructions;
            ILProcessor il = method.Body.GetILProcessor();
            for (int i = 0; i < instrs.Count; i++)
            {
                Instruction instr = instrs[i];

                /*
                We are looking for this call:
                callvirt instance class GameSave.SaveData LibraryModel::GetSaveData()
                */

                if (instr.OpCode == OpCodes.Callvirt &&
                    (instr.Operand as MethodReference)?.GetID() == "GameSave.SaveData LibraryModel::GetSaveData()")
                {
                    // push the cursor over the stloc instruction
                    i += 2;

                    // push savedata to stack
                    instrs.Insert(i, il.Create(OpCodes.Ldloc_S, loc_SaveData));
                    i++;

                    // call our method
                    instrs.Insert(i, il.Create(OpCodes.Call, m_RunGameDataSave));
                    i++;
                }
            }
        }

        public static void PatchLoadSaveData(MethodDefinition method, CustomAttribute attrib)
        {
            if (!method.HasBody)
                return;

            if (Seshat == null)
                Seshat = MonoModRule.Modder.FindType("Seshat.Seshat")?.Resolve();
            if (Seshat == null)
                return;

            MethodDefinition m_RunGameDataLoad = Seshat.FindMethod("System.Void RunGameDataLoad(GameSave.SaveData)");
            if (m_RunGameDataLoad == null)
                return;

            VariableDefinition loc_SaveData = method.Body.Variables[8];

            Mono.Collections.Generic.Collection<Instruction> instrs = method.Body.Instructions;
            ILProcessor il = method.Body.GetILProcessor();
            for (int i = 0; i < instrs.Count; i++)
            {
                Instruction instr = instrs[i];

                /*
                We are looking for this call:
                callvirt instance void LibraryModel::LoadFromSaveData(class GameSave.SaveData)
                */

                if (instr.OpCode == OpCodes.Callvirt &&
                    (instr.Operand as MethodReference)?.GetID() == "System.Void LibraryModel::LoadFromSaveData(GameSave.SaveData)")
                {
                    i++;

                    // push savedata to stack
                    instrs.Insert(i, il.Create(OpCodes.Ldloc_S, loc_SaveData));
                    i++;

                    // call our method
                    instrs.Insert(i, il.Create(OpCodes.Call, m_RunGameDataLoad));
                    i++;
                }
            }
        }
    }
}
