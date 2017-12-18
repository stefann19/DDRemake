using System;
using System.Collections.Generic;

namespace DDRemakeProject.GamePlay
{
    public class AvatarSpotsManager
    {

        static AvatarSpotsManager()
        {
            _availableAllySpots = new List<Tuple<CharacterUi, bool>>
            {
                new Tuple<CharacterUi, bool>(new CharacterUi(BattleEngine.BattleWindowUi.Position0,BattleEngine.BattleWindowUi.Icon0), true),
                new Tuple<CharacterUi, bool>(new CharacterUi(BattleEngine.BattleWindowUi.Position1,BattleEngine.BattleWindowUi.Icon1), true),
                new Tuple<CharacterUi, bool>(new CharacterUi(BattleEngine.BattleWindowUi.Position2,BattleEngine.BattleWindowUi.Icon2), true)
            };
            _availableEnemySpots = new List<Tuple<CharacterUi, bool>>
            {
                new Tuple<CharacterUi, bool>(new CharacterUi(BattleEngine.BattleWindowUi.Position3,BattleEngine.BattleWindowUi.Icon3), true),
                new Tuple<CharacterUi, bool>(new CharacterUi(BattleEngine.BattleWindowUi.Position4,BattleEngine.BattleWindowUi.Icon4), true),
                new Tuple<CharacterUi, bool>(new CharacterUi(BattleEngine.BattleWindowUi.Position5,BattleEngine.BattleWindowUi.Icon5), true)
            };

        }

        private static List<Tuple<CharacterUi,bool>> _availableAllySpots;
        private static List<Tuple<CharacterUi, bool>> _availableEnemySpots;

        public static CharacterUi GetSpot(CharacterTypes.Type type)
        {
            if (type == CharacterTypes.Type.Ally)
            {
                return SearchForFreeSpotInList(_availableAllySpots);
            }
            else
            {
                return SearchForFreeSpotInList(_availableEnemySpots);
            }
        }

        public static CharacterUi SearchForFreeSpotInList(List<Tuple<CharacterUi, bool>> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                Tuple<CharacterUi, bool> t = list[i];
                if (!t.Item2) continue;
                
                list[i] = new Tuple<CharacterUi, bool>(t.Item1, false);
                return t.Item1;
            }
            return null;
        }

        public static void FreeSpot(CharacterUi charAvater, CharacterTypes.Type type)
        {
            if (type == CharacterTypes.Type.Ally)
            {
                FreeSpotForList(charAvater, _availableAllySpots);
            }
            else
            {
                FreeSpotForList(charAvater, _availableAllySpots);
            }
        }

        private static void FreeSpotForList(CharacterUi charUi, List<Tuple<CharacterUi, bool>> list)
        {
            int index = list.FindIndex(pair => pair.Item1.Equals(charUi));
            list[index] = new Tuple<CharacterUi, bool>(charUi, true);
        }
    }
}
