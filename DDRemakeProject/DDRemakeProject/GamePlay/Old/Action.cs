namespace DDRemakeProject.GamePlay.Old
{
    public class Action
    {
        public Action(ButtonStatesImages actionIcons, string actionEffect, int damage, int apCost, int mpCost,
            ActionTypes.ActionType actionType)
        {
            Icon = actionIcons;
            Effect = actionEffect;
            Damage = damage;
            ApCost = apCost;
            MpCost = mpCost;
            ActionType = actionType;
        }

        public ButtonStatesImages Icon { get; }
        public string Effect { get; }

        public int Damage { get; }
        public int ApCost { get; }
        public int MpCost { get; }
        public ActionTypes.ActionType ActionType { get; }
    }
}