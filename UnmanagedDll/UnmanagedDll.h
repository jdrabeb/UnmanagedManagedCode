#pragma once
#include <string>
#include <vector>

#ifdef UNMANAGEDDLL_EXPORTS
#define UNMANAGEDDLL_API __declspec(dllexport)
#else
#define UNMANAGEDDLL_API __declspec(dllimport)
#endif

enum class CLASS
{
	WARRIOR,
	KNIGHT,
	BADIT,
	THIED,
	PYROMANCER
};

struct Stats
{
	int Vitality;
	int Endurance;
	int Dexterity;
	int Strength;
};

struct Character
{
	const char* Name;
	CLASS Class;
	int Level;
	Stats stats;
};

class UNMANAGEDDLL_API Map
{
public:
    Map();

	std:: string GetCurrentZone();
	void SetCurrentZone(std::string updatedZone);
	std::vector<std::string> GetBossList();
	int GetBonfireCount();

private:
	std::vector<std::string> bossList = { "Gundyr", "Abyss Watcher", "Lothric Prince" };
	std::string currentZone = "Anor Lando";
	int bonfires = 8;
};


extern "C" UNMANAGEDDLL_API int GetLifeAfterDamage(int damage);
extern "C" UNMANAGEDDLL_API bool IsCharacterAlive();
extern "C" UNMANAGEDDLL_API Character GetDefaultCharacter();
extern "C" UNMANAGEDDLL_API Stats GetDefaultStats();
extern "C" UNMANAGEDDLL_API int GetCharacterLevel(Character character);
extern "C" UNMANAGEDDLL_API CLASS GetDefaultClass();
extern "C" UNMANAGEDDLL_API const char* GetCharacterName();
extern "C" UNMANAGEDDLL_API bool CompareCharacterName(const char* name);
extern "C" UNMANAGEDDLL_API void UpdateStats(Stats * stats, Character updatedCharacter);
