#include "pch.h"

#include "CLIWrapper.h"


using namespace CLIWrapper;
using namespace Runtime::InteropServices;

ManagedMap::ManagedMap()
{
	map = new Map();
}

ManagedMap::~ManagedMap()
{
	delete map;
}

String^ ManagedMap::GetCurrentZone()
{
	std::string currentZone = map->GetCurrentZone();
	return gcnew String(currentZone.c_str());
}

void ManagedMap::SetCurrentZone(String^ updatedZone)
{
	const char* newZone =
		(const char*)(Marshal::StringToHGlobalAnsi(updatedZone)).ToPointer();
	map->SetCurrentZone(newZone);
	Marshal::FreeHGlobal(IntPtr((void*)newZone));
}
array<String^>^ ManagedMap::GetBossList()
{
	auto nativeBossesList = map->GetBossList();
	size_t size = nativeBossesList.size();
	array<String^>^ bosses = gcnew array<String^>(size);
	for (int i = 0; i < size; ++i)
	{
		bosses[i] = gcnew String(nativeBossesList[i].c_str());
	}
	return bosses;

}
int ManagedMap::GetBonfireCount()
{
	return map->GetBonfireCount();
}
