using System.Runtime.InteropServices;

namespace Managed
{
    internal class Program
    {
        const string UnamanagedDllPath = $"C:\\Users\\jdrab\\source\\repos\\UnmanagedDll\\x64\\Debug\\UnmanagedDll.dll";

        public enum CLASS
        {
            HERO,
            BANDIT,
            WARRIOR,
            PRISONER,
            VAGABOND,
            SAMURAI
        };


        [StructLayout(LayoutKind.Sequential)]
        public struct Stats
        {
            public int Vitality;
            public int Endurance;
            public int Dexterity;
            public int Strength;
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct Character
        {
            public IntPtr Name;
            public CLASS Class;
            public int Level;
            public Stats stats;
        }

        // Import our custom dll
        [DllImport(UnamanagedDllPath)]
        public static extern int GetLifeAfterDamage(int damage);

        [DllImport(UnamanagedDllPath)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsCharacterAlive();

        [DllImport(UnamanagedDllPath, CharSet = CharSet.Unicode)]
        public static extern Stats GetDefaultStats();

        [DllImport(UnamanagedDllPath, CharSet = CharSet.Unicode)]
        public static extern Character GetDefaultCharacter();

        [DllImport(UnamanagedDllPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetCharacterName();

        [DllImport(UnamanagedDllPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetBossName();


        [DllImport(UnamanagedDllPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CompareCharacterName(string name);

        [DllImport(UnamanagedDllPath, CharSet = CharSet.Unicode)]
        public static extern CLASS GetDefaultClass();

        [DllImport(UnamanagedDllPath, CharSet = CharSet.Unicode)]
        public static extern void SetCharacterClass(IntPtr character, CLASS newClass);


        [DllImport(UnamanagedDllPath, CharSet = CharSet.Unicode)]
        public static extern int GetCharacterLevel();

        [DllImport(UnamanagedDllPath, CharSet = CharSet.Unicode)]
        public static extern void UpdateStats(IntPtr Stats, out Character newCharacter);


        static void Main()
        {
            Console.WriteLine("************* Call dll functions: ******************\n\n");

            //
            //// Handle common types
            // int
            int damage = 34;
            int total = GetLifeAfterDamage(damage);
            Console.WriteLine($"Character life after {damage} damage = {total}\n\n");

            // bool
            Console.WriteLine($"Is my character still alive ? {IsCharacterAlive()}");

            // Enums
            CLASS Class = GetDefaultClass();
            Console.WriteLine($"Default class: {Class}\n\n");

            //// Handle char*
            // Parameter
            Console.WriteLine($"Is main Character name Maidenless ? {CompareCharacterName("Maidenless")}");
            Console.WriteLine($"Is main Character name Gwyn ? {CompareCharacterName("Gwyn")}\n\n");

            // Return type
            IntPtr namePtr = GetCharacterName();
            string? name = Marshal.PtrToStringAnsi(namePtr);
            Console.WriteLine($"My character name {name}\n\n");

            // Handle blittable structs
            Stats defaultStats = GetDefaultStats();
            Console.WriteLine($"Default stats: Vit {defaultStats.Vitality}, End {defaultStats.Endurance}, Dex {defaultStats.Dexterity}, Str {defaultStats.Strength} \n");


            // Handle non-blittable structs

            Character defaultCharacter = GetDefaultCharacter();
            string characterName = Marshal.PtrToStringAnsi(defaultCharacter.Name);
            Console.WriteLine($"Default character : \n Name {characterName} \n Class {defaultCharacter.Class} \n Level {defaultCharacter.Level} " +
                $"\n Stats: \n ** Vitality {defaultCharacter.stats.Vitality}\n ** Endurance {defaultCharacter.stats.Endurance}\n ** Dexterity {defaultCharacter.stats.Dexterity}\n ** Strength {defaultCharacter.stats.Strength}\n\n");

            // Pointers
            Stats stats;
            stats.Vitality = 20;
            stats.Endurance = 12;
            stats.Dexterity = 15;
            stats.Strength = 11;

            int sizeofStats = Marshal.SizeOf(typeof(Stats));
            IntPtr statsPtr = Marshal.AllocHGlobal(sizeofStats);
            Marshal.StructureToPtr(stats, statsPtr, false);

            Character character;
            UpdateStats(statsPtr, out character);
            //Character character = (Character)Marshal.PtrToStructure(characterPtr, typeof(Character));

            characterName = Marshal.PtrToStringAnsi(character.Name);
            Console.WriteLine($"Default character : \n Name {characterName} \n Class {character.Class} \n Level {character.Level} " +
                $"\n Stats: \n ** Vitality {defaultCharacter.stats.Vitality}\n ** Endurance {defaultCharacter.stats.Endurance}\n ** Dexterity {defaultCharacter.stats.Dexterity}\n" +
                $" ** Strength {defaultCharacter.stats.Strength}\n\n");

            // Cleanup
            Marshal.FreeHGlobal(statsPtr);

        }
    }
}