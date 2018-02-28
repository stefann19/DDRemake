using System.Collections.Generic;

namespace DDRemakeProject.GamePlay.Old
{
    public class TurnSystem
    {
        public static int CurrentCharIndex;

        public static void GetNextTurn(List<Character> characters)
        {
            while (true)
            {
                CurrentCharIndex = CurrentCharIndex + 1 >= characters.Count ? 0 : CurrentCharIndex + 1;

                if (characters[CurrentCharIndex].Status == CharacterTypes.Status.Dead)
                    continue;
                break;
            }
        }
    }
}