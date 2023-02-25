// Copyright Epic Games, Inc. All Rights Reserved.

#include "BlingvalierGameMode.h"
#include "BlingvalierCharacter.h"
#include "UObject/ConstructorHelpers.h"

ABlingvalierGameMode::ABlingvalierGameMode()
{
	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnBPClass(TEXT("/Game/ThirdPerson/Blueprints/BP_ThirdPersonCharacter"));
	if (PlayerPawnBPClass.Class != NULL)
	{
		DefaultPawnClass = PlayerPawnBPClass.Class;
	}
}
