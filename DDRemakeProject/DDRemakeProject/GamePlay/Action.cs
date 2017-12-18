namespace DDRemakeProject.GamePlay
{
    public class Action
    {
        public Action(ButtonStatesImages actionIcons, string actionEffect, int damage,int apCost,int mpCost,ActionTypes.ActionType actionType)
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

        public int Damage { get; private set; }
        public int ApCost { get; private set; }
        public int MpCost { get; private set; }
        public ActionTypes.ActionType ActionType { get; private set; }
    }
}
