# `Type.GetType();` Hitlist
These are functions that make calls to `Type.GetType();`, typically to
instantiate some specialization of a base class. If you ever write code;
*please do not ever do this.* Never rely on `Type.GetType();`.

```cs
// creates a behaviour action
RencounterManager.CreateBehaviourAction(string)
// ...also creates a behaviouraction
BattleFarAreaPlayManager.EffectPhase(float)
// creates a dice card priority
BattleDiceCardModel.GetPriority(int)
// creates an emotion card ability
BattleEmotionCardModel.ctor(EmotionCardXmlInfo, BattleUnitModel)
// creates a quest mission script
QuestMissionModel.ctor(QuestModel, QuestMissionXmlInfo)
// creates a battle dialogue model script
UnitDataModel.InitBattleDialogueByDefaultBook(int)
// creates an enemy team stage manager script
EnemyTeamStageManager.CreateManager(string)
// creates an aggro setter script (AI technically)
EnemyUnitAggroSetter.CreateAggroSetter(string)
// creates a target setter script (AI technicaly)
EnemyUnitTargetSetter.CreateTargetSetter(string)
```

## Ignored
```cs
// editor thing we can ignore
EditorCardInfoPanel.OnClickScriptChange()
```
