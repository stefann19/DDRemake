using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay
{
    class Skill
    {
        public Skill(string skillIcon, string skillEffect, int damage,int apCost,int mpCost)
        {
            SkillIcon = skillIcon;
            SkillEffect = skillEffect;
            Damage = damage;
            ApCost = apCost;
            MpCost = mpCost;
        }

        public string SkillIcon { get; }
        public string SkillEffect { get; }

        public int Damage { get; private set; }
        public int ApCost { get; private set; }
        public int MpCost { get; private set; }

    }
}
