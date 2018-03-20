using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDRemakeProject.GamePlay.New.Character.Logic;
using DDRemakeProject.GamePlay.Old;
using Action = DDRemakeProject.GamePlay.Old.Action;

namespace DDRemakeProject.GamePlay.New
{
    public class CharacterLogic
    {

        public CharacterLogic(Traits traits)
        {
            Traits = traits;
            Level = new Level(this,1,() => Level.TargetXp * 1.2f);
            Resistances = new Resistances(this);
            Stats = new Stats(this);
            Modifiers = new Modifiers(this);
            GetDefaultActions();
        }

        public void LevelUp()
        {
            Resistances = new Resistances(this);
            Stats = new Stats(this);
            Modifiers = new Modifiers(this);
        }

        private void Initialise()
        {
            /*Stats = new Stats();*/
        }

        public Traits Traits { get; set; }
        public Stats Stats { get; set; }
        public Resistances Resistances { get; set; }
        public Modifiers Modifiers { get; set; }
        public Level Level { get; set; }
        



        //needs refactoring
        public Old.Character CharacterParent { get; set; }

        public List<Action> Actions { get; set; }

        private void GetDefaultActions()
        {
            Actions = new List<Action> { StatsLogic.LowAttack, StatsLogic.MediumAttack, StatsLogic.FireSpell, StatsLogic.HeavyAttack, StatsLogic.AirSpell, StatsLogic.EarthSpell, StatsLogic.WaterSpell, StatsLogic.BasicBlock };
        }
    }
}
