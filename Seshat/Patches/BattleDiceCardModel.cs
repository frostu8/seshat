using LOR_DiceSystem;
using MonoMod;
using Registrar = Seshat.API.Registrar;
using Seshat.API;
using System;
using System.Collections.Generic;
using Seshat;

class patch_BattleDiceCardModel : BattleDiceCardModel
{
#pragma warning disable CS0649 // We know what we're doing!
    private DiceCardXmlInfo _xmlData;
#pragma warning restore CS0649 

    [MonoModReplace]
    public new DiceCardSelfAbilityBase CreateDiceCardSelfAbilityScript()
    {
        if (string.IsNullOrEmpty(this._xmlData.Script))
            return null;

        DiceCardSelfAbilityBase ability;
        try
        {
            ability = Registrar.CardAbility.Get(this._xmlData.Script)?.Instantiate();
        }
        catch (Exception e)
        {
            Logger.Error("seshat.card.ability", 
                $"Failed to load card ability {this._xmlData.Script}!");
            e.LogException();
            return null;
        }

        if (ability == null)
        {
            Logger.Warn("seshat.dice.ability", 
                $"Dice ability {this._xmlData.Script} not found.");
            return null;
        }

        return ability;
    }

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
                    ability = Registrar.DiceAbility.Get(dice.Script)?.Instantiate();
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
