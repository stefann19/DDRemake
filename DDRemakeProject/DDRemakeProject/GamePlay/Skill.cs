using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay
{
    class Skill
    {
        public Skill(string skillIcon, string skillEffect, int damage)
        {
            SkillIcon = skillIcon;
            SkillEffect = skillEffect;
            Damage = damage;
        }

        public string SkillIcon { get; }
        public string SkillEffect { get; }

        public int Damage { get; }
    }
}
