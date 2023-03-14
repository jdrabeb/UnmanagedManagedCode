#pragma once


#include "../UnmanagedDll/UnmanagedDll.h"
#include <vector>

using namespace System;

namespace CLIWrapper{
	public ref class ManagedMap
	{
	public:
		ManagedMap();
		~ManagedMap();

		String^ GetCurrentZone();
		void SetCurrentZone(String^ updatedZone);
		array<String^>^ GetBossList();
		int GetBonfireCount();
	private:
		Map* map;
	};
}
