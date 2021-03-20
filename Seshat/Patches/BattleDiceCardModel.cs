using LOR_DiceSystem;
using MonoMod;
using Seshat.API;
using System;
using System.Collections.Generic;
using Seshat;

class patch_BattleDiceCardModel : BattleDiceCardModel
{
#pragma warning disable CS0649 // We know what we're doing!
    private DiceCardXmlInfo _xmlData;
#pragma warning restore CS0649 

    // lets just replace this method, we can all agree it sucks
    [MonoModReplace]
    public new List<BattleDiceBehavior> CreateDiceCardBehaviorList()
    {
        List<BattleDiceBehavior> diceList = new List<BattleDiceBehavior>();

        for (int i = 0; i < _xmlData.DiceBehaviourList.Count; i++)
        {
            DiceBehaviour dice = _xmlData.DiceBehaviourList[i];

            BattleDiceBehavior battleDice = new BattleDiceBehavior();
            battleDice.behaviourInCard = dice;
            battleDice.SetIndex(i);

            if (!string.IsNullOrEmpty(dice.Script))
            {
                DiceCardAbilityBase ability;
                try
                {
                    ability = DiceCardAbilityRegistrar.GetNew(dice.Script);
                }
                catch (Exception e)
                {
                    Logger.Error("seshat.dice.ability", 
                        $"Failed to load dice ability {dice.Script}!");
                    e.LogException();
                    continue;
                }

                if (ability == null)
                {
                    Logger.Warn("seshat.dice.ability", 
                        $"Dice ability {dice.Script} not found.");
                } 
                else
                {
                    battleDice.AddAbility(ability);
                }
            }

            diceList.Add(battleDice);
        }

        return diceList;
    }
}
