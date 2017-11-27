using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay
{
    public class Action
    {
        public Action(string actionIcon, string actionEffect, int damage,int apCost,int mpCost,ActionTypes.ActionType actionType)
        {
            Icon = actionIcon;
            Effect = actionEffect;
            Damage = damage;
            ApCost = apCost;
            MpCost = mpCost;
            ActionType = actionType;
        }

        public string Icon { get; }
        public string Effect { get; }

        public int Damage { get; private set; }
        public int ApCost { get; private set; }
        public int MpCost { get; private set; }
        public ActionTypes.ActionType ActionType { get; private set; }
    }
}
