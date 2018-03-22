using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NReco.Linq;

namespace DDRemakeProject.GamePlay.New.Character.Logic
{
    public class Modifiers
    {
        public Modifiers(CharacterLogic characterLogic)
        {
            CharacterLogic = characterLogic;

            LambdaParser lambdaParser = new NReco.Linq.LambdaParser();
            Dictionary<string, object> varContext = new Dictionary<string, object> {["traits"] = characterLogic.Traits};

            PhysicalAttack =  double.Parse(lambdaParser.Eval(characterLogic.Race.ArmourAttackFormulaString, varContext).ToString());
            FireAttack = double.Parse(lambdaParser.Eval(characterLogic.Race.FireAttackFormulaString, varContext).ToString());
            WaterAttack = double.Parse(lambdaParser.Eval(characterLogic.Race.WaterAttackFormulaString, varContext).ToString());
            AirAttack = double.Parse(lambdaParser.Eval(characterLogic.Race.AirAttackFormulaString, varContext).ToString());
            EarthAttack = double.Parse(lambdaParser.Eval(characterLogic.Race.EarthAttackFormulaString, varContext).ToString());
        }

        public CharacterLogic CharacterLogic { get; set; }

        public double PhysicalAttack { get; set; }
        public double FireAttack { get; set; }
        public double WaterAttack { get; set; }
        public double AirAttack { get; set; }
        public double EarthAttack { get; set; } 
    }
}
