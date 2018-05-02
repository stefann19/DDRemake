using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using DDRemakeProject.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WpfAnimatedGif;
using NReco;
namespace DDRemakeProject.GamePlay.New.Character.Logic
{
    public enum Races
    {
        Bat,
        Boar,
        Dino,
        Dragon,
        Ghost,
        Bowman,
        Mage,
        Paladin,
        Warrior
        
    }
    public class Race 
    {
        private string _avatarPath;
        private string _iconPath;
        private Races _name;

        public static string GetRaceLocation(Races race) =>  $"{AppDomain.CurrentDomain.BaseDirectory}\\Assets\\Races\\{race.ToString()}.json";
        public static Race GetRaceFromEnum(Races race)
        {
            string jsonString =File.ReadAllText(GetRaceLocation(race));
            return JsonConvert.DeserializeObject<Race>(jsonString);
        } 

        public Race()
        {
            /*string FullPath(string localPath) => System.IO.Path.Combine(Environment.CurrentDirectory, localPath);

            Avatar = new BitmapImage(new Uri(FullPath(AvatarPath)));
            Icon = new BitmapImage(new Uri(FullPath(IconPath)));*/

        }

        public Race(Races name,string avatarPath, string iconPath, int strengthGrowth, int agilityGrowth, int intelligenceGrowth, int enduranceGrowth, Resist armourResistanceFactors, Resist airResistanceFactors, Resist fireArmourResistanceFactors, Resist waterResistanceFactors)
        {
            Name = name;
            _avatarPath = avatarPath;
            _iconPath = iconPath;

            StrengthGrowth = strengthGrowth;
            AgilityGrowth = agilityGrowth;
            IntelligenceGrowth = intelligenceGrowth;
            EnduranceGrowth = enduranceGrowth;
            ArmourResistanceFactors = armourResistanceFactors;
            AirResistanceFactors = airResistanceFactors;
            FireResistanceFactors = fireArmourResistanceFactors;
            WaterResistanceFactors = waterResistanceFactors;
        }


        public Race(Races name, string iconPath, string avatarPath, int strengthGrowth, int agilityGrowth, int intelligenceGrowth, int enduranceGrowth, Resist armourResistanceFactors, Resist airResistanceFactors, Resist fireArmourResistanceFactors, Resist waterResistanceFactors, Resist earthResistanceFactors, string armourAttackFormulaString, string fireAttackFormulaString, string waterAttackFormulaString, string airAttackFormulaString, string earthAttackFormulaString)
        {
            Name = name;
            IconPath = iconPath;
            AvatarPath = avatarPath;

            StrengthGrowth = strengthGrowth;
            AgilityGrowth = agilityGrowth;
            IntelligenceGrowth = intelligenceGrowth;
            EnduranceGrowth = enduranceGrowth;
            ArmourResistanceFactors = armourResistanceFactors;
            AirResistanceFactors = airResistanceFactors;
            FireResistanceFactors = fireArmourResistanceFactors;
            WaterResistanceFactors = waterResistanceFactors;
            EarthResistanceFactors = earthResistanceFactors;
            ArmourAttackFormulaString = armourAttackFormulaString;
            FireAttackFormulaString = fireAttackFormulaString;
            WaterAttackFormulaString = waterAttackFormulaString;
            AirAttackFormulaString = airAttackFormulaString;
            EarthAttackFormulaString = earthAttackFormulaString;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public Races Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
            }
        }

        private string GetFullPath(string localPath) => System.IO.Path.Combine(Environment.CurrentDirectory, localPath);

        public string AvatarPath
        {
            get => _avatarPath;
            set
            {
                _avatarPath = value;
                Avatar = new BitmapImage(new Uri(GetFullPath(AvatarPath)));
            }
        }

        public string IconPath
        {
            get => _iconPath;
            set
            {
                _iconPath = value;
                Icon = new BitmapImage(new Uri(GetFullPath(IconPath)));
            }
        }

        [JsonIgnore]
        public BitmapImage Avatar { get; set; }

        [JsonIgnore]
        public BitmapImage Icon { get; set; }


        public int StrengthGrowth { get; set; }
        public int AgilityGrowth { get; set; }
        public int IntelligenceGrowth { get; set; }
        public int EnduranceGrowth { get; set; }

        public Resist ArmourResistanceFactors { get; set; }
        public Resist AirResistanceFactors { get; set; }
        public Resist FireResistanceFactors { get; set; }
        public Resist WaterResistanceFactors { get; set; }
        public Resist EarthResistanceFactors { get; set; }

        public String ArmourAttackFormulaString { get; set; }
        public String FireAttackFormulaString { get; set; }
        public String WaterAttackFormulaString { get; set; }
        public String AirAttackFormulaString { get; set; }
        public String EarthAttackFormulaString { get; set; }


        public class Resist
        {
            public Resist(double flat, double percentage)
            {
                Flat = flat;
                Percentage = percentage;
            }

            public Resist()
            {
            }

            public double Flat { get; set; }
            public double Percentage { get; set; }

            public override string ToString()
            {
                return $"-({Flat}+{Percentage}%Damage)/point";
            }
        }


    }
}
