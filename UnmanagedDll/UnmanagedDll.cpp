#include "pch.h" 
#include "UnmanagedDll.h"

Stats* stats_ = new Stats{ 14, 12, 9, 16 };
Character* character_ = new Character{ "Maidenless", CLASS::WARRIOR, 7,  *stats_};


int GetLifeAfterDamage(int damage)
{
	return 100 - damage;
}

bool IsCharacterAlive()
{
	return true;
}

int GetCharacterLevel(Character character)
{
	return character.Level;
}

const char* GetCharacterName()
{
	return character_->Name;
}

Character GetDefaultCharacter()
{
	//return Character{ "Maidenless", CLASS::HERO, 7, 100 };
	return *character_;
}

bool CompareCharacterName(const char* name)
{
	if (std::strcmp(character_->Name, name) == 0)
	{
		return true;
	}
	return false;
}

Stats GetDefaultStats()
{
	return *stats_;
	//return Stats{ 14, 12, 9, 16 };
}

CLASS GetDefaultClass()
{
	return CLASS::WARRIOR;
}

void UpdateStats(Stats* stats, Character updatedCharacter)
{
	character_->stats = *stats;
	updatedCharacter = *character_;
}

Map::Map()
{}

std::string Map::GetCurrentZone()
{
	return currentZone;
}

void Map::SetCurrentZone(std::string updatedZone)
{
	currentZone = updatedZone;
}

std::vector<std::string> Map::GetBossList()
{
	return bossList;
}

int Map::GetBonfireCount()
{
	return bonfires;
}