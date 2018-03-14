using System.Collections.Generic;
using DDRemakeProject.GamePlay.New.Character;

namespace DDRemakeProject.GamePlay.New
{
    public class Party
    {
        public Party(List<Character.Character> characters)
        {
            Characters = characters;
        }

        public List<Character.Character> Characters { get; set; }

    }
}
